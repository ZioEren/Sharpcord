using Leaf.xNet;
namespace Sharpcord
{
    public static class TokenManager
    {
        public static bool IsTokenValid(string token, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            if (token.Length != 59 && token.Length != 88)
            {
                return false;
            }
            try
            {
                HttpRequest req = new HttpRequest();
                req.AddHeader("Authorization", token);
                ProxyManager.SimpleChange(req, proxy, proxyType);
                req.Get("https://discord.com/api/v6/users/@me/library");
                return true;
            }
            catch (TokenNotValidException ex)
            {
                return false;
            }
        }
    }
}