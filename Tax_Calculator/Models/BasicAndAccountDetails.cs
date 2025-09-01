using Tax_Calculator.Interfaces;

namespace Tax_Calculator.Models
{
    public class BasicDetails : IBasicDetails
    {
        public int Tax_Payer_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime DOB { get; set; }
        public string PAN_NUMBER { get; set; }
    }

    public class TaxPayerBankAccount : ITaxPayerBankAccount
    {
        public int BankAccountID { get; set; }
        public int Tax_Payer_Id { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountType { get; set; }
    }
    public class TaxPeriod
    {
        public int TaxYearID { get; set; }
        public DateTime TaxYearStartDate { get; set; }
        public DateTime TaxYearEndDate { get; set; }
    }

    public class ResidentStatus
    {
        public int ResidentStatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class TaxRegime
    {
        public int RegimeID { get; set; }
        public string RegimeName { get; set; }
    }

}
