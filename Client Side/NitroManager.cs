using System;
using Microsoft.VisualBasic;
using Leaf.xNet;
using System.Text;

namespace Sharpcord
{
    public static class NitroManager
    {
        public static bool IsCodeValid(string code, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            try
            {
                HttpRequest req = new HttpRequest();
                ProxyManager.SimpleChange(req, proxy, proxyType);
                req.Get($"https://discord.com/api/v7/entitlements/gift-codes/" + CorrectNitroCode(code));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }         
        }
        public static bool ClaimCode(string code, string token, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            try
            {
                HttpRequest req = new HttpRequest();
                ProxyManager.SimpleChange(req, proxy, proxyType);
                req.Post($"https://discordapp.com/api/v6/entitlements/gift-codes/" + CorrectNitroCode(code) + "/redeem", Encoding.ASCII.GetBytes("{}"), "application/json");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static string CorrectNitroCode(string code)
        {
            string newCode = "";
            string content = code.Replace(" ", "").Replace(Environment.NewLine, "").Replace(Constants.vbTab, "").ToLower();
            if (content.Contains("discordapp.com/gifts/"))
            {
                string[] splitter = Strings.Split(content, "discordapp.com/gifts/");
                newCode = splitter[1].Replace("/", "").Replace(" ", "").Replace(Constants.vbTab, "").Substring(0, 16);
            }
            else if (content.Contains("discord.gift/"))
            {
                string[] splitter = Strings.Split(content, "discord.gift/");
                newCode = splitter[1].Replace("/", "").Replace(" ", "").Replace(Constants.vbTab, "").Substring(0, 16);
            }
            return newCode;
        }
    }
}