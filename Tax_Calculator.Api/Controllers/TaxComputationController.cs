using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tax_Calculator.Api.Services;

namespace Tax_Calculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxComputationController : ControllerBase
    {
        private readonly TaxComputationService _taxComputationService;

        public TaxComputationController(TaxComputationService taxComputationService)
        {
            _taxComputationService = taxComputationService;
        }

        [HttpGet("{taxFilingId}")]
        public async Task<IActionResult> ComputeTax(int taxFilingId)
        {
            try
            {
                var result = await _taxComputationService.ComputeTaxAsync(taxFilingId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error computing tax: {ex.Message}");
            }
        }
    }
}
