using Leaf.xNet;
using System;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Sharpcord
{
    public class UserClient
    {
        private string token;
        public UserClient(string token)
        {
            this.token = token;
        }
        public DMChannel GetDMChannel(ulong channelId)
        {
            return new DMChannel(channelId, token);
        }
        public void ClaimNitroCode(string code, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            NitroManager.ClaimCode(code, token, proxy, proxyType);
        }
        public void JoinGuild(string code, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post("https://discordapp.com/api/v6/invite/" + GetInviteCodeByInviteLink(code));
        }
        private string GetInviteCodeByInviteLink(string inviteLink)
        {
            try
            {
                if (inviteLink.EndsWith("/"))
                {
                    inviteLink = inviteLink.Substring(0, inviteLink.Length - 1);
                }
                if (inviteLink.Contains("discord") && inviteLink.Contains("/") && inviteLink.Contains("http"))
                {
                    string[] splitter = Strings.Split(inviteLink, "/");
                    return splitter[splitter.Length - 1];
                }
            }
            catch (Exception ex)
            {
            }
            return inviteLink;
        }
        public string GetEmail()
        {
            return "";
        }
        private string GetBruteInfo(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            return req.Get($"https://discord.com/api/v6/users/@me").ToString();
        }
        private string GetBruteSettings(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            return req.Get($"https://discordapp.com/api/v6/users/@me/settings").ToString();
        }
        public ulong GetID(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.id;
        }
        public string GetEmail(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.email;
        }
        public string GetUsername(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.username;
        }
        public uint GetDiscriminator(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.discriminator;
        }
        public uint GetFlags(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.flags;
        }
        public bool IsVerified(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.verified;
        }
        public bool IsMfaEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.mfa_enabled;
        }
        public bool IsNsfwAllowed(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.nsfw_allowed;
        }
        public string GetPhoneNumber(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.phone;
        }
        public uint GetPublicFlags(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.public_flags;
        }
        public string GetAvatarHash(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.avatar;
        }
        public string GetAvatarLink(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            return "https://cdn.discordapp.com/avatars/" + GetID(proxy, proxyType) + "/" + GetAvatarHash(proxy, proxyType) + ".jpg";
        }
        public Image GetAvatar(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            return (Bitmap)((new ImageConverter()).ConvertFrom(req.Get(GetAvatarLink(proxy, proxyType)).ToBytes()));
        }
        public byte[] GetAvatarBytes(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            return req.Get(GetAvatarLink(proxy, proxyType)).ToBytes();
        }
        public MemoryStream GetAvatarStream(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            return new MemoryStream(GetAvatarBytes(proxy, proxyType));
        }
        public ClientSettings GetClientSettings()
        {
            return new ClientSettings(token);
        }
        public void RemoveEmailVerification(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Get($"https://discord.com/api/v6/guilds/0/members");
        }
        public void SetOnlineStatus(OnlineStatus status, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            string stat = "online";
            if (status == OnlineStatus.DoNotDisturb)
            {
                stat = "dnd";
            }
            else if (status == OnlineStatus.Idle)
            {
                stat = "idle";
            }
            else if (status == OnlineStatus.Invisible)
            {
                stat = "invisible";
            }
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"status\":" + $"\"{stat}\"" + "}", "application/json");
        }
        public OnlineStatus GetOnlineStatus(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            string status = Resp.status;
            if (status == "online")
            {
                return OnlineStatus.Online;
            }
            else if (status == "dnd")
            {
                return OnlineStatus.DoNotDisturb;
            }
            else if (status == "idle")
            {
                return OnlineStatus.Idle;
            }
            else
            {
                return OnlineStatus.Invisible;
            }
        }
        public void SetUsername(string username, string password, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me", "{" + $"\"username\":\"{username}\",\"email\":\"{GetEmail()}\",\"password\":\"{password}\",\"discriminator\":\"{GetDiscriminator().ToString()}\"" + "}", "application/json");
        }
        public void SetDiscriminator(uint discriminator, string password, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me", "{" + $"\"username\":\"{GetUsername()}\",\"email\":\"{GetEmail()}\",\"password\":\"{password}\",\"discriminator\":\"{discriminator.ToString()}\"" + "}", "application/json");
        }
        public void SetEmail(string email, string password, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me", "{" + $"\"username\":\"{GetUsername()}\",\"email\":\"{email}\",\"password\":\"{password}\",\"discriminator\":\"{GetDiscriminator().ToString()}\"" + "}", "application/json");
        }
        public void ChangeProfile(string username, uint discriminator, string email, string password, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me", "{" + $"\"username\":\"{username}\",\"email\":\"{email}\",\"password\":\"{password}\",\"discriminator\":\"{discriminator.ToString()}\"" + "}", "application/json");
        }
        public void AddFriend(ulong id, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Put($"https://discordapp.com/api/v6/users/@me/relationships/{id}", Encoding.ASCII.GetBytes("{}"), "application/json");
        }
        public void AddFriend(string username, uint discriminator, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Post($"https://discordapp.com/api/v6/users/@me/relationships", $"{{\"username\":\"{username}\",\"discriminator\":{discriminator.ToString()}}}", "application/json");
        }
        public void AddFriend(string completeUsername, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            if (!completeUsername.Contains("#"))
            {
                return;
            }
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            string[] splitter = Strings.Split(completeUsername, "#");
            string username = splitter[0];
            string discriminator = splitter[1];
            req.Post($"https://discordapp.com/api/v6/users/@me/relationships", $"{{\"username\":\"{username}\",\"discriminator\":{discriminator}}}", "application/json");
        }
        public List<Relationship> GetRelationships(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            List<Relationship> relationships = new List<Relationship>();
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            string lol = req.Get($"https://discordapp.com/api/v6/users/@me/relationships").ToString();
            string[] splitter = Strings.Split(lol, @"{""id"": """);
            bool can = false;
            for (int i = 0; i < splitter.Length; i++)
            {
                if (!can)
                {
                    string[] otherSplitter = Strings.Split(splitter[i], @"""");
                    string stringId = otherSplitter[0].Replace("[", "").Replace(" ", "").Replace("]", "").Replace("{", "}").Replace(@"""", "").Replace(Constants.vbTab, "");
                    if (stringId.Replace(" ", "") != "")
                    {
                        try
                        {
                            relationships.Add(new Relationship(ulong.Parse(stringId), token));
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                if (!can)
                {
                    can = true;
                }
                else
                {
                    can = false;
                }
            }
            return relationships;
        }
        public DiscordUser GetUser(ulong userId)
        {
            return new DiscordUser(userId, token);
        }
        public Relationship GetRelationship(ulong userId)
        {
            return new Relationship(userId, token);
        }
        public List<DiscordGuild> GetGuilds(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            List<DiscordGuild> guilds = new List<DiscordGuild>();
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            string lol = req.Get($"https://discordapp.com/api/v6/users/@me/guilds").ToString();
            string[] splitter = Strings.Split(lol, @"""id"": ");
            for (int i = 0; i < splitter.Length; i++)
            {
                if (i != 0)
                {
                    guilds.Add(new DiscordGuild(ulong.Parse(splitter[i].Substring(0, 20).Replace(@"""", "")), token));
                }
            }
            return guilds;
        }
        public DiscordGuild GetGuild(ulong guildId)
        {
            return new DiscordGuild(guildId, token);
        }
    }
}