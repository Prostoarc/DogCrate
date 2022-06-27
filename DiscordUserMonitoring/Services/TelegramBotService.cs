using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DiscordUserMonitoring.Services
{
    internal class TelegramBotService : ITelegramBotService
    {
        private readonly TelegramBotClient _client;
        private readonly TelegramOptions _telegramOptions;
        public TelegramBotService(IOptions<TelegramOptions> options)
        {
            _telegramOptions = options.Value;
            _client = new TelegramBotClient(_telegramOptions.Token);
        }

        public async Task SendMessage(string messageText)
        {
            foreach (var chatId in _telegramOptions.AllowedUserIds)
            {
                await _client.SendTextMessageAsync(chatId, messageText);
            }
        }
    }
}
