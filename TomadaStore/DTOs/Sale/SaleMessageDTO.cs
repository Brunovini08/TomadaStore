using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleMessageDTO
    {
        public CustomerResponseDTO Customer { get; set; }
        public List<ProductResponseDTO> Products { get; set; }
    }
}