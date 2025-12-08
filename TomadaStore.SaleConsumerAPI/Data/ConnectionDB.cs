using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleConsumerAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Sale> _salesCollection;
        public ConnectionDB(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _salesCollection = database.GetCollection<Sale>(mongoDBSettings.Value.CollectionName);
        }

        public IMongoCollection<Sale> GetSalesCollection()
        {
            return _salesCollection;
        }
    }
}
