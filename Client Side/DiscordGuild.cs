using Leaf.xNet;
using System.Text;
namespace Sharpcord
{
    public class DiscordGuild
    {
        private ulong guildId;
        private string token;
        public DiscordGuild(ulong guildId, string token)
        {
            this.guildId = guildId;
            this.token = token;
        }
        public ulong GetGuildID()
        {
            return guildId;
        }
        public void Leave(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Delete($"https://discordapp.com/api/v6/users/@me/guilds/{guildId}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public void Delete(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post($"https://discordapp.com/api/v6/guilds/{guildId}/delete", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public void Mute(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post($"https://discord.com/api/v6/users/@me/guilds/{guildId}/settings", "{\"muted\":true,\"mute_config\":{\"selected_time_window\":-1,\"end_time\":null}}", "application/json");
        }
        public void Unmute(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post($"https://discord.com/api/v6/users/@me/guilds/{guildId}/settings", "{\"muted\":\"false\"}", "application/json");
        }
    }
}