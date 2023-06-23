using TelegramBot.Context;

namespace TelegramBot.Helper;

public static class StartUpHelper
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration Configuration)
    {
        MongoDbConfig.Configure();
        services.Configure<MongoDbSettings>(Configuration.GetSection(typeof(MongoDbSettings).Name));
        services.AddTransient<IMongoDbContext, MongoDbContext>();
    }
}