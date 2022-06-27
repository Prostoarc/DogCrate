namespace DiscordUserMonitoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = new ServiceCollection()
                .AddOptions()
                .Configure<DiscordOptions>(config.GetSection("DiscordOptions"))
                .Configure<TelegramOptions>(config.GetSection("TelegramOptions"))
                .AddSingleton<IEventService, EventService>()
                .AddSingleton<IDiscordBotService, DiscordBotService>()
                .AddSingleton<ITelegramBotService, TelegramBotService>()
                .AddSingleton<Actions>()
                .BuildServiceProvider();

            var startup = services.GetService<Actions>();
            startup!.RunAsync().Wait();
        }
    }
}