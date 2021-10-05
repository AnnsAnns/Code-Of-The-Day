using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI.GameList
{
    public class NotInstalledGamesLister
    {
        public List<GameInfo> Games { get; protected set; }
        private GamesLister all;
        private GamesLister installed;
        private ListViewSafeWriter lv;

        public NotInstalledGamesLister(GamesLister all, GamesLister installed, ListViewSafeWriter lv)
        {
            this.all = all;
            this.installed = installed;
            this.lv = lv;
        }

        public void RefreshListing()
        {
            Thread thread = new Thread(Calculate);
            thread.Start();
        }

        public void Calculate()
        {
            if (all.Games == null)
            {
                LaunchProcess proc = new LaunchProcess(all.args);
                proc.ReturnFunc = all.Parser;
                proc.WaitOnExit = true;

                if (proc.Run() < 0)
                    return;
            }

            if (installed.Games == null)
            {
                LaunchProcess proc = new LaunchProcess(installed.args);
                proc.ReturnFunc = installed.Parser;
                proc.WaitOnExit = true;

                if (proc.Run() < 0)
                    return;
            }

            Games = new List<GameInfo>();

            foreach (var allItem in all.Games)
            {
                bool found = false;

                foreach (var installedItem in installed.Games)
                {
                    if (allItem.AppName == installedItem.AppName)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Games.Add(allItem);
                }
            }

            ListWriter();
        }

        public void ListWriter()
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
