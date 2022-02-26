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

namespace HttpDownloader
{
	public partial class Downloader : UserControl
	{
		DownloadConfig config;
		HttpWebRequest request;
		long writeBytes;
		int autoRetryCount;
		string savePath;

		public Downloader()
		{
			InitializeComponent();
		}

		internal void Start(DownloadConfig config)
		{
			this.config = config;
			lblName.Text = config.Uri.ToString();
			btnOther.Text = "‖";
			ThreadPool.QueueUserWorkItem(_Request);
		}

		internal void Cancel()
		{
			if(request != null)
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
						_ReportProgressStyle(barStyle, contentLength);

						using (Stream respStream = resp.GetResponseStream())
						{
							byte[] buffer = new byte[32768]; //32K
							//var stopWatcher = Stopwatch.StartNew();
							//var readBytes = 0;
							do
							{
								int read = respStream.Read(buffer, 0, buffer.Length);

								if (read > 0)
								{
									output.Write(buffer, 0, read);
									writeBytes += read;
									//readBytes += read;
									//long elapsed = stopWatcher.ElapsedMilliseconds;
									//if (elapsed >= 1000)
									//{
									//	_ReportProgress(readBytes, elapsed);
									//	stopWatcher.Restart();
									//	readBytes = 0;
									//}
									_ReportProgress(0, 0);
								}
								else
								{
									_ReportComplete(contentLength);
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
				//Console.WriteLine(request.RequestUri);
				//Console.WriteLine(request.Host);
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
			if (InvokeRequired)
				Invoke(new Action<ProgressBarStyle, long>(_ReportProgressStyle), style, maxValue);
			else
			{
				progress.Style = style;
				if (style != ProgressBarStyle.Marquee)
					progress.Maximum = (int)maxValue;
				progress.Value = 0;
			}
		}

		private void _ReportProgress(long read, long dur)
		{
			if (InvokeRequired)
				Invoke(new Action<long, long>(_ReportProgress), read, dur);
			else
			{
				string text = "";
				//var bytes = (double)read / dur * 1000.0;
				//if (bytes > 1048576)
				//	text = (bytes / 1048576).ToString("0.# MB/s");
				//else if (bytes > 1024)
				//	text = (bytes / 1024).ToString("0.# KB/s");
				//else
				//	text = bytes.ToString("0.# B/s");

				if (writeBytes <= progress.Maximum)
				{
					progress.Value = (int)writeBytes;
					text = /*text + " " +*/ ((double)writeBytes / progress.Maximum).ToString("0.#%");
				}

				progress.Text = text;
				progress.Invalidate();
			}
		}

		private void _ReportComplete(long maxValue)
		{
			if (InvokeRequired)
				Invoke(new Action<long>(_ReportComplete), maxValue);

			else if (maxValue <= 0 || writeBytes >= maxValue)
			{
				progress.Value = progress.Maximum;
				progress.Text = "Complete";
				config = null;
				btnOther.Text = "";
			}
			else
			{
				progress.Text = "Interrupted";
				_TryRetry();
			}
		}

		private void _ReportError(Exception ex)
		{
			if (config == null)
				return;

			if (InvokeRequired)
				Invoke(new Action<Exception>(_ReportError), ex);
			else if (config != null)
			{
				progress.Text = "Error";
				((MainForm)ParentForm).ReportError(ex);
				_TryRetry();
			}
		}

		private void btnOther_Click(object sender, EventArgs e)
		{
			if(request != null)
			{
				Cancel();
				SetRetry();
			}
			else if(config != null)
			{
				autoRetryCount = 0;
				Start(config);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Cancel();
			config = null;
			((MainForm)ParentForm).OnTaskCancelled(this);
		}

		private void SetRetry()
		{
			config.Resume = true;
			btnOther.Text = "↻";
		}

		private void _TryRetry()
		{
			SetRetry();

			if (!config.AutoRetry || autoRetryCount >= 5)
				return;

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
