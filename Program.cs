using Hangfire;
using TelegramBot.Helper;
using TelegramBot.Helper.Job;
using TelegramBot.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Telegram bot for notification
builder.Services.AddTelegramBot(configuration);

// Register database repositories
builder.Services.AddMongoDb(configuration);
builder.Services.AddHangfireService(configuration.GetSection("IMongoDbSettings:Host").Value ?? "", $"telegram_bot");

var app = builder.Build();

app.MapGet("report",
    () =>
    {
        RecurringJob.AddOrUpdate<IWeekReportJob>("send_report_weekly", c => c.Excute(),
            () => { return "0 0 8 */7 * *"; });
        Console.WriteLine("Job is running");
    });

app.Run();