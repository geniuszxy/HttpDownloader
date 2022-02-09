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
				var fn = _GetSaveFileName();
				if (fn == null)
					return;

				config.Save = fn;
				using (var output = new FileStream(fn, fm, FileAccess.ReadWrite))
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

		private string _GetSaveFileName()
		{
			var fn = config.Save;
			if (writeBytes > 0) //this is a resume
				return fn;

			var ow = config.Overwrite;
			switch (ow)
			{
				case OverwriteMethod.Confirm:
					return !File.Exists(fn) ||
						MessageBox.Show("Overwrite?" + (config.Resume ? " [Resume]" : ""),
							"File exists", MessageBoxButtons.YesNo) == DialogResult.Yes ?
						fn : null;

				case OverwriteMethod.AutoRename:
					if (File.Exists(fn))
					{
						var dir = Path.GetDirectoryName(fn);
						fn = Path.GetFileName(fn);
						var ext = Path.GetExtension(fn);
						fn = Path.GetFileNameWithoutExtension(fn);
						var p = fn.LastIndexOfAny("-_.".ToCharArray());
						if(p >= 0 && p < fn.Length - 1)
						{
							var prefix = fn.Substring(0, p + 1);
							var suffix = fn.Substring(p + 1);
							if(int.TryParse(suffix, out int id))
							{
								do
								{
									id++;
									fn = Path.Combine(dir, prefix + id + ext);
								}
								while (File.Exists(fn));
								dir = null;
							}
						}
						if (dir != null)
						{
							int id = 0;
							do
							{
								id++;
								fn = Path.Combine(dir, fn + "-" + id + ext);
							}
							while (File.Exists(fn));
						}
					}
					return fn;

				case OverwriteMethod.Replace:
				default:
					return fn;
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
				SetRetry();

				if (config.AutoRetry)
					_AutoRetry(0);
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
				SetRetry();
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
