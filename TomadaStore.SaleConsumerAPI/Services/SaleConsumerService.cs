using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStore.SaleConsumerAPI.Repositories.Interfaces;
using TomadaStore.SaleConsumerAPI.Services.Interfaces;

namespace TomadaStore.SaleConsumerAPI.Services
{
    public class SaleConsumerService : ISaleConsumerService
    {
        private readonly ISaleConsumerRepository _saleRepository;
        public SaleConsumerService(ISaleConsumerRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public Task ProcessSaleMessageAsync(SaleConsumerResponseDTO sale)
        {
            try
            {

                if (sale != null)
                {
                    return _saleRepository.CreateSaleAsync(sale);
                }
                else
                {
                    throw new Exception("Sale message is invalid.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
