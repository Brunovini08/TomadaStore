namespace TomadaStore.SaleConsumerAPI.Services.Interfaces
{
    public interface ISaleConsumerService
    {
        public Task ProcessSaleMessageAsync(string saleMessage);
    }
}
