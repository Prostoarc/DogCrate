namespace DiscordUserMonitoring.Configs
{
    internal class TelegramOptions
    {
        public string Token { get; set; } = null!;
        public List<long> AllowedUserIds { get; set; } = null!;
    }
}
