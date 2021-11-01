using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpDownloader
{
	public partial class LogWindow : Form
	{
		public LogWindow()
		{
			InitializeComponent();
		}

		private void LogWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Hide();
			e.Cancel = true;
		}

		public void Append(string text)
		{
			if (InvokeRequired)
				Invoke(new Action<string>(Append), text);
			else
			{
				if (richTextBox1.TextLength > 0)
					richTextBox1.AppendText("\n");
				richTextBox1.AppendText(text);
				richTextBox1.ScrollToCaret();
			}
		}
	}
}
