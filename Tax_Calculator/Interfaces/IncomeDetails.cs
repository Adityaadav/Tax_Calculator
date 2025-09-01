namespace Tax_Calculator.Interfaces
{

    
        public interface IIncomeFromSalary
        {
            int SalaryIncomeID { get; set; }
            decimal TotalSalary { get; set; }
            string EmployerName { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IIncomeFromInterest
        {
            int InterestID { get; set; }
            string IncomeSource { get; set; }
            decimal InterestAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface RentalIncome
        {
            int RentalID { get; set; }
            string PropertyAddress { get; set; }
            decimal MonthlyRent { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IncomeFromDigital
        {
            int DigitaLAssetIncomeId { get; set; }
            string DigitalSource { get; set; }
            string TranscationType { get; set; }
            decimal TranscationAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface InterestHomeLoanLetout
        {
            int LoanInterestId { get; set; }
            string LenderName { get; set; }
            decimal interestAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }
}

        
    

