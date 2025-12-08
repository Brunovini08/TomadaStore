using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductRequestDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(ProductRequestDTO product);
        Task UpdateProductAsync(string id, ProductRequestDTO product);
        Task DeleteProductAsync(string id);
    }
}