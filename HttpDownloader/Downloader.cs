using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace HttpDownloader
{
	public partial class Downloader : UserControl
	{
		enum State
		{
			Idle,
			Start,
			Request,
			Download,
			Complete,
			Pause,
			Error,
			Retry,
		}

		DownloadConfig config;
		HttpWebRequest request;
		long writeBytes;
		int autoRetryCount;
		string savePath;
		State state;

		public Downloader()
		{
			InitializeComponent();
		}

		internal void Start(DownloadConfig config)
		{
			state = State.Start;
			this.config = config;
			lblName.Text = config.Uri.ToString();
			btnOther.Text = "‖";
			ThreadPool.QueueUserWorkItem(_Request);
		}

		private void Restart()
		{

		}

		internal void Cancel()
		{
			if (state != State.Complete && state != State.Error)
				state = State.Pause;

			if (request != null)
			{
				try
				{
					request.Abort();
				}
				catch (Exception ex)
				{
					_ReportError(ex);
				}
				finally
				{
					request = null;
				}
			}
		}

		private void _Request(object state)
		{
			try
			{
				FileMode fm = config.Resume ? FileMode.OpenOrCreate : FileMode.Create;
				if (!_UpdateSavePath())
					return;

				using (var output = new FileStream(savePath, fm, FileAccess.ReadWrite))
				{
					var req = request = config.CreateRequest();
					writeBytes = 0;
					if (config.Resume)
					{
						writeBytes = (int)output.Seek(0, SeekOrigin.End);
						if (writeBytes > 0)
							req.AddRange(writeBytes);
					}

					state = State.Request;
					using (var resp = (HttpWebResponse)req.GetResponse())
					{
						if (resp.StatusCode < HttpStatusCode.OK || resp.StatusCode >= HttpStatusCode.Ambiguous)
							throw new Exception("Http status: " + resp.StatusCode);

						long contentLength = resp.ContentLength;
						ProgressBarStyle barStyle = ProgressBarStyle.Marquee;
						if (contentLength > 0)
						{
							barStyle = ProgressBarStyle.Continuous;
							contentLength += writeBytes;
						}
						this.TryInvoke(_ReportProgressStyle, barStyle, contentLength);

						state = State.Download;
						using (Stream respStream = resp.GetResponseStream())
						{
							byte[] buffer = new byte[32768]; //32K
							do
							{
								int read = respStream.Read(buffer, 0, buffer.Length);

								if (read > 0)
								{
									output.Write(buffer, 0, read);
									writeBytes += read;
									this.TryInvoke(_ReportProgress, 0L, 0L);
								}
								else
								{
									this.TryInvoke(_ReportComplete, contentLength);
									return;
								}
							}
							while (true);
						}
					}
				}
			}
			catch(Exception ex)
			{
				_ReportError(ex);
			}
			finally
			{
				request = null;
			}
		}

		private bool _UpdateSavePath()
		{
			if (savePath != null)
				return true;

			string filename, directory = null;

			if (config.AutoFilename)
				filename = Path.GetFileName(config.Uri.AbsolutePath);
			else
				filename = config.Filename.Trim();

			if (!string.IsNullOrEmpty(filename))
			{
				directory = _GetDirectoryFromSave(config.Save);
				savePath = Path.Combine(directory, filename);
			}
			else
				savePath = config.Save;

			switch (config.Overwrite)
			{
				case OverwriteMethod.Confirm:
					//file isn't existed or confirm to overwrite
					return !File.Exists(savePath) ||
						MessageBox.Show(
							"Overwrite?" + (config.Resume ? " [Resume]" : ""),
							"File exists",
							MessageBoxButtons.YesNo
						) == DialogResult.Yes;

				case OverwriteMethod.AutoRename:
					if (File.Exists(savePath))
						_AutoRename(directory, filename);
					goto default;

				case OverwriteMethod.Replace:
				default:
					return true;
			}
		}

		private static string _GetDirectoryFromSave(string save)
		{
			if (Directory.Exists(save))
				return save;

			save = Path.GetDirectoryName(save);
			if (Directory.Exists(save))
				return save;

			throw new InvalidDataException("Save doesn't contain a directory: " + save);
		}

		private void _AutoRename(string dir, string fn)
		{
			if (dir == null)
				dir = Path.GetDirectoryName(config.Save);
			if (string.IsNullOrEmpty(fn))
				fn = Path.GetFileName(config.Save);

			var ext = Path.GetExtension(fn);
			fn = Path.GetFileNameWithoutExtension(fn);

			var p = fn.LastIndexOfAny("-_.".ToCharArray());
			if (p >= 0 && p < fn.Length - 1)
			{
				var prefix = fn.Substring(0, p + 1);
				var suffix = fn.Substring(p + 1);
				if (int.TryParse(suffix, out int id))
				{
					do
					{
						id++;
						fn = prefix + id + ext;
						savePath = Path.Combine(dir, fn);
					}
					while (File.Exists(savePath));
					dir = null;
				}
			}
			if (dir != null)
			{
				int id = 0;
				var prefix = fn;
				do
				{
					id++;
					fn = prefix + "-" + id + ext;
					savePath = Path.Combine(dir, fn);
				}
				while (File.Exists(savePath));
			}

			//update config
			if (!config.AutoFilename)
			{
				if (!string.IsNullOrWhiteSpace(config.Filename))
					config.Filename = fn;
				else
					config.Save = savePath;
			}
		}

		private void _ReportProgressStyle(ProgressBarStyle style, long maxValue)
		{
			progress.Style = style;
			if (style != ProgressBarStyle.Marquee)
				progress.Maximum = (int)maxValue;
			progress.Value = 0;
		}

		private void _ReportProgress(long read, long dur)
		{
			string text = "";
			if (writeBytes <= progress.Maximum)
			{
				progress.Value = (int)writeBytes;
				text = ((double)writeBytes / progress.Maximum).ToString("0.##%");
			}

			progress.Text = text;
			progress.Invalidate();
		}

		private void _ReportComplete(long maxValue)
		{
			if (maxValue <= 0 || writeBytes >= maxValue)
			{
				state = State.Complete;
				progress.Value = progress.Maximum;
				progress.Text = "Complete";
				config = null;
				btnOther.Text = "";
			}
			else
			{
				state = State.Error;
				progress.Text = "Interrupted";
				TryRetry();
			}
		}

		private void _ReportError(Exception ex)
		{
			if ((state == State.Pause && ex is WebException) || state == State.Complete || config == null)
				return;

			state = State.Error;
			this.TryInvoke(e =>
			{
				progress.Text = "Error";
				((MainForm)ParentForm).ReportError(e);
				TryRetry();
			},
			ex);
		}

		//Pause or retry
		private void btnOther_Click(object sender, EventArgs e)
		{
			switch (state)
			{
				case State.Start:
				case State.Request:
				case State.Download:
					Cancel();
					SetRetryButton();
					break;

				case State.Pause:
				case State.Error:
					autoRetryCount = 0;
					Restart();
					break;
			}
		}

		//Remove
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Cancel();
			config = null;
			if(ParentForm is MainForm f)
				f.OnTaskCancelled(this);
		}

		private void SetRetryButton()
		{
			config.Resume = true;
			btnOther.Text = "↻";
		}

		private void TryRetry()
		{
			SetRetryButton();

			if (!config.AutoRetry || autoRetryCount >= 5)
				return;

			state = State.Retry;
			autoRetryCount++;
			_AutoRetry(0);
		}

		private void _AutoRetry(object state)
		{
			if (state.Equals(0))
				ThreadPool.QueueUserWorkItem(_AutoRetry, 1);
			else
			{
				Thread.Sleep(2000);

				if (InvokeRequired)
					Invoke(new WaitCallback(_AutoRetry), state);
				else if (config != null)
					Start(config);
			}
		}
	}
}
