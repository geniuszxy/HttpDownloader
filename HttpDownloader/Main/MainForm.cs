using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HttpDownloader
{
	public partial class MainForm : Form
	{
		const string CONFIG_FILE = "config.xml";
		ConfigFile _cf;

		public MainForm(string[] args)
		{
			InitializeComponent();

			if (File.Exists(CONFIG_FILE))
			{
				var serializer = new XmlSerializer(typeof(ConfigFile));
				using (var fs = new FileStream(CONFIG_FILE, FileMode.Open))
					_cf = (ConfigFile)serializer.Deserialize(fs);

				_cf.MainWindow.Apply(this);
			}
			else
				_cf = new ConfigFile();

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			if(args.Length > 0)
				Load += new InitTasks(args).OnLoad;
		}

		/// <summary>
		/// Callback from TaskConfigWindow
		/// </summary>
		internal void AddNewTask(DownloadConfig config)
		{
			var d = new Downloader
			{
				Height = 50,
				Dock = DockStyle.Top,
			};
			mainContainer.Panel1.Controls.Add(d);
			d.BringToFront();
			d.Start(config);
		}

		/// <summary>
		/// Callback from TaskConfigWindow
		/// </summary>
		internal async void AddNewMultiTasks(DownloadConfig config)
		{
			await Task.Yield();
			var splitter = new char[] { '\r', '\n' };
			var urls = config.URL.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
			Action<DownloadConfig> a = AddNewTask;
			foreach(var url in urls)
			{
				config.URL = url;
				this.TryInvoke(a, config.Clone());
				await Task.Yield();
			}
		}

		private void NewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new TaskConfigWindow(_cf);
			dialog.Show(this);
		}

		private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialog = new MainConfigWindow(_cf);
			dialog.ShowDialog(this);
		}

		internal void OnTaskCancelled(Downloader downloader)
		{
			mainContainer.Panel1.Controls.Remove(downloader);
			downloader.Dispose();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_cf == null)
				_cf = new ConfigFile();

			_cf.MainWindow = new ControlRect(this);

			SaveConfig(_cf);
		}

		/// <summary>
		/// To manually save the configure file
		/// </summary>
		internal static void SaveConfig(ConfigFile cf)
		{
			var serializer = new XmlSerializer(typeof(ConfigFile));
			using (var fs = new FileStream(CONFIG_FILE, FileMode.Create))
				serializer.Serialize(fs, cf);
		}

		internal void ReportError(Exception ex)
		{
			this.TryInvoke(AppendLog, ex.ToString());
		}

		private void ConsoleCheckedChanged(object sender, EventArgs e)
		{
			var on = consoleToolStripMenuItem.Checked;
			mainContainer.Panel2Collapsed = !on;
			consoleToolStripMenuItem.Text = (on ? "*" : "") + "&Console";
			if (!on)
				ClearLog();
		}

		private void OnClean(object sender, EventArgs e)
		{
			var panel = mainContainer.Panel1;
			var controls = panel.Controls;
			panel.SuspendLayout();
			{
				var removeList = new List<Control>();
				foreach (Downloader control in controls)
					if (control.IsComplete)
						removeList.Add(control);
				foreach (var control in removeList)
				{
					controls.Remove(control);
					control.Dispose();
				}
				removeList.Clear();
			}
			panel.ResumeLayout(true);
		}

		class InitTasks
		{
			string[] tasks;

			public InitTasks(string[] args)
			{
				tasks = args;
			}

			public void OnLoad(object sender, EventArgs e)
			{
				if (sender is MainForm form)
				{
					form.TryInvoke(form.CreateTask, tasks[0]); //Create only the first task
					form.Load -= OnLoad;
				}
			}
		}

		internal void CreateTask(string url)
		{
			var dc = new DownloadConfig();
			dc.URL = dc.Referer = url;
			_cf.Configs.Add(dc);
			_cf.LastConfigIndex = _cf.Configs.Count - 1;
			var dialog = new TaskConfigWindow(_cf);
			dialog.Show(this);
		}

		private void AppendLog(string text)
		{
			consoleToolStripMenuItem.Checked = true;
			if (log.TextLength > 0)
				log.AppendText("\n");
			log.AppendText(text);
			log.ScrollToCaret();
		}

		private void ClearLog()
		{
			log.Clear();
			log.ScrollToCaret();
		}
	}
}
