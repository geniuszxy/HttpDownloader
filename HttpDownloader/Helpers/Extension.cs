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

		public static string FindBetween(this string s, string start, string end, int offset = 0)
		{
			int startPos = s.IndexOf(start, offset);
			if (startPos < 0)
				return null;

			int endPos = s.IndexOf(end, startPos += start.Length);
			if (endPos < 0)
				return null;

			return s.Substring(startPos, endPos - startPos);
		}
	}
}
