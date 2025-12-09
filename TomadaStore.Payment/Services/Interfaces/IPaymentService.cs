using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.Payment.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task ProcessPaymentAsync(SaleMessageDTO saleMessage);
    }
}
