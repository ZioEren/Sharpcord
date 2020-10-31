using Leaf.xNet;
using Newtonsoft.Json;
using System.Text;
namespace Sharpcord
{
    public class ClientSettings
    {
        private string token;
        public ClientSettings(string token)
        {
            this.token = token;
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
        public string GetLocale(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            return Resp.locale;
        }
        public DiscordLanguage GetLanguage(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteInfo(proxy, proxyType));
            string locale = Resp.locale;
            return DiscordLanguageTranslator.GetLanguageFromLocale(locale);
        }
        public string GetStringLanguage(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            return GetLanguage(proxy, proxyType).ToString();
        }
        public void SetDeveloperMode(bool mode, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"developer_mode\":" + $"\"{mode.ToString().ToLower()}\"" + "}", "application/json");
        }
        public void SetDisplayCompact(bool compact, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"message_display_compact\":" + $"\"{compact.ToString().ToLower()}\"" + "}", "application/json");
        }
        public void SetDiscordTheme(DiscordTheme theme, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            if (theme == DiscordTheme.Dark)
            {
                req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"theme\":\"dark\"}", "application/json");
            }
            else
            {
                req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"theme\":\"light\"}", "application/json");
            }
        }
        public void SetLocale(string locale, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"locale\":" + $"\"{locale}\"" + "}", "application/json");
        }
        public void SetLanguage(DiscordLanguage language, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"locale\":" + $"\"{DiscordLanguageTranslator.GetLocaleFromLanguage(language)}\"" + "}", "application/json");
        }
        public void SetShowCurrentGame(bool show, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"show_current_game\":" + $"\"{show.ToString().ToLower()}\"" + "}", "application/json");
        }
        public DiscordTheme GetDiscordTheme(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            string theme = Resp.theme;
            if (theme == "light")
            {
                return DiscordTheme.Light;
            }
            return DiscordTheme.Dark;
        }
        public bool CanShowCurrentGame(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.show_current_game;
        }
        public bool IsDisplayCompact(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.message_display_compact;
        }
        public bool IsDeveloperModeEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.developer_mode;
        }
        public void SetTTSCommandEnabled(bool enabled, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"enable_tts_command\":" + $"\"{enabled.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsTTSCommandEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.enable_tts_command;
        }
        public void SetRenderEmbeds(bool render, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"render_embeds\":" + $"\"{render.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool CanRenderEmbeds(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.render_embeds;
        }
        public void SetRenderReactions(bool render, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"render_reactions\":" + $"\"{render.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool CanRenderReactions(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.render_reactions;
        }
        public void SetGIFAutoPlay(bool auto, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"gif_auto_play\":" + $"\"{auto.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool CanAutoPlayGIF(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.gif_auto_play;
        }
        public void SetContactAutoSyncEnabled(bool enabled, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"contact_sync_enabled\":" + $"\"{enabled.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsContactAutoSyncEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.contact_sync_enabled;
        }
        public void SetConvertEmoticonsEnabled(bool enabled, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"convert_emoticons\":" + $"\"{enabled.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsConvertEmoticonsEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.convert_emoticons;
        }
        public void SetDetectPlatformAccountsEnabled(bool detect, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"detect_platform_accounts\":" + $"\"{detect.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsDetectPlatformAccountsEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.detect_platform_accounts;
        }
        public void SetNativePhoneIntegrationEnabled(bool enabled, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"native_phone_integration_enabled\":" + $"\"{enabled.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsNativePhoneIntegrationEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.native_phone_integration_enabled;
        }
        public void AllowAccessibilityDetection(bool allowed, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"allow_accessibility_detection\":" + $"\"{allowed.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsAccessibilityDetectionAllowed(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.allow_accessibility_detection;
        }
        public void SetAFKTimeout(int timeout, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"afk_timeout\":" + $"\"{timeout.ToString().ToLower()}\"" + "}", "application/json");
        }
        public int GetAFKTimeout(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.afk_timeout;
        }
        public void SetClientPrivacy(ClientPrivacy privacy, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            string lol = "0";
            if (privacy == ClientPrivacy.GoodFriends)
            {
                lol = "1";
            }
            else if (privacy == ClientPrivacy.ScanEverything)
            {
                lol = "2";
            }
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"explicit_content_filter\":" + $"\"{lol}\"" + "}", "application/json");
        }
        public ClientPrivacy GetClientPrivacy(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            int lol = Resp.explicit_content_filter;
            if (lol == 0)
            {
                return ClientPrivacy.NoScan;
            }
            else if (lol == 1)
            {
                return ClientPrivacy.GoodFriends;
            }
            else
            {
                return ClientPrivacy.ScanEverything;
            }
        }
        public void SetStreamNotificationsEnabled(bool enabled, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"stream_notifications_enabled\":" + $"\"{enabled.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool AreStreamNotificationEnabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.stream_notifications_enabled;
        }
        public void DisableGamesTab(bool disable, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"disable_games_tab\":" + $"\"{disable.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool IsGamesTabDisabled(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.disable_games_tab;
        }
        public void SetEmojisAnimated(bool animated, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"animate_emoji\":" + $"\"{animated.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool AreEmojisAnimated(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.animate_emoji;
        }
        public void SetInlineAttachmentMedia(bool inline, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"inline_attachment_media\":" + $"\"{inline.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool AreMediaAttachmentInline(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.inline_attachment_media;
        }
        public void SetInlineEmbedMedia(bool inline, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"inline_embed_media\":" + $"\"{inline.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool AreMediaEmbedInline(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.inline_embed_media;
        }
        public void SetRestrictGuilds(bool restrict, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"default_guilds_restricted\":" + $"\"{restrict.ToString().ToLower()}\"" + "}", "application/json");
        }
        public bool AreGuildsRestricted(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.default_guilds_restricted;
        }

        public void SetTimezoneOffset(int offset, string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            HttpRequest req = new HttpRequest();
            req.AddHeader("Authorization", token);
            ProxyManager.SimpleChange(req, proxy, proxyType);
            req.Patch($"https://discordapp.com/api/v6/users/@me/settings", "{\"timezone_offset\":" + $"\"{offset.ToString().ToLower()}\"" + "}", "application/json");
        }
        public int GetTimezoneOffset(string proxy = "", ProxyType proxyType = ProxyType.HTTPS)
        {
            dynamic Resp = JsonConvert.DeserializeObject(GetBruteSettings(proxy, proxyType));
            return Resp.timezone_offset;
        }
    }
}