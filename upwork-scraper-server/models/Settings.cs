namespace upwork_scraper_server.models
{
    public class Settings
    {
        public string Cookie { get; set; }
        public bool Active { get; set; }
        public string TelegramApiKey { get; set; }
        public string TelegramChatId { get; set; }
        public string Engagement { get; set; }
    }
}