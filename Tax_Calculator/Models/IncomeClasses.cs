using Tax_Calculator.Interfaces;

namespace Tax_Calculator.Models
{
    public class IncomeFromSalary : IIncomeFromSalary
    {
       

        public int SalaryIncomeID { get; set; }
        public decimal TotalSalary { get; set; }
        public string EmployerName { get; set; }

        public int ConnectionDetailsID { get; set; }


        public IncomeFromSalary(int salaryIncomeID, decimal totalSalary, string employerName, int connectionDetailsID   )
        {
            SalaryIncomeID = salaryIncomeID;
            TotalSalary = totalSalary;
            EmployerName = employerName;
            ConnectionDetailsID = connectionDetailsID;
        }

    }

    public class DigitalAssetIncome : IncomeFromDigital
    {
        public int DigitaLAssetIncomeId { get; set; }
        public string DigitalSource { get; set; }
        public string TranscationType { get; set; }
        public decimal TranscationAmount { get; set; }

        public int ConnectionDetailsID { get; set; }


        public DigitalAssetIncome(int digitaLAssetIncomeId, string digitalSource, string transcationType, decimal transcationAmount, int connectionDetailsID)
        {
            DigitaLAssetIncomeId = digitaLAssetIncomeId;
            DigitalSource = digitalSource;
            TranscationType = transcationType;
            TranscationAmount = transcationAmount;
            ConnectionDetailsID = connectionDetailsID;  
        }

       
    }
    public class IncomeFromRent : RentalIncome
    {
        public int RentalID { get; set; }
        public string PropertyAddress { get; set; }
        public decimal MonthlyRent { get; set; }

        public int ConnectionDetailsID { get; set; }

        public IncomeFromRent(int rentalId, decimal monthlyRent, string propertyAddress, int connectionDetailsID)
        {
            RentalID = rentalId;
            MonthlyRent = monthlyRent;
            PropertyAddress = propertyAddress;
            ConnectionDetailsID = connectionDetailsID;
        }


    }

    public class IncomeFromInterest : IIncomeFromInterest
    {
        public int InterestID { get; set; }
        public string IncomeSource { get; set; }
        public decimal InterestAmount { get; set; }

        public int ConnectionDetailsID { get; set; }



        public IncomeFromInterest(int interestID, string incomeSource, decimal interestAmount, int connectionDetailsID)
        {
            InterestID = interestID;
            IncomeSource = incomeSource;
            InterestAmount = interestAmount;
            ConnectionDetailsID = connectionDetailsID;  

        }

       
    }

    public class HomeLoanLetoutInterest : InterestHomeLoanLetout
    {
        public int LoanInterestId { get; set; }
        public string LenderName { get; set; }
        public decimal interestAmount { get; set; }

        public int ConnectionDetailsID { get; set; }


        public HomeLoanLetoutInterest(int loanInterestId, string lenderName, decimal interestAmount,int connectionDetailsID)
        {
            LoanInterestId = loanInterestId;
            LenderName = lenderName;
            this.interestAmount = interestAmount;
            ConnectionDetailsID = connectionDetailsID;

        }

        
    }


}
