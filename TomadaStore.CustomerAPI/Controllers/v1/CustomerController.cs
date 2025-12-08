using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerAPI.Services;
using TomadaStore.CustomerAPI.Services.interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        public readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody]CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer.");
                await _customerService.InsertCustomerAsync(customer);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new customer." + ex.Message);  
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Creating a new customer.");
                var customers = await _customerService.GetAllCustomersAsync();
                if(customers is null || customers.Count == 0)
                {
                    NotFound("Nenhum cliente encontrado");
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Creating a new customer.");
                var customer = await _customerService.GetAllCustomersAsync();
                if(customer is null)
                {
                    NotFound("Cliente não encontrado");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new customer." + ex.Message);
                return Problem(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> UpdateSituationCustomer([FromBody] CustomerUpdateSituationDTO customer, int id)
        {
            try
            {
                _logger.LogInformation("Updated a customer.");
                await _customerService.UpdateSituationCustomerAsync(customer, id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while updating a customer." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
