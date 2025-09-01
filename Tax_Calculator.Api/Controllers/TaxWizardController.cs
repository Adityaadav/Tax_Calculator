using Microsoft.AspNetCore.Mvc;
using global::Tax_Calculator.Api.HelperClass;
using global::Tax_Calculator.Api.Interface;
using global::Tax_Calculator.Api.Services;
using global::Tax_Calculator.Models;
using Microsoft.AspNetCore.Mvc;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Api.Services;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Controllers
{
 
    
        public class TaxWizardController : Controller
        {
            private readonly IBasicDetailsService _basicDetailsService;
            private readonly ITaxPayerBankAccountService _bankAccountService;
            private readonly IConnectionDetailsService _connectionService;
            private readonly IIncomeAggregatorService _incomeAggregatorService;
            private readonly IDeductionAggregatorService1 _deductionAggregatorService;
            private readonly TaxComputationService _taxComputationService;

            public TaxWizardController(
                IBasicDetailsService basicDetailsService,
                ITaxPayerBankAccountService bankAccountService,
                IConnectionDetailsService connectionService,
                IIncomeAggregatorService incomeAggregatorService,
                IDeductionAggregatorService1 deductionAggregatorService,
                TaxComputationService taxComputationService)
            {
                _basicDetailsService = basicDetailsService;
                _bankAccountService = bankAccountService;
                _connectionService = connectionService;
                _incomeAggregatorService = incomeAggregatorService;
                _deductionAggregatorService = deductionAggregatorService;
                _taxComputationService = taxComputationService;
            }

            // Step 1: Basic Details
            [HttpGet]
            public IActionResult BasicDetails()
            {
                return View("_BasicDetails");
            }

            [HttpPost]
            public async Task<IActionResult> BasicDetails(BasicDetails model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                _basicDetailsService.AddBasicDetails(model);

                // Create connection details record
                var connectionId = await _connectionService.AddConnectionDetailsAsync(new ConnectionDetails
                {
                    TaxPayerID = model.Tax_Payer_Id,
                    //TaxYearID = DateTime.UtcNow
                });

                TempData["ConnectionId"] = connectionId;

                return RedirectToAction("_BankDetails");
            }

            // Step 2: Bank Details
            [HttpGet]
            public IActionResult BankDetails()
            {
                return View("_BankDetails");
            }

            [HttpPost]
            public IActionResult BankDetails(TaxPayerBankAccount account)
            {
                if (!ModelState.IsValid)
                    return View(account);

                _bankAccountService.AddTaxPayerBankAccount(account);
                return RedirectToAction("_Income");
            }

            // Step 3: Income Details
            [HttpGet]
            public IActionResult IncomeDetails()
            {
                return View("_Income");
            }

            [HttpPost]
            public IActionResult IncomeDetails(IncomeDetailsViewModel model)
            {
                if (!ModelState.IsValid)
                    return View(model);


                return RedirectToAction("_Deduction");
            }

            [HttpGet]
            public IActionResult DeductionDetails()
            {
                return View("_Deduction");
            }

            [HttpPost]
            public IActionResult DeductionDetails(DeductionInputsDto model)
            {
                if (!ModelState.IsValid)
                    return View(model);

                // Save deduction details similarly
                return RedirectToAction("Review");
            }

            // Step 5: Review / Summary
            [HttpGet]
            public async Task<IActionResult> Review()
            {
                if (TempData["ConnectionId"] == null)
                    return RedirectToAction("BasicDetails");

                int connectionId = Convert.ToInt32(TempData["ConnectionId"]);

                var taxSummary = await _taxComputationService.ComputeTaxAsync(connectionId);
                return View(taxSummary);
            }
        }
    }




