using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Interfaces;
using Tax_Calculator.Models;
namespace Tax_Calculator.Api.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class BasicDetailsController : Controller
    {
        private readonly IBasicDetailsService _service;
        public BasicDetailsController(IBasicDetailsService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult Add(BasicDetails details)
        {
            _service.AddBasicDetails(details);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,BasicDetails details) {
            details.Tax_Payer_Id = id;
            _service.UpdateBasicDetails(details);
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) { 
          var result = _service.GetBasicDetailsById(id);    
            if (result == null) return NotFound();  
            return Ok(result);  
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class TaxPayerBankAccountController : ControllerBase
    {
        private readonly ITaxPayerBankAccountService _service;

        public TaxPayerBankAccountController(ITaxPayerBankAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(TaxPayerBankAccount account)
        {
            _service.AddTaxPayerBankAccount(account);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(TaxPayerBankAccount account)
        {
            _service.UpdateTaxPayerBankAccount(account);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var account = _service.GetTaxPayerBankAccountById(id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class TaxPeriodController : ControllerBase
    {
        private readonly ITaxPeriodService _service;
        public TaxPeriodController(ITaxPeriodService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(TaxPeriod period)
        {
            _service.AddTaxPeriod(period);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(TaxPeriod period)
        {
            _service.UpdateTaxPeriod(period);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetTaxPeriodById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentStatusController : ControllerBase
    {
        private readonly IResidentStatusService _service;

        public ResidentStatusController(IResidentStatusService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(ResidentStatus status)
        {
            _service.AddResidentStatus(status);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(ResidentStatus status)
        {
            _service.UpdateResidentStatus(status);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.GetResidentStatusById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class TaxRegimeController : ControllerBase
    {
        private readonly ITaxRegimeService _service;

        public TaxRegimeController(ITaxRegimeService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(TaxRegime regime)
        {
            _service.AddTaxRegime(regime);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(TaxRegime regime)
        {
            _service.UpdateTaxRegime(regime);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.GetTaxRegimeById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }

}
