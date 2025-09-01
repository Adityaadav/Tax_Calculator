using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Models;
using DeductionCalculator = Tax_Calculator.Api.Services.DeductionCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTests;

[TestClass]
public class Test3
{
     private DeductionCalculator _calculator;
    private DeductionInputsDto _dto;
    private DeductionLimit _limit;

    [TestInitialize]
    public void Setup()
    {
        _calculator = new DeductionCalculator();
        _dto = new DeductionInputsDto();
        _limit = new DeductionLimit();
    }

    [TestMethod]
    public void CalculateEmployerContributionToNPS_ShouldReturn_MinOfContributionAndCap_WhenRegime2()
    {
        _dto.ContributionAmount = 50000;
        _limit.PercentageOfSalary = 10; // 10%
        decimal salary = 400000; // cap = 40,000

        var result = _calculator.CalculateEmployerContributionToNPS(_dto, salary, _limit, 2);

        Assert.AreEqual(40000, result);
    }

    [TestMethod]
    public void CalculateEmployerContributionToNPS_ShouldReturn0_WhenWrongRegime()
    {
        var result = _calculator.CalculateEmployerContributionToNPS(_dto, 400000, _limit, 1);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void CalculateMedicalInsurance80D_ShouldApplyMaxLimit()
    {
        _dto.PremiumPaidSelfFamily = 25000;
        _dto.PreventiveHealthCheckup = 5000;
        _limit.MaximumAmount = 20000;

        var result = _calculator.CalculateMedicalInsurance80D(_dto, false, _limit, 1);

        Assert.AreEqual(20000, result);
    }

    [TestMethod]
    public void CalculateLTA_ShouldReturnMinOfClaimedAndActual()
    {
        _dto.ClaimedAmount = 20000;
        _dto.ActualTravelExpense = 15000;

        var result = _calculator.CalculateLTA(_dto, _limit, 1);

        Assert.AreEqual(15000, result);
    }

    [TestMethod]
    public void CalculateDisabledIndividual80U_ShouldReturnMaxAmount()
    {
        _limit.MaximumAmount = 125000;

        var result = _calculator.CalculateDisabledIndividual80U(_dto, _limit, 1);

        Assert.AreEqual(125000, result);
    }

    [TestMethod]
    public void CalculateEVLoanInterest80EEB_ShouldApplyLimit()
    {
        _dto.InterestAmountPaid = 120000;
        _limit.MaximumAmount = 100000;

        var result = _calculator.CalculateEVLoanInterest80EEB(_dto, _limit, 1);

        Assert.AreEqual(100000, result);
    }

    [TestMethod]
    public void CalculateInterestHomeLoanSelf_ShouldApplyLimit()
    {
        _dto.InterestAmountPaidSelf = 250000;
        _limit.MaximumAmount = 200000;

        var result = _calculator.CalculateInterestHomeLoanSelf(_dto, _limit, 1);

        Assert.AreEqual(200000, result);
    }

    [TestMethod]
    public void CalculateInterestHomeLoanLetOut_ShouldReturnInterestPaid()
    {
        _dto.InterestAmountPaidLetOut = 180000;

        var result = _calculator.CalculateInterestHomeLoanLetOut(_dto, _limit, 1);

        Assert.AreEqual(180000, result);
    }

    [TestMethod]
    public void CalculateDonation80G_ShouldApplyPercentage()
    {
        _dto.DonationAmount = 10000;
        _limit.PercentageOfSalary = 50; // 50%

        var result = _calculator.CalculateDonation80G(_dto, true, _limit, 1);

        Assert.AreEqual(5000, result);
    }

    [TestMethod]
    public void CalculateConveyanceAllowance_ShouldReturnMin()
    {
        _dto.ConveyanceAmountReceived = 4000;
        _dto.ConveyanceExemptAmount = 1600;

        var result = _calculator.CalculateConveyanceAllowance(_dto, _limit, 1);

        Assert.AreEqual(1600, result);
    }

    [TestMethod]
    public void CalculateTransportAllowanceSpeciallyAbled_ShouldApplyLimit()
    {
        _dto.TransportAllowanceReceived = 3000;
        _limit.MaximumAmount = 2000;

        var result = _calculator.CalculateTransportAllowanceSpeciallyAbled(_dto, _limit, 1);

        Assert.AreEqual(2000, result);
    }

    [TestMethod]
    public void CalculateAgniveerCorpusFund_ShouldAddEmployeeAndGovtContributions_WhenRegime2()
    {
        _dto.EmployeeCorpusContribution = 5000;
        _dto.GovtCorpusContribution = 7000;

        var result = _calculator.CalculateAgniveerCorpusFund(_dto, _limit, 2);

        Assert.AreEqual(12000, result);
    }

    [TestMethod]
    public void CalculateExemption10C_ShouldApplyLimit()
    {
        _dto.ExemptAmount10C = 60000;
        _limit.MaximumAmount = 50000;

        var result = _calculator.CalculateExemption10C(_dto, _limit, 1);

        Assert.AreEqual(50000, result);
    }

    [TestMethod]
    public void CalculateSavingBankInterest_ShouldApplyLimit()
    {
        _dto.SavingBankInterestEarned = 15000;
        _limit.MaximumAmount = 10000;

        var result = _calculator.CalculateSavingBankInterest(_dto, false, _limit, 1);

        Assert.AreEqual(10000, result);
    }

    [TestMethod]
    public void CalculateDeduction80C_ShouldApplyLimit()
    {
        _dto.Total80CDeductionClaimed = 200000;
        _limit.MaximumAmount = 150000;

        var result = _calculator.CalculateDeduction80C(_dto, _limit, 1);

        Assert.AreEqual(150000, result);
    }

    [TestMethod]
    public void CalculateEntertainmentProfessionalTax_ShouldAddBothWithinLimits()
    {
        _dto.EntertainmentAllowanceReceived = 6000;
        _dto.ProfessionalTaxPaid = 3000;
        _limit.MaximumAmount = 5000;

        var result = _calculator.CalculateEntertainmentProfessionalTax(_dto, _limit, 1);

        // entLimit = min(5000, 6000) = 5000
        // proTaxLimit = min(5000, 3000) = 3000
        Assert.AreEqual(8000, result);
    }
   
}
