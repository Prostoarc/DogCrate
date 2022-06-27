namespace DiscordUserMonitoring.Services.Abstractions
{
    internal interface ITelegramBotService
    {
        public Task SendMessage(string messageText);
    }
}
