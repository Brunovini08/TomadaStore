

using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
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
