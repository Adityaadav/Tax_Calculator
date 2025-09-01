using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionDetailsController : ControllerBase
    {
        private readonly IConnectionDetailsService _connectionDetailsService;

        public ConnectionDetailsController(IConnectionDetailsService connectionDetailsService)
        {
            _connectionDetailsService = connectionDetailsService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddConnectionDetails([FromBody] ConnectionDetails model)
        {
            try
            {
                var id = await _connectionDetailsService.AddConnectionDetailsAsync(model);
                return Ok(new { ConnectionDetailsID = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConnectionDetailsById(int id)
        {
            var result = await _connectionDetailsService.GetConnectionDetailsByIdAsync(id);
            if (result == null)
                return NotFound(new { Message = "ConnectionDetails not found." });

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateConnectionDetails([FromBody] ConnectionDetails model)
        {
            try
            {
                await _connectionDetailsService.UpdateConnectionDetailsAsync(model);
                return Ok(new { Message = "Updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }

}
