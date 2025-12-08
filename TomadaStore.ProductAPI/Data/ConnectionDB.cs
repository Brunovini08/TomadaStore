using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;

namespace TomadaStore.ProductAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Product> _productsCollection;
        public ConnectionDB(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _productsCollection = database.GetCollection<Product>(mongoDBSettings.Value.CollectionName);
        }

        public IMongoCollection<Product> GetProductsCollection()
        {
            return _productsCollection;
        }
    }
}
