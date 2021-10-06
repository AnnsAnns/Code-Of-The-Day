using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI
{
    public static class FormConsole
    {
        public static RichTextBox Console { get; set; }
        private delegate void SafeCallDelegate(string text);

        public static void WriteLine(string text)
        {
            if (Console.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteLine);
                Console.Invoke(d, new object[] { text });
            }
            else
            {
                Console.Text += $"{text}\n";
            }
        }
    }
}
