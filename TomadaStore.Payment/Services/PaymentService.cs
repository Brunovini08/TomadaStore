using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.Payment.Services.Interfaces;

namespace TomadaStore.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task ProcessPaymentAsync(SaleMessageDTO saleMessage)
        {
            try
            {
                var totalPrice = saleMessage.Products.Sum(p => p.Price * saleMessage.Products.Count);

                List<Product> products = new List<Product>();
                foreach (var productDto in saleMessage.Products)
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
                var customer = new Customer(
                    saleMessage.Customer.Id,
                    saleMessage.Customer.FirstName,
                    saleMessage.Customer.LastName,
                    saleMessage.Customer.Email,
                    saleMessage.Customer.PhoneNumber,
                    saleMessage.Customer.Situation
                );
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "payment", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                if (totalPrice > 1000)
                {

                    var sale = new Sale(customer, products, totalPrice, false);
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sale));
                    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "payment", body: body);
                }
                else
                {
                    var sale = new Sale(customer, products, totalPrice, true);
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sale));
                    await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "payment", body: body);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
