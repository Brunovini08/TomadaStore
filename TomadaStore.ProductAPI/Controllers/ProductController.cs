using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductAPI.Services.Interfaces;

namespace TomadaStore.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all products.");
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while retrieving products." + ex.Message);
                return Problem(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetProductByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("Retrieving product by ID.");
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while retrieving product by ID." + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequestDTO product)
        {
            try
            {
                _logger.LogInformation("Creating a new product.");
                await _productService.CreateProductAsync(product);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new product." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
