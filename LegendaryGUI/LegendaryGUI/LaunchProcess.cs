using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI
{
    public class LaunchProcess
    {
        public string Arguments { get; set; }
        public List<string> Output { get; private set; }
        public List<string> ErrOutput { get; private set; }
        public Return ReturnFunc { get; set; }
        public bool HasProcessFinished { get; private set; } = false;
        public bool WaitOnExit { get; set; } = false;

        private static bool isRunning = false;
        private Process proc;

        private delegate void SafeCallDelegate(string text);
        public delegate void Return(LaunchProcess ret);

        public LaunchProcess(string args)
        {
            Arguments = args;
            Output = new List<string>();
            ErrOutput = new List<string>();
            HasProcessFinished = false;
            WaitOnExit = false;
        }

        public virtual bool CheckRunStatus()
        {
            return isRunning;
        }

        public virtual void WriteRunStatus(bool status)
        {
            isRunning = status;
        }

        public int Run()
        {
            if (CheckRunStatus())
            {
                MessageBox.Show("Current thread is busy!");
                HasProcessFinished = true;
                return -1;
            }

            WriteRunStatus(true);

            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = Arguments;
            start.FileName = "legendary";
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            start.UseShellExecute = false;

            proc = new Process();
            proc.StartInfo = start;
            FormConsole.WriteLine($"CONSOLE> {start.FileName} {Arguments}");
            var runThread = new Thread(RunInThread);
            runThread.Start();

            if (WaitOnExit)
                runThread.Join();

            return 0;
        }

        private void GetErrOutput()
        {
            while (true)
            {
                string line = proc.StandardError.ReadLine();
                if (line == null)
                    break;
                ErrOutput.Add(line);
                FormConsole.WriteLine(line);
            }
        }

        private void RunInThread()
        {
            bool check = true;
            try
            {
                proc.Start();
            }
            catch
            {
                check = false;
            }
            
            if (check)
            {
                var errTread = new Thread(GetErrOutput);
                errTread.Start();

                while (true)
                {
                    string line = proc.StandardOutput.ReadLine();
                    if (line == null)
                        break;
                    Output.Add(line);
                    FormConsole.WriteLine(line);
                }

                errTread.Join();
                proc.WaitForExit();
            }

            if (!check)
            {
                MessageBox.Show("Legendary seems to not be installed");
            }
            else if (proc.ExitCode != 0)
            {
                MessageBox.Show("Legendary call exited unexpectedly. See console for details");
            }
            else
            {
                ReturnFunc?.Invoke(this);
            }
            WriteRunStatus(false);
            HasProcessFinished = true;
        }
    }

    public class LaunchProcessMT : LaunchProcess
    {
        public override bool CheckRunStatus()
        {
            return false;
        }

        public override void WriteRunStatus(bool status)
        {
            return;
        }

        public LaunchProcessMT(string args)
            : base(args)
        { }
    }
}
