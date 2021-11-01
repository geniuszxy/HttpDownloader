using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
	public class DownloadConfig
	{
		public string Save;
		public bool Resume = true;

		public string URL;
		public string Method = "GET";
		public string Referer;
		public string UserAgent;
		public string Accept = "video/webm,video/ogg,video/*,application/ogg,audio/*,*/*";
		public string Connection = "keep-alive";

		public string Sec_Fetch_Dest = "video";
		public string Sec_Fetch_Mode = "no-cors";
		public string Sec_Fetch_Site = "same-origin";
		public string Pragma = "no-cache";
		public string Cache_Control = "no-cache";

		public HttpWebRequest CreateRequest()
		{
			var req = (HttpWebRequest)WebRequest.Create(URL);

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

		public DownloadConfig Clone()
		{
			return (DownloadConfig)MemberwiseClone();
		}
	}
}
