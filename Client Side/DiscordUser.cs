using System;
using System.Collections.Generic;
using Leaf.xNet;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;
namespace Sharpcord
{
    public class DiscordUser
    {
        private ulong userId;
        private string token;
        public DiscordUser(ulong userId, string token)
        {
            this.userId = userId;
            this.token = token;
        }
        public ulong GetUserID()
        {
            return userId;
        }
        public Relationship GetRelationship()
        {
            return new Relationship(userId, token);
        }
        public List<Relationship> GetMutualRelationships(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            List<Relationship> relationships = new List<Relationship>();
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            string lol = req.Get($"https://discordapp.com/api/v6/users/{userId}/relationships").ToString();
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
        private ulong GetChannelID(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            dynamic Resp = JsonConvert.DeserializeObject(req.Post($"https://discordapp.com/api/v6/users/@me/channels", "{" + $"\"recipient_id\": \"{userId}\"" + "}", "application/json").ToString());
            return Resp.id;
        }
        public void SetNote(string note, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Put($"https://discordapp.com/api/v6/users/@me/notes/{userId}", "{" + $"\"note\":\"{note}\"" + "}", "application/json");
        }
        public DMChannel GetDMChannel(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            return new DMChannel(GetChannelID(proxy, proxyType), token);
        }
        private string GetBruteUser(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            return req.Get($"https://discord.com/api/v6/users/{userId}/profile").ToString();
        }
        public string GetUsername(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteUser(proxy, proxyType));
            return Resp.user.username;
        }
        public string GetAvatarHash(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteUser(proxy, proxyType));
            return Resp.user.avatar;
        }
        public string GetAvatarLink(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            return "https://cdn.discordapp.com/avatars/" + userId + "/" + GetAvatarHash(proxy, proxyType) + ".jpg";
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
        public uint GetDiscriminator(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteUser(proxy, proxyType));
            return Resp.user.discriminator;
        }
        public uint GetFlags(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteUser(proxy, proxyType));
            return Resp.user.flags;
        }
        public uint GetPublicFlags(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteUser(proxy, proxyType));
            return Resp.user.public_flags;
        }
    }
}