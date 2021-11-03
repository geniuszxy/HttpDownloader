using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms.Design;

namespace HttpDownloader
{
	[Serializable]
	public class ConfigFile
	{
		//Position of the window
		public int X, Y, W, H;

		//Saved configs
		public List<DownloadConfig> Configs = new List<DownloadConfig>();
		public int LastConfigIndex;
	}

	[Serializable]
	[DefaultProperty("URL")]
	public class DownloadConfig
	{
		private string _host, _url;

		[Category("Main")]
		[Editor(typeof(FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Save { get; set; }
		public bool Resume { get; set; } = true;

		[Category("Main")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public string URL
		{
			get { return _url; }
			set
			{
				if(value != _url)
				{
					_url = value;
					_host = null;
				}
			}
		}
		[Category("Main")]
		public string Method { get; set; } = "GET";
		[Category("Main")]
		public string Referer { get; set; }
		public string UserAgent { get; set; }
		public string Accept { get; set; } = "video/webm,video/ogg,video/*,application/ogg,audio/*,*/*";
		public string Connection { get; set; } = "keep-alive";

		public string Sec_Fetch_Dest { get; set; } = "video";
		public string Sec_Fetch_Mode { get; set; } = "no-cors";
		public string Sec_Fetch_Site { get; set; } = "same-origin";
		public string Pragma { get; set; } = "no-cache";
		public string Cache_Control { get; set; } = "no-cache";

		[Category("Main")]
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
		}

		public HttpWebRequest CreateRequest()
		{
			var req = (HttpWebRequest)WebRequest.Create(URL);

			if (_host != null)
				req.Host = _host;

			req.Method = Method ?? "GET";
			if (Referer != null) req.Referer = Referer;
			if (UserAgent != null) req.UserAgent = UserAgent;
			if (Accept != null) req.Accept = Accept;
			if (Connection == "keep-alive") req.KeepAlive = true;

			var headers = req.Headers;
			if (Sec_Fetch_Dest != null) headers.Add("Sec-Fetch-Dest", Sec_Fetch_Dest);
			if (Sec_Fetch_Mode != null) headers.Add("Sec-Fetch-Mode", Sec_Fetch_Mode);
			if (Sec_Fetch_Site != null) headers.Add("Sec-Fetch-Site", Sec_Fetch_Site);
			if (Cache_Control != null) headers.Add(HttpRequestHeader.CacheControl, Cache_Control);
			if (Pragma != null) headers.Add(HttpRequestHeader.Pragma, Pragma);

			req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			return req;
		}

		internal void SetIPAddress(IPAddress addr)
		{
			var uri = new UriBuilder(URL);
			var host = uri.Host;
			uri.Host = addr.ToString();
			URL = uri.ToString(); //change _host
			_host = host; //replace with the real host
		}

		public DownloadConfig Clone()
		{
			return (DownloadConfig)MemberwiseClone();
		}

		public override string ToString()
		{
			return URL ?? "Empty Config";
		}
	}
}
