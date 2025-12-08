using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Repositories.Interfaces;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public async Task CreateProductAsync(ProductRequestDTO product)
        {
            try
            {
                var newProduct = new Product(
                    product.Name,
                    product.Description,
                    product.Price,
                    new Category(
                        product.Category.Name,
                        product.Category.Description
                    ));
                await _productRepository.CreateProductAsync(newProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new product." + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductRequestDTO>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while retrieving products." + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            } catch(Exception ex)
            {
                _logger.LogError("Error occurred while retrieving product by ID." + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateProductAsync(string id, ProductRequestDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
