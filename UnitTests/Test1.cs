
using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Api.Services;
using Tax_Calculator.Models;
using DeductionCalculator = Tax_Calculator.Api.Services.DeductionCalculator;
namespace UnitTests
{
    [TestClass]
    public  class Test1
    {
        [TestMethod]
        public void CalculateTax_WithHRAExemption_ShouldReduceTaxableIncome()
        {
            decimal income = 600000;
            var deductions = new DeductionInputsDto
            {
                ActualHRAReceived = 120000,
                RentPaidAnnually = 150000
               
            };
            var limit = new DeductionLimit
            {
                PercentageOfSalary = 40m    // 40% of salary
            };

            var result = new DeductionCalculator().CalculateHRAExemption(deductions, income, limit, 1);

            Assert.IsTrue(result < income, "HRA exemption not applied properly");
        }
    }
}
