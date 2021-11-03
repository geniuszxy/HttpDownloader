using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HttpDownloader
{
	static class Program
	{
		const string PIPE_NAME = "geniuszxy.HttpDownloader";
		static MainForm _mainForm;

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				if (!InitArgs(args))
					return;

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(_mainForm = new MainForm(args));
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		static bool InitArgs(string[] args)
		{
			//There is an instance
			if (!CreatePipe())
			{
				//Try send args to the instance
				if (args.Length > 0)
				{
					var pipeClient = new NamedPipeClientStream(".", PIPE_NAME, PipeDirection.Out);
					try
					{
						pipeClient.Connect(100);
						using (var sw = new StreamWriter(pipeClient, Encoding.UTF8))
						{
							//Send only the first argument
							sw.Write(args[0]);
							sw.Flush();
							pipeClient.WaitForPipeDrain();
						}

						//Continue task by another instance
						return false;
					}
					catch (TimeoutException)
					{
						//Send failed, continue with this program
						return true;
					}
				}
				//Nothing to send
				else
					return true;
			}
			//No instance
			else
			{
				return true;
			}
		}

		//Create a pipe server
		static bool CreatePipe()
		{
			NamedPipeServerStream pipeServer = null;
			try
			{
				pipeServer = new NamedPipeServerStream(PIPE_NAME, PipeDirection.In, 1);
			}
			catch(IOException)
			{
				return false;
			}

			if (pipeServer != null)
			{
				return ThreadPool.QueueUserWorkItem(ReadPipeMessages, pipeServer);
			}

			return false;
		}

		//Read message
		static void ReadPipeMessages(object state)
		{
			var pipeServer = state as NamedPipeServerStream;
			if (pipeServer == null)
				return;

			do
			{
				pipeServer.WaitForConnection();
				using (var sr = new StreamReader(pipeServer, Encoding.UTF8, true, 1024, true))
				{
					var data = sr.ReadToEnd();
					_mainForm?.CreateTask(data);
				}
				pipeServer.Disconnect();
			}
			while (true);
		}
	}
}
