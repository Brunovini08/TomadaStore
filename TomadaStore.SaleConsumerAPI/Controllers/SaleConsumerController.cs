
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumerAPI.Services.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleConsumerController : ControllerBase
    {
        private readonly ISaleConsumerService _saleConsumerService;
        public SaleConsumerController(ISaleConsumerService saleConsumerService)
        {
            _saleConsumerService = saleConsumerService;
        }

        [HttpPost]
        public async Task<IActionResult> ConsumeSaleMessageAsync()
        {
            try
            
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "payment", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);   

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var sale = JsonSerializer.Deserialize<SaleConsumerResponseDTO>(message);
                    return _saleConsumerService.ProcessSaleMessageAsync(sale);
                };

                await channel.BasicConsumeAsync("payment", autoAck: true, consumer: consumer);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
