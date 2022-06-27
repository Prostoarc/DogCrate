namespace DiscordUserMonitoring.Services
{
    internal class DiscordBotService : IDiscordBotService
    {
        private readonly IEventService _eventService;
        private readonly DiscordSocketClient _client;
        private readonly DiscordOptions _discordOptions;

        public DiscordBotService(
            IOptions<DiscordOptions> options,
            IEventService eventService)
        {
            _discordOptions = options.Value;
            _eventService = eventService;

            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = _discordOptions.AlwaysDownloadUsers,
                MessageCacheSize = _discordOptions.MessageCacheSize
            };

            _client = new DiscordSocketClient(config);
        }

        public async Task RunAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _discordOptions.Token);
            await _client.StartAsync();

            _client.Ready += () =>
            {
                Console.WriteLine($"Start listening for Dog Crate");
                return Task.CompletedTask;
            };

            _client.UserVoiceStateUpdated += async (arg1, arg2, arg3) => await OnUserJoin(arg1, arg2, arg3);
            await Task.Delay(-1);
        }

        private Task OnUserJoin(SocketUser socketUser, SocketVoiceState socketVoiceState, SocketVoiceState socketVoiceState2)
        {
            if (socketVoiceState.VoiceChannel?.Id != socketVoiceState2.VoiceChannel?.Id
                && socketVoiceState.VoiceChannel?.Id == null
                && _discordOptions.ObservedUserIds.Contains(socketUser.Id))
            {
                var discordUser = new DiscordUser
                {
                    UserName = socketUser.Username,
                    FromChannelName = socketVoiceState.VoiceChannel?.Name,
                    ToChannelName = socketVoiceState2.VoiceChannel?.Name
                };

                _eventService.DiscordUserMoved(discordUser);
            }

            return Task.CompletedTask;
        }
    }
}
