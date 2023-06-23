using Hangfire.Annotations;

namespace TelegramBot.Service;

public static class TelegramBotServiceCollectionExtensions
{
    public static void AddTelegramBot(
        [NotNull] this IServiceCollection services, 
        [NotNull] IConfiguration config)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (config == null) throw new ArgumentNullException(nameof(config));

        services.Configure<TelegramBotOptions>(
            config.GetSection(TelegramBotOptions.Position));
        services.AddSingleton<ITelegramBotService, TelegramBotService>();
    }
}