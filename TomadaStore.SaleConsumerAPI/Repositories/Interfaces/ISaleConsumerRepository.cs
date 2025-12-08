using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleConsumerAPI.Repositories.Interfaces
{
    public interface ISaleConsumerRepository
    {
        public Task CreateSaleAsync(CustomerResponseDTO customer, List<ProductResponseDTO> products);
    }
}
