using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryGUI
{
    public abstract class GameInfoBase
    {
        public string Name { get; set; }
        public string AppName { get; set; }
        public string Version { get; set; }
        public string Size { get; set; }
        public bool Update { get; set; }
    }

    public class GameInfo : GameInfoBase
    {
        public GameInfo(string inLine)
        {
            Parse(inLine);
        }

        private void Parse(string inLine)
        {
            string noStars = inLine.Replace('*', ' ');
            string noSpaces = noStars.Trim().Replace("(Test branch)", "");
            // FormConsole.WriteLine(noSpaces);
            int indexOfLB = noSpaces.IndexOf('(');
            Name = noSpaces.Substring(0, indexOfLB).Trim();
            string args = noSpaces.Substring(indexOfLB + 1, noSpaces.IndexOf(')') - 1 - indexOfLB);
            string[] argsSplit = args.Split('|');
            AppName = argsSplit[0].Substring(argsSplit[0].IndexOf(':') + 1).Trim();
            Version = argsSplit[1].Substring(argsSplit[1].IndexOf(':') + 1).Trim();
            if (argsSplit.Length > 2)
                Size = argsSplit[2].Substring(argsSplit[2].IndexOf(':') + 1).Trim();
        }
    }
    public class ForceGameInfo : GameInfoBase
    {
        public string GamePath { get; set; }

        public ForceGameInfo(string name, string appName, string version, string size, string path)
        {
            Name = name;
            AppName = appName;
            Version = version;
            Size = size;
            GamePath = path;
        }
        public ForceGameInfo()
        { }
    }
}
