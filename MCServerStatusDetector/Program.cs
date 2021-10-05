using Discord;
using Discord.Webhook;
using MCServerStatus;
using System;
using System.Linq;
using System.Threading;

namespace MCServerStatusDetector {
    class Program {

        const string IP = "";
        const string DISCORD_WEBHOOK = "";
        const string PING_ID = "";

        static bool lastVal = false;

        static void Main(string[] args) {
            Console.WriteLine("MC Server Status Detector");

            while (true) {
                IMinecraftPinger pinger = new MinecraftPinger(IP, 25565);
                bool online = true;
                try {
                    pinger.PingAsync().Wait();
                } catch {
                    online = false;
                }

                if (online != lastVal) {
                    Notify(online);
                    lastVal = online;
                }

                Thread.Sleep(60000); //1minute
                
            }
        }

        private static void Notify(bool online) {
            using (var client = new DiscordWebhookClient(DISCORD_WEBHOOK)) {
                client.SendMessageAsync(text: PING_ID, embeds: new Embed[] { BuildDiscordEmbed(online) }).Wait();
            }
        }

        private static Embed BuildDiscordEmbed(bool online) {
            var embed = new EmbedBuilder {
                Title = "SERVER IS " + (online ? "ONLINE" : "OFFLINE"),
                Description = "SERVER IS " + (online ? "ONLINE" : "OFFLINE"),
                Timestamp = DateTime.Now
            };

            return embed.Build();
        }
    }
}
