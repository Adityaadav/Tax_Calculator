using Tax_Calculator.Api.HelperClass;

namespace UnitTests;

[TestClass]
public class Test4
{
    [TestMethod]
    
    public void CalculateTax_OldRegime_Junior_BelowRebate_ShouldReturnZero()
    {
        decimal income = 450000; 
        int taxRegimeId = 1;
        bool isSenior = false, isSuperSenior = false;

        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CalculateTax_OldRegime_Senior_ShouldApplyCorrectSlabs()
    {
        decimal income = 600000;
        int taxRegimeId = 1; 
        bool isSenior = true, isSuperSenior = false;

        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CalculateTax_OldRegime_SuperSenior_ShouldApplyCorrectSlabs()
    {
        decimal income = 800000;
        int taxRegimeId = 1;
        bool isSenior = false, isSuperSenior = true;

        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        
        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CalculateTax_NewRegime_BelowRebate_ShouldReturnZero()
    {
        decimal income = 600000; 
        int taxRegimeId = 2; 
        bool isSenior = false, isSuperSenior = false;

        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CalculateTax_NewRegime_AboveThreshold_ShouldReturnTax()
    {
        decimal income = 1200000; // 12L, should incur tax
        int taxRegimeId = 2; // New Regime
        bool isSenior = false, isSuperSenior = false;

        
        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        Assert.IsTrue(result > 0);
    }

    [TestMethod]
    public void CalculateTax_InvalidRegime_ShouldReturnZero()
    {
        // Arrange
        decimal income = 1000000;
        int taxRegimeId = 99; // Invalid
        bool isSenior = false, isSuperSenior = false;

        // Act
        var result = TaxCalculatorHelper.CalculateTax(income, taxRegimeId, isSenior, isSuperSenior);

        // Assert
        Assert.AreEqual(0, result);
    }
}
