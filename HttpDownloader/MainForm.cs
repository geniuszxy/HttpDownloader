using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HttpDownloader
{
	public partial class MainForm : Form
	{
		const string CONFIG_FILE = "config.xml";
		ConfigFile _cf;
		LogWindow _logw;

		public MainForm()
		{
			InitializeComponent();

			if (File.Exists(CONFIG_FILE))
			{
				var serializer = new XmlSerializer(typeof(ConfigFile));
				using (var fs = new FileStream(CONFIG_FILE, FileMode.Open))
					_cf = (ConfigFile)serializer.Deserialize(fs);

				Location = new Point(_cf.X, _cf.Y);
				Size = new Size(_cf.W, _cf.H);
			}
			else
				_cf = new ConfigFile();

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		/// <summary>
		/// Callback from TaskConfigWindow
		/// </summary>
		internal void AddNewTask(DownloadConfig config)
		{
			if (InvokeRequired)
				Invoke(new Action<DownloadConfig>(AddNewTask), config);
			else
			{
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
			var dialog = new TaskConfigWindow(_cf);
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
			if (_cf == null)
				_cf = new ConfigFile();

			_cf.X = Location.X;
			_cf.Y = Location.Y;
			_cf.W = Width;
			_cf.H = Height;

			var serializer = new XmlSerializer(typeof(ConfigFile));
			using (var fs = new FileStream(CONFIG_FILE, FileMode.Create))
				serializer.Serialize(fs, _cf);
		}

		internal void ReportError(Exception ex)
		{
			if(_logw == null)
			{
				_logw = new LogWindow();
			}

			_logw.Show();
			_logw.Append(ex.Message);
			_logw.Append(ex.StackTrace);
		}
	}
}
