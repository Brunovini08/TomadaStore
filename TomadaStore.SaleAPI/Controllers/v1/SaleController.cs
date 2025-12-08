using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.SaleAPI.Services.Interfaces;

namespace TomadaStore.SaleAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;
        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleAsync([FromBody] SaleRequestDTO saleRequestDTO)
        {
            try
            {
                _logger.LogInformation("Creating a new sale.");
                await _saleService.CreateSaleAsync(saleRequestDTO);
                return Created();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating a new sale." + ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}
