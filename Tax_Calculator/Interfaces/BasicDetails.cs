namespace Tax_Calculator.Interfaces
{
    public interface IBasicDetails
    {
        int Tax_Payer_Id { get; set; }
        string First_Name { get; set; }
        string Last_Name { get; set; }
        DateTime DOB { get; set; }
        string PAN_NUMBER { get; set; }
    }

    public interface ITaxPayerBankAccount
    {
        int BankAccountID { get; set; }
        int Tax_Payer_Id { get; set; }
        string BankName { get; set; }
        string BankAccountNumber { get; set; }
        string AccountType { get; set; }
    }
}
