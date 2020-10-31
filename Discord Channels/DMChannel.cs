using Leaf.xNet;
using System.Text;
namespace Sharpcord
{
    public class DMChannel
    {
        private ulong channelId;
        private string token;
        public DMChannel(ulong channelId, string token)
        {
            this.channelId = channelId;
            this.token = token;
        }
        public ulong GetChannelID()
        {
            return channelId;
        }
        public void Mute(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discord.com/api/v6/users/@me/guilds/%40me/settings", "{\"channel_overrides\":{\"" + channelId.ToString() + "\":{\"muted\":\"true\"}}}", "application/json");
        }
        public void Unmute(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discord.com/api/v6/users/@me/guilds/%40me/settings", "{\"channel_overrides\":{\"" + channelId.ToString() + "\":{\"muted\":\"false\"}}}", "application/json");
        }
        public void SendMessage(string message, bool isTTS = false, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post($"https://discordapp.com/api/v6/channels/{channelId}/messages", "{\"content\":\"" + message + "\", \"tts\": " + isTTS.ToString().ToLower() + "}", "application/json");
        }
        public void CloseDM(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Delete($"https://discordapp.com/api/v6/channels/{channelId}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
    }
}