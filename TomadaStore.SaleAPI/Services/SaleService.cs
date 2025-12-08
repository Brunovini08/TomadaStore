using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Repositories.Interfaces;
using TomadaStore.SaleAPI.Services.Interfaces;

namespace TomadaStore.SaleAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ILogger<SaleService> _logger;
        private readonly ISaleRepository _saleRepository;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;
        public SaleService(ISaleRepository saleRepository, IHttpClientFactory httpClientFactory, ILogger<SaleService> logger)
        {
            _httpClientProduct = httpClientFactory.CreateClient("productAPI"); ;
            _httpClientCustomer = httpClientFactory.CreateClient("customerAPI"); ;
            _saleRepository = saleRepository;
            _logger = logger;
        }
        public async Task CreateSaleAsync(SaleRequestDTO saleRequestDTO)
        {
            try
            {
                var customers = await _httpClientCustomer.GetFromJsonAsync<List<CustomerResponseDTO>>(saleRequestDTO.CustomerId.ToString());
                var customer = customers?.FirstOrDefault(c => c.Id == saleRequestDTO.CustomerId);
                if (customer == null)
                    throw new Exception("Customer not found.");

                List<ProductResponseDTO> productRequestDTOs = new List<ProductResponseDTO>();

                foreach (var item in saleRequestDTO.ProductsIds)
                {
                   
                    var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(item.ToString());
                    if (product != null)
                    {
                        productRequestDTOs.Add(new ProductResponseDTO
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Category = new CategoryResponseDTO
                            {
                                Id = product.Category.Id,
                                Name = product.Category.Name,
                                Description = product.Category.Description

                            }
                        });
                    }
                }
                await _saleRepository.CreateSaleAsync(customer, productRequestDTOs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SaleMessageDTO> SendSaleToMessage(SaleRequestDTO saleRequestDTO)
        {
            try
            {
                var customers = await _httpClientCustomer.GetFromJsonAsync<List<CustomerResponseDTO>>(saleRequestDTO.CustomerId.ToString());
                var customer = customers?.FirstOrDefault(c => c.Id == saleRequestDTO.CustomerId);
                if (customer == null)
                    throw new Exception("Customer not found.");

                List<ProductResponseDTO> productRequestDTOs = new List<ProductResponseDTO>();

                foreach (var item in saleRequestDTO.ProductsIds)
                {

                    var product = await _httpClientProduct.GetFromJsonAsync<ProductResponseDTO>(item.ToString());
                    if (product != null)
                    {
                        productRequestDTOs.Add(new ProductResponseDTO
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                            Category = new CategoryResponseDTO
                            {
                                Id = product.Category.Id,
                                Name = product.Category.Name,
                                Description = product.Category.Description

                            }
                        });
                    }
                }
                
                var saleMessage = new SaleMessageDTO
                {
                    Customer = customer,
                    Products = productRequestDTOs
                };

                return saleMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
