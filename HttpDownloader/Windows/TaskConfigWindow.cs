using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpDownloader
{
	public partial class TaskConfigWindow : Form
	{
		ConfigFile _cf;

		public TaskConfigWindow(ConfigFile cf)
		{
			InitializeComponent();

			_cf = cf;

			if (cf.Configs.Count == 0)
				cf.Configs.Add(new DownloadConfig());
			cbbConfigs.Items.AddRange(cf.Configs.ToArray());
			cbbConfigs.SelectedIndex = cf.LastConfigIndex;
			cf.TaskWindow.Apply(this);
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			//var dialog = new SaveFileDialog();
			//if (!string.IsNullOrWhiteSpace(tbOutput.Text))
			//{
			//	dialog.InitialDirectory = Path.GetDirectoryName(tbOutput.Text);
			//	dialog.FileName = Path.GetFileName(tbOutput.Text);
			//}
			//if (dialog.ShowDialog(this) == DialogResult.OK)
			//	tbOutput.Text = dialog.FileName;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			var dc = (DownloadConfig)cbbConfigs.SelectedItem;

			if (string.IsNullOrWhiteSpace(dc.URL))
			{
				MessageBox.Show("必须输入网址");
				return;
			}

			if (string.IsNullOrWhiteSpace(dc.Save))
			{
				MessageBox.Show("必须指定输出目录");
				return;
			}

			if (Owner is MainForm f)
			{
				if (dc.MultiURL)
					f.TryInvoke(f.AddNewMultiTasks, dc.Clone());
				else
					f.TryInvoke(f.AddNewTask, dc.Clone());
			}

			Close();
		}

		private void cbbConfigs_SelectedIndexChanged(object sender, EventArgs e)
		{
			_cf.LastConfigIndex = cbbConfigs.SelectedIndex;
			var dc = (DownloadConfig)cbbConfigs.SelectedItem;
			pgHeader.SelectedObject = dc;
		}

		private void btnAddConfig_Click(object sender, EventArgs e)
		{
			var index = cbbConfigs.SelectedIndex;
			var cnfs = _cf.Configs;
			var dc = cnfs[index].Clone();
			cnfs.Add(dc);
			cbbConfigs.Items.Add(dc);
			cbbConfigs.SelectedIndex = cnfs.Count - 1;
		}

		private void btnRemoveConfig_Click(object sender, EventArgs e)
		{
			var index = cbbConfigs.SelectedIndex;
			var cnfs = _cf.Configs;
			cnfs.RemoveAt(index);
			cbbConfigs.Items.RemoveAt(index);

			if (cnfs.Count > 0)
				cbbConfigs.SelectedIndex = index >= cnfs.Count ? index - 1 : index;
			else
			{
				var dc = new DownloadConfig();
				cnfs.Add(dc);
				cbbConfigs.Items.Add(dc);
				cbbConfigs.SelectedIndex = 0;
			}
		}

		private void btnDNS_Click(object sender, EventArgs e)
		{
			var dc = (DownloadConfig)cbbConfigs.SelectedItem;
			var host = dc.Host;
			if (host == null)
			{
				MessageBox.Show("Set host first");
			}
			else
			{
				var hosts = Dns.GetHostEntry(host);
				var w = new DNSWindow(hosts);
				var result = w.ShowDialog(this);
				if (result == DialogResult.OK)
				{
					dc.IP = w.SelectedAddress.ToString();
					pgHeader.Refresh();
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SetLabelColumnWidth(pgHeader, 120);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			_cf.TaskWindow = new ControlRect(this);
		}

		private void tsbSave_Click(object sender, EventArgs e)
		{
			_cf.TaskWindow = new ControlRect(this);
			MainForm.SaveConfig(_cf);
		}

		//private void tbURL_TextChanged(object sender, EventArgs e)
		//{
		//	if (string.IsNullOrWhiteSpace(tbRefer.Text))
		//		tbRefer.Text = tbURL.Text;
		//}

		public static void SetLabelColumnWidth(PropertyGrid grid, int width)
		{
			const BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;

			var fi = grid.GetType().GetField("gridView", flag);
			var view = fi.GetValue(grid);
			var mi = view.GetType().GetMethod("MoveSplitterTo", flag);
			mi.Invoke(view, new object[] { width });
		}
	}
}
