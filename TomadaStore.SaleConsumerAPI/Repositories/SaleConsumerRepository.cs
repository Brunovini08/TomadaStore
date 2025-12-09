

using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumerAPI.Data;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Repositories
{
    public class SaleConsumerRepository : ISaleConsumerRepository
    {
        private readonly IMongoCollection<Sale> _salesCollection;
        public SaleConsumerRepository(ConnectionDB connectionDB)
        {
            _salesCollection = connectionDB.GetSalesCollection();
        }
        public async Task CreateSaleAsync(Sale sale)
        {
            try
            {
                await _salesCollection.InsertOneAsync(sale);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
