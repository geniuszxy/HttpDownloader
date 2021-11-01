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
		long writeBytes;

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
				FileMode fm = config.Resume ? FileMode.OpenOrCreate : FileMode.Create;
				using (var output = new FileStream(config.Save, fm, FileAccess.ReadWrite))
				{
					var req = request = config.CreateRequest();
					if(config.Resume)
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
						_ReportProgressStyle(contentLength > 0 ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee, contentLength);
						writeBytes = 0;

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
				_ReportError(ex);
			}
			finally
			{
				request = null;
			}
		}

		private void _ReportProgressStyle(ProgressBarStyle style, long maxValue)
		{
			if (this.InvokeRequired)
				this.Invoke(new Action<ProgressBarStyle, long>(_ReportProgressStyle), style, maxValue);
			else
			{
				progress.Style = style;
				if (style != ProgressBarStyle.Marquee)
					progress.Maximum = (int)maxValue;
				progress.Value = 0;
			}
		}

		private void _ReportProgress(long p)
		{
			if (this.InvokeRequired)
				this.Invoke(new Action<long>(_ReportProgress), p);
			else
			{
				if (p <= progress.Maximum)
					progress.Value = (int)p;
				Invalidate();
			}
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

		private void _ReportError(Exception ex)
		{
			if (this.InvokeRequired)
				this.Invoke(new Action<Exception>(_ReportError), ex);
			else
			{
				lblName.Text = "(Error)" + lblName.Text;
				((MainForm)this.ParentForm).ReportError(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Cancel();
			((MainForm)this.ParentForm).OnTaskCancelled(this);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			var g = e.Graphics;
			var str = writeBytes.ToString();
			var f = Font;
			var size = g.MeasureString(str, f);
			g.DrawString(str, f, SystemBrushes.WindowText, btnOther.Left - size.Width - 3f, 3f);
		}
	}
}
