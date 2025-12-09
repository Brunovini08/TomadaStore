using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Threading.Tasks;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Payment.Services.Interfaces;

namespace TomadaStore.Payment.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Payment Service is running.");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            try
            {

                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();
                await channel.QueueDeclareAsync(queue: "sale", durable: false, exclusive: false, autoDelete: false,
                    arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    var sale = JsonSerializer.Deserialize<SaleMessageDTO>(message);
                    return _paymentService.ProcessPaymentAsync(sale);
                };
                await channel.BasicConsumeAsync("sale", autoAck: true, consumer: consumer);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
