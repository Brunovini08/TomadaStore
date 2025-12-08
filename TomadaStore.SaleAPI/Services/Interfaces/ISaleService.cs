using TomadaStore.Models.DTOs.Sale;

namespace TomadaStore.SaleAPI.Services.Interfaces
{
    public interface ISaleService
    {
        public Task CreateSaleAsync(SaleRequestDTO saleRequestDTO);
        public Task<SaleMessageDTO> SendSaleToMessage(SaleRequestDTO saleRequestDTO);
    }
}
