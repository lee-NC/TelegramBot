using MongoDB.Driver;

namespace TelegramBot.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);

        Task<IClientSessionHandle> StartSessionAsync();
    }
}
