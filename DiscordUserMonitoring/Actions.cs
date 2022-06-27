namespace DiscordUserMonitoring
{
    internal class Actions
    {
        private readonly IDiscordBotService _discordBotService;
        private readonly ITelegramBotService _telegramBotService;
        private readonly IEventService _eventService;

        public Actions(
            IDiscordBotService discordBotService,
            ITelegramBotService telegramBotService,
            IEventService eventService)
        {
            _discordBotService = discordBotService;
            _telegramBotService = telegramBotService;
            _eventService = eventService;

            _eventService.OnDiscordUserMoved += async (discordUser) => await SendTelegramMessage(discordUser);
        }

        public async Task RunAsync()
        {
            var list = new List<Task>();

            list.Add(Task.Run(async () => { await _discordBotService.RunAsync(); }));
            await Task.WhenAll(list);
        }

        private async Task SendTelegramMessage(DiscordUser discordUser)
        {
            await _telegramBotService.SendMessage($"{discordUser.UserName}: " +
                $"{discordUser.FromChannelName ?? "null"} -> {discordUser.ToChannelName ?? "null"}");
        }
    }
}
