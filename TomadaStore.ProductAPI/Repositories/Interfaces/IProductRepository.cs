using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;

namespace TomadaStore.ProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductRequestDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(string id, ProductRequestDTO product);
        Task DeleteProductAsync(string id);
    }
}
