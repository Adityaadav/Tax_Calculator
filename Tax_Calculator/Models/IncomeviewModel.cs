namespace Tax_Calculator.Models
{
    public class IncomeDetailsViewModel
    {
        public IncomeFromSalary Salary { get; set; } = new IncomeFromSalary(0, 0, string.Empty, 0);
        public DigitalAssetIncome DigitalAsset { get; set; } = new DigitalAssetIncome(0, string.Empty, string.Empty, 0, 0);
        public IncomeFromRent Rent { get; set; } = new IncomeFromRent(0, 0, string.Empty, 0);
        public IncomeFromInterest Interest { get; set; } = new IncomeFromInterest(0, string.Empty, 0, 0);
        public HomeLoanLetoutInterest HomeLoan { get; set; } = new HomeLoanLetoutInterest(0, string.Empty, 0, 0);
    }

}
