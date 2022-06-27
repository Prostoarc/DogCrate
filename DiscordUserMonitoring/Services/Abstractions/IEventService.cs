namespace DiscordUserMonitoring.Services.Abstractions
{
    internal interface IEventService
    {
        public event Func<DiscordUser, Task>? OnDiscordUserMoved;
        public void DiscordUserMoved(DiscordUser discordUser);
    }
}
