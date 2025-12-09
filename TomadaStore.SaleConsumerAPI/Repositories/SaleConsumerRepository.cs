

using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumerAPI.Data;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Repositories
{
    public class SaleConsumerRepository : ISaleConsumerRepository
    {
        private readonly IMongoCollection<Sale> _salesCollection;
        public SaleConsumerRepository(ConnectionDB connectionDB)
        {
            _salesCollection = connectionDB.GetSalesCollection();
        }
        public async Task CreateSaleAsync(SaleConsumerResponseDTO sale)
        {
            try
            {
                var customer = new Customer(
                    sale.Customer.Id,
                    sale.Customer.FirstName,
                    sale.Customer.LastName,
                    sale.Customer.Email,
                    sale.Customer.PhoneNumber,
                    sale.Customer.Situation
                );
                List<Product> products = new List<Product>();
                foreach (var productDto in sale.Products)
                {
                    var product = new Product(
                        productDto.Id,
                        productDto.Name,
                        productDto.Description,
                        productDto.Price,
                        new Category(
                            productDto.Category.Id,
                            productDto.Category.Name,
                            productDto.Category.Description
                        )
                    );
                    products.Add(product);
                }
                var totalPrice = sale.Products.Sum(p => p.Price);
                var newSale = new Sale(customer, products, totalPrice, sale.Approvals);
                await _salesCollection.InsertOneAsync(newSale);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
