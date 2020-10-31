using System.Text;
using Leaf.xNet;
namespace Sharpcord
{
    public class Relationship
    {
        private ulong userId;
        private string token;
        public Relationship(ulong userId, string token)
        {
            this.userId = userId;
            this.token = token;
        }
        public ulong GetUserID()
        {
            return userId;
        }
        public void Remove(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Delete($"https://discordapp.com/api/v6/users/@me/relationships/{userId}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public void Add(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Put($"https://discordapp.com/api/v6/users/@me/relationships/{userId}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public void Block(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Put($"https://discordapp.com/api/v6/users/@me/relationships/{userId}", "{" + "\"type\":\"2\"" + "}", "application/json");
        }
        public void Unblock(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Delete($"https://discordapp.com/api/v6/users/@me/relationships/{userId}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public DiscordUser GetUser()
        {
            return new DiscordUser(userId, token);
        }
    }
}