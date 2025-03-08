using OrderedPropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace HttpDownloader
{
	[Serializable]
	public class ConfigFile
	{
		//Position of the window
		public ControlRect MainWindow;
		public ControlRect TaskWindow;

		//Saved configs
		public List<DownloadConfig> Configs = new List<DownloadConfig>();
		public int LastConfigIndex;
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string FFMpegPath { get; set; }
	}

	[Serializable]
	public struct ControlRect
	{
		public int X, Y, W, H;

		public ControlRect(Control c)
		{
			X = c.Location.X;
			Y = c.Location.Y;
			W = c.Width;
			H = c.Height;
		}

		public void Apply(Control c)
		{
			c.Location = new Point(X, Y);
			c.Size = new Size(W, H);
		}
	}

	public enum OverwriteMethod
	{
		Replace,
		Confirm,
		AutoRename,
	}

	[Serializable, DefaultProperty("URL"), TypeConverter(typeof(PropertySorter))]
	public class DownloadConfig
	{
		private string _host, _url;

		[Category("Main"), PropertyOrder(0)] public string Name { get; set; }
		[Category("Main"), PropertyOrder(20), Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string Save { get; set; }
		[Category("Main"), PropertyOrder(30)] public string Filename { get; set; }
		[Category("Config")] public bool Resume { get; set; } = true;
		[Category("Config")] public bool AutoRetry { get; set; } = true;
		[Category("Config")] public bool AutoDecompress { get; set; } = true;
		[Category("Config")] public bool CopyRefer { get; set; } = true;
		[Category("Config")] public bool ForceSetHost { get; set; } = true;
		[Category("Config")] public OverwriteMethod Overwrite { get; set; } = OverwriteMethod.Replace;
		[Category("Config")] public bool AutoFilename { get; set; } = true;

		[Category("Main"), PropertyOrder(1), RefreshProperties(RefreshProperties.Repaint)]
		public string URL
		{
			get { return _url; }
			set
			{
				if (value != _url)
				{
					_url = value;
					_host = null;
				}
			}
		}
		[Category("Main"), PropertyOrder(10)] public string Method { get; set; } = "GET";
		[Category("Main"), PropertyOrder(40)] public string Referer { get; set; }
		[Category("Main"), PropertyOrder(41)] public string Origin { get; set; }
		public string UserAgent { get; set; }
		public string Accept { get; set; } = "video/webm,video/ogg,video/*,application/ogg,audio/*,*/*";
		public string Connection { get; set; } = "keep-alive";

		public string Sec_Fetch_Dest { get; set; } = "video";
		public string Sec_Fetch_Mode { get; set; } = "no-cors";
		public string Sec_Fetch_Site { get; set; } = "same-origin";
		public string Pragma { get; set; } = "no-cache";
		public string Cache_Control { get; set; } = "no-cache";
		public string Cookie { get; set; }
		public bool UseCookie { get; set; }
		public bool Debug { get; set; } = false;

		[Category("Proxy")] public string Proxy { get; set; }
		[Category("Proxy")] public bool UseProxy { get; set; }
		[Category("Main"), PropertyOrder(2)] public string IP { get; set; }

		[Category("Main"), PropertyOrder(1)]
		public string Host
		{
			get
			{
				if (_host != null)
					return _host;

				if (string.IsNullOrWhiteSpace(URL))
					return null;

				var uri = new Uri(URL);
				return uri.Host;
			}

			set
			{
				_host = value;
			}
		}

		public HttpWebRequest CreateRequest()
		{
			var uri = Uri;
			var req = (HttpWebRequest)WebRequest.Create(uri);

			if (_host != null)
				req.Host = _host;
			else if (ForceSetHost)
				req.Host = Host;

			req.Method = Method ?? "GET";

			if (Referer.HasValue())
				req.Referer = Referer;
			else if (CopyRefer)
				req.Referer = URL;

			if (UserAgent.HasValue()) req.UserAgent = UserAgent;
			if (Accept.HasValue()) req.Accept = Accept;
			if (Connection == "keep-alive") req.KeepAlive = true;

			var headers = req.Headers;
			if (Sec_Fetch_Dest.HasValue()) headers.Add("Sec-Fetch-Dest", Sec_Fetch_Dest);
			if (Sec_Fetch_Mode.HasValue()) headers.Add("Sec-Fetch-Mode", Sec_Fetch_Mode);
			if (Sec_Fetch_Site.HasValue()) headers.Add("Sec-Fetch-Site", Sec_Fetch_Site);
			if (Cache_Control.HasValue()) headers.Add(HttpRequestHeader.CacheControl, Cache_Control);
			if (Pragma.HasValue()) headers.Add(HttpRequestHeader.Pragma, Pragma);
			if (Origin.HasValue()) headers.Add("Origin", Origin);

			if (AutoDecompress) req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			if (UseProxy && Proxy.HasValue()) req.Proxy = new WebProxy(Proxy);

			if (UseCookie && Cookie.HasValue())
			{
				req.CookieContainer = new CookieContainer();
				req.CookieContainer.SetCookies(uri, Cookie);
			}

			return req;
		}

		public DownloadConfig Clone()
			=> (DownloadConfig)MemberwiseClone();

		public override string ToString()
			=> string.IsNullOrEmpty(Name) ? 
			(URL ?? "Empty Config") :
			Name;

		internal Uri Uri 
			=> IP.HasValue() ?
			new UriBuilder(URL) { Host = IP }.Uri :
			new Uri(URL);
	}

	public static class StringUtils
	{
		public static bool HasValue(this string str)
		{
			return !string.IsNullOrWhiteSpace(str);
		}
	}
}
