using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;
using TelegramBot.Helper.Job;

namespace TelegramBot.Helper;

public static class HangfireHelper
{
    public static void AddHangfireService(this IServiceCollection services, string mongoUri, string dbName)
    {
        var mongoUrlBuilder = new MongoUrlBuilder($"{mongoUri}/{dbName}");
        var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

        // Add Hangfire services. Hangfire.AspNetCore nuget required
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                },
                Prefix = "telegram_bot",
                CheckConnection = true,
                CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
            })
        );
        // Add the processing server as IHostedService
        services.AddHangfireServer(serverOptions => { serverOptions.ServerName = "Hangfire.Mongo telegram_bot"; });

        services.AddSingleton<IWeekReportJob, WeekReportJob>();
    }
}