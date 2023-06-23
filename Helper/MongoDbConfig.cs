namespace TelegramBot.Helper;

public static class MongoDbConfig
{
    public static void Configure()
    {
        //// Cấu hình MongoDb driver bỏ qua thuộc tính thừa trong db
        // BsonClassMap.RegisterClassMap<TransactionLog>(cm =>
        // {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
    }
}