using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Models;
using TomadaStore.ProductAPI.Data;
using TomadaStore.ProductAPI.Repositories.Interfaces;

namespace TomadaStore.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly ConnectionDB _connectionDB;
        public ProductRepository(ILogger<ProductRepository> logger, ConnectionDB connectionDB)
        {
            _logger = logger;
            _connectionDB = connectionDB;
            _productsCollection = _connectionDB.GetProductsCollection();
        }

        public async Task CreateProductAsync(Product product)
        {
            try
            {
                await _productsCollection.InsertOneAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductRequestDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            try
            {
                var product = await _productsCollection.FindAsync(product => product.Id == ObjectId.Parse(id));
                var result = product.FirstOrDefault();
                if (result == null)
                {
                    throw new Exception("Product not found.");
                }
                return new ProductResponseDTO
                {
                    Id = result.Id.ToString(),
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = result.Category.Id.ToString(),
                        Name = result.Category.Name,
                        Description = result.Category.Description
                    }
                };
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateProductAsync(string id, ProductRequestDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
