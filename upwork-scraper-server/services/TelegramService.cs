using System.Threading.Tasks;
using Telegram.Bot;
using upwork_scraper_server.configuration;

namespace upwork_scraper_server.services
{
    public class TelegramService
    {
        private static string _chatId => StaticConfiguration.GetTelegramChatId();

        private static TelegramBotClient _bot => new TelegramBotClient(StaticConfiguration.GetTelegramApiKey());
        
        public async Task SendMessageAsync(string text)
        {
            await _bot.SendTextMessageAsync(_chatId, text);
        }

        public async Task SendErrorMessage()
        {
            await SendMessageAsync("There has been an error in UpworkScraperBot!");
        }
    }
}