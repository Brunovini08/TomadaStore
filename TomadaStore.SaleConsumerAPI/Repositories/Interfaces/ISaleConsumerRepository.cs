using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleConsumerAPI.Repositories.Interfaces
{
    public interface ISaleConsumerRepository
    {
        public Task CreateSaleAsync(SaleConsumerResponseDTO sale);
    }
}
