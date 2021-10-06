using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI
{
    public class ListViewSafeWriter
    {
        public ListView ListView { get; private set; }
        private delegate void SafeCallAdd(ListViewItem item);
        private delegate void SafeCall();

        public ListViewSafeWriter(ListView lv)
        {
            ListView = lv;
        }

        public void Add(ListViewItem item)
        {
            if (ListView.InvokeRequired)
            {
                var d = new SafeCallAdd(Add);
                ListView.Invoke(d, new object[] { item });
            }
            else
            {
                ListView.Items.Add(item);
            }
        }
        public void Clear()
        {
            if (ListView.InvokeRequired)
            {
                var d = new SafeCall(Clear);
                ListView.Invoke(d);
            }
            else
            {
                ListView.Items.Clear();
            }
        }
    }
}
