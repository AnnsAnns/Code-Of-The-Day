using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegendaryGUI
{
    public class ConfigFile
    {
        public List<ForceGameInfo> ForceLaunchGames { get; set; }
        public string InstallFolder { get; set; }

        public ConfigFile()
        {
            ForceLaunchGames = new List<ForceGameInfo>();
            InstallFolder = "";
        }
    }
    public static class Config
    {
        public static string ConfigPath { get; set; } = "./launcherConfig.json";
        public static ConfigFile config = new ConfigFile();

        public static void Read()
        {
            string json;

            try
            {
                json = File.ReadAllText(ConfigPath);
            }
            catch
            {
                return;
            }

            config = JsonSerializer.Deserialize<ConfigFile>(json);
        }

        public static void Write()
        {
            string jsonString = JsonSerializer.Serialize(config);
            File.WriteAllText(ConfigPath, jsonString);
        }
    }
}
