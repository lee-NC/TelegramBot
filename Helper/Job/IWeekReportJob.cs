using Microsoft.Extensions.Caching.Distributed;
using TelegramBot.Service;

namespace TelegramBot.Helper.Job
{
    public interface IWeekReportJob
    {
        Task Excute();
    }

    public class WeekReportJob : IWeekReportJob
    {
        private readonly ILogger<WeekReportJob> _logger;
        private readonly IDistributedCache _cache;
        private readonly ITelegramBotService _teleBot;

        public WeekReportJob(
            ILogger<WeekReportJob> logger,
            IDistributedCache cache,
            ITelegramBotService teleBot
            )
        {
            _logger = logger;
            _cache = cache;
            _teleBot = teleBot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Excute()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "TSU:Health bad with exception {ExMesg}", ex.Message);
                // _teleBot.SendMessage($"Hsm session state exception {ex.Message}");
                // _cache.SetAsync<HsmHealthEvent>("HSM_HEALTH_SYNC", new HsmHealthEvent
                // {
                //     IsSync = false,
                //     Timestamp = DateTime.UtcNow.Ticks,
                //     Reason= ex.Message
                // });
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
