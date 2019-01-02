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

namespace HttpDownloader
{
	public partial class Downloader : UserControl
	{
		HttpWebRequest request;

		public Downloader()
		{
			InitializeComponent();
		}

		internal void Start(DownloadConfig config)
		{
			lblName.Text = config.URL;
			ThreadPool.QueueUserWorkItem(_Request, config);
		}

		internal void Cancel()
		{
			if(request != null)
			{
				request.Abort();
				request = null;
			}
		}

		private void _Request(object state)
		{
			var config = (DownloadConfig)state;

			try
			{
				using (var output = new FileStream(config.Save, FileMode.Create))
				{
					var req = request = (HttpWebRequest)WebRequest.Create(config.URL);
					req.Method = "GET";
					if (config.Referer != null)
						req.Referer = config.Referer;
					//req.Accept = "video/webm,video/ogg,video/*;q=0.9,application/ogg;q=0.7,audio/*;q=0.6,*/*;q=0.5";

					using (var resp = (HttpWebResponse)req.GetResponse())
					{
						if (resp.StatusCode < HttpStatusCode.OK || resp.StatusCode >= HttpStatusCode.Ambiguous)
							throw new Exception("Http status: " + resp.StatusCode);

						int contentLength = (int)resp.ContentLength;
						_ReportProgressStyle(contentLength > 0 ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee, contentLength);
						int writeBytes = 0;

						using (Stream respStream = resp.GetResponseStream())
						{
							byte[] buffer = new byte[4096];
							int read;
							do
							{
								read = respStream.Read(buffer, 0, buffer.Length);

								if (read > 0)
								{
									output.Write(buffer, 0, read);
									writeBytes += read;
									_ReportProgress(writeBytes);
								}
								else
								{
									_ReportComplete();
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
				throw ex;
			}
			finally
			{
				request = null;
			}
		}

		private void _ReportProgressStyle(ProgressBarStyle style, int maxValue)
		{
			if (this.InvokeRequired)
				this.Invoke(new Action<ProgressBarStyle, int>(_ReportProgressStyle), style, maxValue);
			else
			{
				progress.Style = style;
				if (style != ProgressBarStyle.Marquee)
					progress.Maximum = maxValue;
				progress.Value = 0;
			}
		}

		private void _ReportProgress(int p)
		{
			if (this.InvokeRequired)
				this.Invoke(new Action<int>(_ReportProgress), p);
			else
				progress.Value = p;
		}

		private void _ReportComplete()
		{
			if (this.InvokeRequired)
				this.Invoke(new Action(_ReportComplete));
			else
			{
				progress.Value = progress.Maximum;
				lblName.Text = "(Complete)" + lblName.Text;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Cancel();
			((MainForm)this.ParentForm).OnTaskCancelled(this);
		}
	}

	public class DownloadConfig
	{
		public string URL;
		public string Referer;
		public string Save;
	}

	[Serializable]
	public class DefaultConfig
	{
		public string Referer;
		public string Save;

		public int X, Y, W, H;
	}
}
