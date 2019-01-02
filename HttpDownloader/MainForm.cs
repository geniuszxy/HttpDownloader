using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HttpDownloader
{
	public partial class MainForm : Form
	{
		const string CONFIG_FILE = "config.xml";
		DefaultConfig _dc;

		public MainForm()
		{
			InitializeComponent();

			if (File.Exists(CONFIG_FILE))
			{
				var serializer = new XmlSerializer(typeof(DefaultConfig));
				_dc = (DefaultConfig)serializer.Deserialize(new FileStream(CONFIG_FILE, FileMode.Open));

				this.Location = new Point(_dc.X, _dc.Y);
				this.Size = new Size(_dc.W, _dc.H);
			}
			else
				_dc = new DefaultConfig();
		}

		internal void AddNewTask(DownloadConfig config)
		{
			if (InvokeRequired)
				Invoke(new Action<DownloadConfig>(AddNewTask), config);
			else
			{
				_dc.Referer = config.Referer;
				_dc.Save = config.Save;
				var d = new Downloader
				{
					Width = flowLayoutPanel1.ClientSize.Width,
					Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
				};
				flowLayoutPanel1.Controls.Add(d);

				d.Start(config);
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new TaskConfigWindow(_dc);
			dialog.Show(this);
		}

		internal void OnTaskCancelled(Downloader downloader)
		{
			if (InvokeRequired)
				Invoke(new Action<Downloader>(OnTaskCancelled), downloader);
			else
			{
				flowLayoutPanel1.Controls.Remove(downloader);
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if(_dc == null)
				_dc = new DefaultConfig();

			_dc.X = this.Location.X;
			_dc.Y = this.Location.Y;
			_dc.W = this.Width;
			_dc.H = this.Height;

			var serializer = new XmlSerializer(typeof(DefaultConfig));
			serializer.Serialize(new FileStream(CONFIG_FILE, FileMode.Create), _dc);
		}
	}
}
