namespace DiscordUserMonitoring.Services
{
    internal class EventService : IEventService
    {
        public event Func<DiscordUser, Task>? OnDiscordUserMoved;
        public void DiscordUserMoved(DiscordUser discordUser)
        {
            OnDiscordUserMoved?.Invoke(discordUser);
        }
    }
}
