using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        public Task CreateSaleAsync(CustomerResponseDTO customer, List<ProductResponseDTO> products);
    }
}
