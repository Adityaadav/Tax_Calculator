using System;
using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;    
namespace Tax_Calculator.Api.Services
{
    public class TaxComputationService
    {
        private readonly IDeductionAggregatorService1 _deductionAggregatorService;
        private readonly IIncomeAggregatorService _incomeAggregatorService;
        private readonly IDeductionLimitService _deductionLimitService;

        public TaxComputationService(
            IDeductionAggregatorService1 deductionAggregatorService,
            IIncomeAggregatorService incomeAggregatorService,
            IDeductionLimitService deductionLimitService)
        {
            _deductionAggregatorService = deductionAggregatorService;
            _incomeAggregatorService = incomeAggregatorService;
            _deductionLimitService = deductionLimitService;
        }

        public async Task<TaxSummary> ComputeTaxAsync(int taxFilingId)
        {
            var income = await _incomeAggregatorService.GetIncomeComponentsAsync(taxFilingId);

            income.TryGetValue("Salary", out decimal salary);
            income.TryGetValue("DigitalAsset", out decimal digitalAsset);
            income.TryGetValue("Rental", out decimal rentalIncome);
            income.TryGetValue("Interest", out decimal interestIncome);
            income.TryGetValue("HomeLoanLetout", out decimal businessIncome);

            decimal totalIncome = salary + digitalAsset + rentalIncome + interestIncome + businessIncome;

            var basicDetails = await _incomeAggregatorService.GetBasicDetailsAsync(taxFilingId);
            int age = CalculateAge(basicDetails.DOB);
            bool isSenior = age >= 60 && age < 80;
            bool isSuperSenior = age >= 80;

            int taxRegimeId = await _incomeAggregatorService.GetTaxRegimeIdAsync(taxFilingId);
            if (taxRegimeId == 0)
            {
                throw new InvalidOperationException("Tax regime ID is not set for the tax filing.");
            }

            var rawDeductions = await _deductionAggregatorService.GetDeductionsAsync(taxFilingId); // DeductionInputsDto

            

            var limitsDict = await _deductionLimitService.GetDeductionLimitsAsync();
            var limits = limitsDict.Values.Where(l => l != null).ToList();

            decimal totalDeductions = new  DeductionCalculator().CalculateTotalDeduction(
                rawDeductions,
                limits,
                salary,
                isSenior,
                taxRegimeId
            );

            decimal taxableIncome = Math.Max(0, totalIncome - totalDeductions);

            decimal totalTax = TaxCalculatorHelper.CalculateTax(
                taxableIncome,
                taxRegimeId,
                isSenior,
                isSuperSenior
            );

            return new TaxSummary
            {
                TaxFilingId = taxFilingId,
                TotalIncome = totalIncome,
                TotalDeductions = totalDeductions,
                TaxableIncome = taxableIncome,
                TotalTaxPayable = totalTax,
                TaxRegimeId = taxRegimeId,
                IsSeniorCitizen = isSenior,
                IsSuperSeniorCitizen = isSuperSenior
            };
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }

    public class TaxSummary
    {
        public int TaxFilingId { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal TotalTaxPayable { get; set; }
        public int TaxRegimeId { get; set; }
        public bool IsSeniorCitizen { get; set; }
        public bool IsSuperSeniorCitizen { get; set; }
    }

}
