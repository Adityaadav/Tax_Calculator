using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Api.Services;
using DeductionCalculator = Tax_Calculator.Api.Services.DeductionCalculator;
using Tax_Calculator.Models;

namespace UnitTests
{
    [TestClass]
    public  class Test2

    {
        [TestMethod]
        public void CalculateTax_WithSection80C_ShouldReduceTaxableIncome()
        {
            // Arrange
            decimal income = 800000;
            var deductions = new DeductionInputsDto
            {
                Amount80CCD1 = 120000,  // Eligible under 80C
                Amount80CCD1B = 50000   // Extra NPS deduction
            };
            var limit = new DeductionLimit
            {
               MaximumAmount = 200000 // Maximum limit for 80C deductions

            };

            // Act
            var result = new DeductionCalculator().CalculateEmployeeContributionToNPS(deductions, limit, 1);

            // Assert
            Assert.IsTrue(result < income, "80C deductions not applied");
        }

    }
}
