using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Data;
using TomadaStore.SaleAPI.Repositories.Interfaces;

namespace TomadaStore.SaleAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger;
        private readonly IMongoCollection<Sale> _salesCollection;
        public SaleRepository(ILogger<SaleRepository> logger, ConnectionDB connectionDB)
        {
            _logger = logger;
            _salesCollection = connectionDB.GetSalesCollection();  
        }
        public async Task CreateSaleAsync(CustomerResponseDTO customer, List<ProductResponseDTO> products)
        {
            try
            {
                List<Product> productList = new List<Product>();
                foreach(var product in products)
                {
                    productList.Add(
                        new Product(
                            product.Id,
                            product.Name,
                            product.Description,
                            product.Price,
                            new Category(
                                product.Category.Id,
                                product.Category.Name,
                                product.Category.Description
                            )
                        )
                    );
                }
                var totalAmount = productList.Sum(p => p.Price);
                await _salesCollection.InsertOneAsync(
                    new Sale(
                    new Customer(customer.Id, customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber, customer.Situation),
                    productList,
                    totalAmount
                ));
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
