using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpDownloader
{
	class M3U8Downloader : Downloader
	{

	}

	class M3U8DownloadProcess : Process
	{
		private string _lastLine = string.Empty;
		private double _duration = -1.0;
		private double _progress;

		public M3U8DownloadProcess(string ffmpeg, string url, string output, string proxy)
		{
			var si = StartInfo;

			//proxy
			var proxyArg = "";
			if (!string.IsNullOrEmpty(proxy))
			{
				proxyArg = $" -http_proxy {proxy}";
				si.Environment.Add("http_proxy", proxy);
			}

			si.FileName = ffmpeg;
			si.Arguments = $@"-hide_banner -protocol_whitelist " +
				@"""file,http,https,tcp,tls,crypto,httpproxy""" +
				$@"{proxyArg} -i ""{url}"" -c copy ""{output}""";
			si.UseShellExecute = false;
			si.RedirectStandardOutput = true;
			si.RedirectStandardError = true;

			OutputDataReceived += OnOutput;
			ErrorDataReceived += OnError;
			Exited += OnExited;
		}

		public void StartProcess()
		{
			EnableRaisingEvents = true;
			Start();
			BeginOutputReadLine();
			BeginErrorReadLine();
		}

		private void OnExited(object sender, EventArgs e)
		{
		}

		private void OnError(object sender, DataReceivedEventArgs e)
		{
			_lastLine = e.Data;
		}

		private void OnOutput(object sender, DataReceivedEventArgs e)
		{
			var text = _lastLine = e.Data;

			if (_duration < 0.0)
			{
				var dur = text.FindBetween("Duration: ", ",");
				if (dur != null)
					_duration = TimeSpanString2Double(dur);
			}
			else
			{
				var time = text.FindBetween("time=", " ");
				if (time != null)
				{
					var tt = TimeSpanString2Double(time);
					_progress = tt / _duration;
				}
			}
		}

		private static double TimeSpanString2Double(string text)
		{
			var ts = TimeSpan.ParseExact(text, "h:m:s.ff", null);
			return ts.TotalSeconds;
		}

		public string LastLine => _lastLine;
		public double Progress => _progress;
	}
}
