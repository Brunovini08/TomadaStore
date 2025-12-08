using TomadaStore.CustomerAPI.Repositories;
using TomadaStore.CustomerAPI.Repositories.interfaces;
using TomadaStore.CustomerAPI.Services.interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;
using TomadaStore.Models.Models.Enums.Customer;

namespace TomadaStore.CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }
        public Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                return _customerRepository.GetAllCustomersAsync();
            } catch(Exception ex)
            {
                throw new Exception("Error retrieving customers: " + ex.Message);
            }
        }

        public Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                return _customerRepository.GetCustomerByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customers: " + ex.Message);
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                await _customerRepository.InsertCustomerAsync(new Customer(customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber));
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting customer: " + ex.Message);
            }
        }

        public async Task UpdateSituationCustomerAsync(CustomerUpdateSituationDTO customer, int id)
        {
            try
            {
                if (!Enum.IsDefined(typeof(SituationType), customer.Situation))
                    throw new Exception("Valor da situação está incorreto");
                await _customerRepository.UpdateSituationCustomerAsync(customer, id);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
