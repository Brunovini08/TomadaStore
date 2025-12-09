using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadaStore.SaleConsumerAPI.Services.Interfaces
{
    public interface ISaleConsumerService
    {
        public Task ProcessSaleMessageAsync(SaleConsumerResponseDTO sale);
    }
}
