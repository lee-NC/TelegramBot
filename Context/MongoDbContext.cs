using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TelegramBot.Helper;

namespace TelegramBot.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        protected IMongoDatabase _db { get; set; }
        protected MongoClient _mongoClient { get; set; }
        protected IClientSessionHandle Session { get; set; }

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            MongoDbSettings setting = settings.Value;
            _mongoClient = new MongoClient(setting.ConnectionString);
            _db = _mongoClient.GetDatabase(setting.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }
        
        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await _mongoClient.StartSessionAsync();
        }
    }
}
