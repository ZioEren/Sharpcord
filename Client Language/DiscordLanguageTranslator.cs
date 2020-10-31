using System;
namespace Sharpcord
{
    public static class DiscordLanguageTranslator
    {
        private static string[] languages = { "da", "de", "en", "es", "fr", "hr", "it", "lt", "hu", "nl", "no", "pl", "pt", "ro", "fi", "sv", "vi", "tr", "cs", "el", "bg", "ru", "uk", "hi", "th", "zh", "jp", "kr" };
        public static DiscordLanguage GetLanguageFromLocale(string locale)
        {
            if (locale.Length > 2)
            {
                return DiscordLanguage.None;
            }
            for (int i = 0; i < languages.Length; i++)
            {
                if (languages[i] == locale)
                {
                    return (DiscordLanguage)Enum.ToObject(typeof(DiscordLanguage), i);
                }
            }
            return DiscordLanguage.None;
        }
        public static string GetLocaleFromLanguage(DiscordLanguage language)
        {
            try
            {
                return languages[(int)language];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}