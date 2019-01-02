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
		public TaskConfigWindow(DefaultConfig config)
		{
			InitializeComponent();

			textBox2.Text = config.Referer;
			textBox3.Text = config.Save;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog();
			if (!string.IsNullOrWhiteSpace(textBox3.Text))
			{
				dialog.InitialDirectory = Path.GetDirectoryName(textBox3.Text);
				dialog.FileName = Path.GetFileName(textBox3.Text);
			}
			if (dialog.ShowDialog(this) == DialogResult.OK)
				textBox3.Text = dialog.FileName;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string URL = textBox1.Text;
			string refer = textBox2.Text;
			string output = textBox3.Text;

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

			if (string.IsNullOrWhiteSpace(refer))
				refer = null;

			((MainForm)this.Owner).AddNewTask(new DownloadConfig
			{
				URL = URL,
				Referer = refer,
				Save = output,
			});

			this.Close();
		}
	}
}
