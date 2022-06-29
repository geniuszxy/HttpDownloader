using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace HttpDownloader
{
	public partial class DNSWindow : Form
	{
		CancellationTokenSource _cancel = new CancellationTokenSource();

		public DNSWindow(IPHostEntry hosts)
		{
			InitializeComponent();

			listView1.Items.AddRange(hosts.AddressList.Select(
				ipaddr => new ListViewItem(
					new string[] { ipaddr.ToString(), "-" }
					){ Tag = ipaddr })
				.ToArray());

			PingAddresses(_cancel.Token);

			DialogResult = DialogResult.Cancel;
		}

		public IPAddress SelectedAddress
		{
			get { return listView1.SelectedItems[0].Tag as IPAddress; }
		}

		private void listView1_DoubleClick(object sender, System.EventArgs e)
		{
			_cancel.Cancel();

			if (listView1.SelectedItems.Count > 0)
				DialogResult = DialogResult.OK;
		}

		private async void PingAddresses(CancellationToken token)
		{
			var ping = new Ping();
			foreach (ListViewItem item in listView1.Items)
			{
				if (token.IsCancellationRequested)
					break;
				var reply = await ping.SendPingAsync((IPAddress)item.Tag, 5000);
				item.SubItems[1].Text = reply.RoundtripTime.ToString();
			}
		}
	}
}
