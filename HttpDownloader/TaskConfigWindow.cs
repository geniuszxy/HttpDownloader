using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog();
			if (!string.IsNullOrWhiteSpace(tbOutput.Text))
			{
				dialog.InitialDirectory = Path.GetDirectoryName(tbOutput.Text);
				dialog.FileName = Path.GetFileName(tbOutput.Text);
			}
			if (dialog.ShowDialog(this) == DialogResult.OK)
				tbOutput.Text = dialog.FileName;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			string URL = tbURL.Text;
			string output = tbOutput.Text;

			if (string.IsNullOrWhiteSpace(URL))
			{
				MessageBox.Show("必须输入网址");
				return;
			}

			if (string.IsNullOrWhiteSpace(output))
			{
				MessageBox.Show("必须指定输出目录");
				return;
			}


			var dc = (DownloadConfig)cbbConfigs.SelectedItem;
			dc.URL = URL;
			dc.Save = output;
			dc.Referer = WrapTextBox(tbRefer);
			dc.Method = WrapTextBox(tbMethod);
			dc.Resume = cbResume.Checked;
			dc.Accept = WrapTextBox(tbAccept);
			dc.Pragma = WrapTextBox(tbPragma);
			dc.Connection = WrapTextBox(tbConnection);
			dc.UserAgent = WrapTextBox(tbUserAgent);
			dc.Cache_Control = WrapTextBox(tbCacheControl);
			dc.Sec_Fetch_Dest = WrapTextBox(tbSecFetchDest);
			dc.Sec_Fetch_Mode = WrapTextBox(tbSecFetchMode);
			dc.Sec_Fetch_Site = WrapTextBox(tbSecFetchSite);

			((MainForm)this.Owner).AddNewTask(dc);

			this.Close();
		}

		private void cbbConfigs_SelectedIndexChanged(object sender, EventArgs e)
		{
			var dc = (DownloadConfig)cbbConfigs.SelectedItem;

			tbURL.Text = dc.URL ?? "";
			tbOutput.Text = dc.Save ?? "";
			tbRefer.Text = dc.Referer ?? "";
			tbMethod.Text = dc.Method ?? "";
			cbResume.Checked = dc.Resume;
			tbAccept.Text = dc.Accept ?? "";
			tbPragma.Text = dc.Pragma ?? "";
			tbConnection.Text = dc.Connection ?? "";
			tbUserAgent.Text = dc.UserAgent ?? "";
			tbCacheControl.Text = dc.Cache_Control ?? "";
			tbSecFetchDest.Text = dc.Sec_Fetch_Dest ?? "";
			tbSecFetchMode.Text = dc.Sec_Fetch_Mode ?? "";
			tbSecFetchSite.Text = dc.Sec_Fetch_Site ?? "";
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

			if(cnfs.Count > 0)
				cbbConfigs.SelectedIndex = index >= cnfs.Count ? index - 1 : index;
			else
			{
				var dc = new DownloadConfig();
				cnfs.Add(dc);
				cbbConfigs.Items.Add(dc);
				cbbConfigs.SelectedIndex = 0;
			}
		}

		private static string WrapTextBox(TextBox tb)
		{
			var text = tb.Text;
			return string.IsNullOrWhiteSpace(text) ? null : text;
		}
	}
}
