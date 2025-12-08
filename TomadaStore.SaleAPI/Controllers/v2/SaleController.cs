using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;
        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }
        public async Task<IActionResult> PostSaleInQueue([FromBody] SaleRequestDTO saleRequestDTO)
        {
            try
            {

                var saleMessage = await _saleService.SendSaleToMessage(saleRequestDTO);

                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "sale", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(saleMessage));
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "sale", body: body);
                _logger.LogInformation("Sale message sent to queue.");
                return Created();
            } catch(Exception ex)
            {
                _logger.LogError("Error occurred while creating a new sale." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
