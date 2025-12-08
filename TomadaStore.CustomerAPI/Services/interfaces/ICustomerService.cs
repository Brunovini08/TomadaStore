using TomadaStore.Models.DTOs.Customer;


namespace TomadaStore.CustomerAPI.Services.interfaces
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);
        Task UpdateSituationCustomerAsync(CustomerUpdateSituationDTO customer, int id);
    }
}
