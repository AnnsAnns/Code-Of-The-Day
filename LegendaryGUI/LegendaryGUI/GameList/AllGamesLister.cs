using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI.GameList
{
    public class AllGamesLister : GamesLister
    {
        public AllGamesLister(ListViewSafeWriter lv)
            : base(lv)
        {
            args = "list-games";
        }

        public override void ListWriter()
        {
            lv.Clear();

            foreach (GameInfo info in Games)
            {
                ListViewItem li = new ListViewItem(info.Name);
                li.SubItems.Add(info.AppName);
                li.SubItems.Add(info.Version);
                lv.Add(li);
            }
        }
    }
}
