using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI.GameList
{
    public class InstalledGamesLister : GamesLister
    {
        public InstalledGamesLister(ListViewSafeWriter lv)
            : base(lv)
        {
            args = "list-installed --check-updates";
        }

        public override void ListWriter()
        {
            lv.Clear();

            foreach (GameInfo info in Games)
            {
                ListViewItem li = new ListViewItem(info.Name);
                li.SubItems.Add(info.AppName);
                li.SubItems.Add(info.Version);
                li.SubItems.Add(info.Size);
                li.SubItems.Add(info.Update ? "Yes" : "No");
                lv.Add(li);
            }
        }
    }
}
