using System;
using Microsoft.Extensions.Configuration;

namespace upwork_scraper_server.configuration
{
    public class StaticConfiguration
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public static T GetValue<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }
        
        public static string GetTelegramApiKey()
        {
            return Environment.GetEnvironmentVariable("TELEGRAM_API_KEY");
        }

        public static string GetTelegramChatId()
        {
            return Environment.GetEnvironmentVariable("TELEGRAM_CHAT_ID");
        }
    }
}