using System.Net;
using System.Windows.Forms;

namespace HttpDownloader
{
	public partial class DNSWindow : Form
	{
		public DNSWindow(IPHostEntry hosts)
		{
			InitializeComponent();

			listBox1.Items.AddRange(hosts.AddressList);
			DialogResult = DialogResult.Cancel;
		}

		private void listBox1_DoubleClick(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		public IPAddress SelectedAddress
		{
			get { return listBox1.SelectedItem as IPAddress; }
		}
	}
}
