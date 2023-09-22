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
	public partial class MainConfigWindow : Form
	{
		public MainConfigWindow(ConfigFile cf)
		{
			InitializeComponent();
			pgMainConfig.SelectedObject = cf;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
		}
	}
}
