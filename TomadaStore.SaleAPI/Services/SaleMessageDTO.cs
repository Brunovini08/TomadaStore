using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.SaleAPI.Services
{
    public class SaleMessageDTO
    {
        public CustomerResponseDTO Customer { get; set; }
        public List<ProductResponseDTO> Products { get; set; }
    }
}