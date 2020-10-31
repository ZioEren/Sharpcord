namespace Sharpcord
{
    public class DiscordClient
    {
        private string token;
        public DiscordClient()
        {
        }
        public DiscordClient(string token, bool checkToken = true, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            SetToken(token, checkToken, proxy, proxyType);
        }
        public string GetToken()
        {
            return token;
        }
        public void SetToken(string token, bool checkToken = true, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            if (checkToken)
            {
                if (TokenManager.IsTokenValid(token, proxy, proxyType))
                {
                    this.token = token;
                }
                else
                {
                    throw new TokenNotValidException();
                }
            }
            else
            {
                this.token = token;
            }
        }
        public UserClient GetUserClient()
        {
            return new UserClient(token);
        }
    }
}