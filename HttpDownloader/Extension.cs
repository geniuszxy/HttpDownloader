using System;
using System.Windows.Forms;

namespace HttpDownloader
{
	static class Extension
	{
		public static void TryInvoke(this Control c, Action a)
		{
			if(c.InvokeRequired)
				c.BeginInvoke(a);
			else
				a();
		}

		public static void TryInvoke<T>(this Control c, Action<T> a, T p)
		{
			if (c.InvokeRequired)
				c.BeginInvoke(a, p);
			else
				a(p);
		}

		public static void TryInvoke<T1, T2>(this Control c, Action<T1, T2> a, T1 p1, T2 p2)
		{
			if (c.InvokeRequired)
				c.BeginInvoke(a, p1, p2);
			else
				a(p1, p2);
		}
	}
}
