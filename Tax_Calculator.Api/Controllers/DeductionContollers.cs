using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Api.Services;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRAExemptionController : ControllerBase
    {
        private readonly IHRAExemptionService _hraService;

        public HRAExemptionController(IHRAExemptionService hraService)
        {
            _hraService = hraService;
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    return Ok(_hraService.GetAll());
        //}

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hra = _hraService.GetById(id);
            if (hra == null) return NotFound();
            return Ok(hra);
        }

        [HttpPost]
        public IActionResult Create([FromBody] HRAExemption hra)
        {
            _hraService.Add(hra);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HRAExemption hra)
        {
            hra.ConnectionDetailsID = id;
            _hraService.Update(hra);
            return Ok();
        }

        
    }
    [ApiController]
    [Route("api/[controller]")]
    public class EntertainmentProfessionalTaxController : ControllerBase
    {
        private readonly IEntertainmentProfessionalTaxService _service;

        public EntertainmentProfessionalTaxController(IEntertainmentProfessionalTaxService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] EntertainmentProfessionalTax model)
        {
             _service.Add(model);
             return Ok("Record added successfully.");
            
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var data = _service.GetById(id);
            if (data == null) return NotFound("Record not found.");

            return Ok(data);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] EntertainmentProfessionalTax model)
        {
             _service.Update(model);
             return Ok("Record updated successfully.");
            
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeContributionToNPSController : ControllerBase
    {
        private readonly IEmployeeContributionToNPSService _service;

        public EmployeeContributionToNPSController(IEmployeeContributionToNPSService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] EmployeeContributionToNPS model)
        {
             _service.Add(model);
             return Ok("Record added successfully.");
           
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var data = _service.GetById(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeContributionToNPS model)
        {
             _service.Update(model);
           
            return Ok();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LTAAllowanceController : ControllerBase
    {
        private readonly ILTAAllowanceService _service;

        public LTAAllowanceController(ILTAAllowanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] LTAAllowance model)
        {
            var id = _service.Add(model);
            return Ok(new { LTAId = id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] LTAAllowance model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }



        [ApiController]
        [Route("api/[controller]")]
        public class DisabledIndividual80UController : ControllerBase
        {
            private readonly IDisabledIndividual80UService _service;

            public DisabledIndividual80UController(IDisabledIndividual80UService service)
            {
                _service = service;
            }

            [HttpPost]
            public IActionResult Add([FromBody] DisabledIndividual80U model)
            {
                var id = _service.Add(model);
                return Ok(id);
            }

            [HttpPut("{id}")]
            public IActionResult Update(int id,[FromBody] DisabledIndividual80U model)
            {
            model.ConnectionDetailsID = id;
                _service.Update(model);
                return NoContent();
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var result = _service.Get(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
        }


    [ApiController]
    [Route("api/[controller]")]
    public class ElectricVehicleLoanController : ControllerBase
    {
        private readonly IInterestOnElectricVehicleLoanService _service;

        public ElectricVehicleLoanController(IInterestOnElectricVehicleLoanService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] InterestOnElectricVehicleLoan model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] InterestOnElectricVehicleLoan model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return Ok(result);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class EmployerNPSController : ControllerBase
    {
        private readonly IEmployerContributionToNPSService _service;

        public EmployerNPSController(IEmployerContributionToNPSService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] EmployerContributionToNPS model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] EmployerContributionToNPS model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return Ok(result);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class HomeLoanSelfController : ControllerBase
    {
        private readonly IInterestOnHomeLoanSelfService _service;

        public HomeLoanSelfController(IInterestOnHomeLoanSelfService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] InterestOnHomeLoanSelf model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] InterestOnHomeLoanSelf model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class InterestOnHomeLoanLetOutController : ControllerBase
    {
        private readonly IInterestOnHomeLoanLetOutService _service;

        public InterestOnHomeLoanLetOutController(IInterestOnHomeLoanLetOutService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] InterestOnHomeLoanLetOut model)
        {
            var result = _service.Add(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] InterestOnHomeLoanLetOut model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class TransportAllowanceSpeciallyAbledController : ControllerBase
    {
        private readonly ITransportAllowanceSpeciallyAbledService _service;

        public TransportAllowanceSpeciallyAbledController(ITransportAllowanceSpeciallyAbledService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] TransportAllowanceSpeciallyAbled model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] TransportAllowanceSpeciallyAbled model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return result == null ? NotFound() : Ok(result);
        }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class ConveyanceAllowanceController : ControllerBase
    {
        private readonly IConveyanceAllowanceService _service;

        public ConveyanceAllowanceController(IConveyanceAllowanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ConveyanceAllowance model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] ConveyanceAllowance model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class DonationToPoliticalPartyTrustController : ControllerBase
    {
        private readonly IDonationToPoliticalPartyTrustService _service;

        public DonationToPoliticalPartyTrustController(IDonationToPoliticalPartyTrustService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add([FromBody] DonationToPoliticalPartyTrust model)
        {
            var id = _service.Add(model);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] DonationToPoliticalPartyTrust model)
        {
            model.ConnectionDetailsID = id;
            _service.Update(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class AgniveerCorpusFundController : ControllerBase
    {
        private readonly IAgniveerCorpusFundService _service;

        public AgniveerCorpusFundController(IAgniveerCorpusFundService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(AgniveerCorpusFund fund)
        {
            _service.AddAgniveerCorpusFund(fund);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,AgniveerCorpusFund fund)
        {
            fund.ConnectionDetailsID = id;
            _service.UpdateAgniveerCorpusFund(fund);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var fund = _service.GetAgniveerCorpusFundById(id);
            if (fund == null)
                return NotFound();
            return Ok(fund);
        }
    }
    [ApiController]
    [Route("api/[controller]")]
    public class Exemption10CController : ControllerBase
    {
        private readonly IExemption10CService _service;

        public Exemption10CController(IExemption10CService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(Exemption10C exemption)
        {
            _service.AddExemption10C(exemption);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,Exemption10C exemption)
        {
            exemption.ConnectionDetailsID = id;
            _service.UpdateExemption10C(exemption);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetExemption10CById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
    
    [ApiController]
    [Route("api/[controller]")]
    public class Deduction80CController : ControllerBase
    {
        private readonly IDeduction80CService _service;  

        public Deduction80CController(IDeduction80CService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(Deduction80C deduction)  
        {
            _service.AddDeduction80C(deduction);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,Deduction80C deduction)
        {
            deduction.ConnectionDetailsID = id;
            _service.UpdateDeduction80C(deduction);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deduction = _service.GetDeduction80CById(id);
            if (deduction == null)
                return NotFound();
            return Ok(deduction);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class SavingBankInterestController : ControllerBase
    {
        private readonly ISavingBankInterestService _service;

        public SavingBankInterestController(ISavingBankInterestService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(SavingBankInterest interest)
        {
            _service.AddSavingBankInterest(interest);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,SavingBankInterest interest)
        {
            interest.ConnectionDetailsID = id;
            _service.UpdateSavingBankInterest(interest);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var interest = _service.GetSavingBankInterestById(id);
            if (interest == null)
                return NotFound();
            return Ok(interest);
        }
    }



}



