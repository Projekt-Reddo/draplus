using DraplusApi.Models;
using MongoDB.Driver;

namespace DraplusApi.Data
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }

    public class MongoContext : IMongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(MongoDbSetting mongoDbSetting)
        {
            var mongoClient = new MongoClient(mongoDbSetting.ConnectionString);
            Database = mongoClient.GetDatabase(mongoDbSetting.DatabaseName);
        }
    }
}