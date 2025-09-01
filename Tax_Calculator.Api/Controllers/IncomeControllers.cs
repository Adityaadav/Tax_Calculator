using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;
using Tax_Calculator.Api.Services;

namespace Tax_Calculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeFromSalaryController : Controller
    {
        private readonly IIncomeFromSalaryService _salaryService;

        public IncomeFromSalaryController(IIncomeFromSalaryService salaryService)
        {
            _salaryService = salaryService;
        }

  

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _salaryService.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] IncomeFromSalary salary)
        {
            _salaryService.Add(salary);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] IncomeFromSalary salary)
        {
            salary.ConnectionDetailsID = id;
            _salaryService.Update(salary);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _salaryService.Delete(id);
            return Ok();
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeFromInterestController : ControllerBase
    {
        private readonly IIncomeFromInterestService _service;
        public IncomeFromInterestController(IIncomeFromInterestService service) => _service = service;

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost] public IActionResult Add([FromBody] IncomeFromInterest item) { _service.Add(item); return Ok(); }
        [HttpPut("{id}")] public IActionResult Update(int id, [FromBody] IncomeFromInterest item) { item.ConnectionDetailsID = id; _service.Update(item); return Ok(); }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { _service.Delete(id); return Ok(); }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class DigitalAssetIncomeController : ControllerBase
    {
        private readonly IDigitalAssetIncomeService _service;
        public DigitalAssetIncomeController(IDigitalAssetIncomeService service) => _service = service;

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost] public IActionResult Add([FromBody] DigitalAssetIncome item) { _service.Add(item); return Ok(); }
        [HttpPut("{id}")] public IActionResult Update(int id, [FromBody] DigitalAssetIncome item) { item.ConnectionDetailsID = id; _service.Update(item); return Ok(); }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { _service.Delete(id); return Ok(); }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class IncomeFromRentController : ControllerBase
    {
        private readonly IIncomeFromRentService _service;
        public IncomeFromRentController(IIncomeFromRentService service) => _service = service;

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var data = _service.GetById(id);
            if (data == null)
                return NotFound(); 

            return Ok(data); 
        }
        [HttpPost] public IActionResult Add([FromBody] IncomeFromRent item) { _service.Add(item); return Ok(); }
        [HttpPut("{id}")] public IActionResult Update(int id, [FromBody] IncomeFromRent item) { item.ConnectionDetailsID = id; _service.Update(item); return Ok(); }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { _service.Delete(id); return Ok(); }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class HomeLoanLetoutController : ControllerBase
    {
        private readonly IHomeLoanLetoutService _service;
        public HomeLoanLetoutController(IHomeLoanLetoutService service) => _service = service;

        [HttpGet("{id}")] public IActionResult GetById(int id) => Ok(_service.GetById(id));
        [HttpPost] public IActionResult Add([FromBody] HomeLoanLetoutInterest item) { _service.Add(item); return Ok(); }
        [HttpPut("{id}")] public IActionResult Update(int id, [FromBody] HomeLoanLetoutInterest item) { item.ConnectionDetailsID = id; _service.Update(item); return Ok(); }
        [HttpDelete("{id}")] public IActionResult Delete(int id) { _service.Delete(id); return Ok(); }
    }

}
