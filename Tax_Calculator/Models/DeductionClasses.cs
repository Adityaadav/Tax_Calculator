using Tax_Calculator.Interfaces;

namespace Tax_Calculator.Models
{
    public class HRAExemption : IHRAExemption
    {
        public int HRAExemptionID { get; set; }
        public decimal ActualHRAReceived { get; set; }
        public decimal RentPaidAnnually { get; set; }
        public int ConnectionDetailsID { get; set; }

        public HRAExemption(int hraExemptionID, decimal actualHRAReceived, decimal rentPaidAnnually, int connectionDetailsID)
        {
            HRAExemptionID = hraExemptionID;
            ActualHRAReceived = actualHRAReceived;
            RentPaidAnnually = rentPaidAnnually;
            ConnectionDetailsID = connectionDetailsID;
        }

        public HRAExemption() { }
    }

    public class EntertainmentProfessionalTax : IEntertainmentProfessionalTax
    {
        public int ProfessionalId { get; set; }
        public decimal ProfessionalTaxPaid { get; set; }
        public decimal EntertainmentAllowanceReceived { get; set; }
        public int ConnectionDetailsID { get; set; }

        public EntertainmentProfessionalTax(int professionalId, decimal professionalTaxPaid, decimal entertainmentAllowanceReceived, int connectionDetailsID)
        {
            ProfessionalId = professionalId;
            ProfessionalTaxPaid = professionalTaxPaid;
            EntertainmentAllowanceReceived = entertainmentAllowanceReceived;
            ConnectionDetailsID = connectionDetailsID;
        }

        public EntertainmentProfessionalTax() { }
    }

    public class EmployeeContributionToNPS : IEmployeeContributionToNPS
    {
        public int EmployeeNPSID { get; set; }
        public decimal Amount80CCD1 { get; set; }
        public decimal Amount80CCD1B { get; set; }
        public int ConnectionDetailsID { get; set; }

        public EmployeeContributionToNPS(int employeeNPSID, decimal amount80CCD1, decimal amount80CCD1B, int connectionDetailsID)
        {
            EmployeeNPSID = employeeNPSID;
            Amount80CCD1 = amount80CCD1;
            Amount80CCD1B = amount80CCD1B;
            ConnectionDetailsID = connectionDetailsID;
        }

        public EmployeeContributionToNPS() { }
    }

    public class LTAAllowance : ILTAAllowance
    {
        public int LTAId { get; set; }
        public decimal ClaimedAmount { get; set; }
        public decimal ActualTravelExpense { get; set; }
        public int ConnectionDetailsID { get; set; }

        public LTAAllowance(int ltaId, decimal claimedAmount, decimal actualTravelExpense, int connectionDetailsID)
        {
            LTAId = ltaId;
            ClaimedAmount = claimedAmount;
            ActualTravelExpense = actualTravelExpense;
            ConnectionDetailsID = connectionDetailsID;
        }

        public LTAAllowance() { }
    }

    public class MedicalInsurancePremium80D : IMedicalInsurancePremium80D
    {
        public int Medical80DID { get; set; }
        public decimal PremiumPaidSelfFamily { get; set; }
        public decimal PreventiveHealthCheckup { get; set; }
        public int ConnectionDetailsID { get; set; }

        public MedicalInsurancePremium80D(int medical80DID, decimal premiumPaidSelfFamily, decimal preventiveHealthCheckup, int connectionDetailsID)
        {
            Medical80DID = medical80DID;
            PremiumPaidSelfFamily = premiumPaidSelfFamily;
            PreventiveHealthCheckup = preventiveHealthCheckup;
            ConnectionDetailsID = connectionDetailsID;
        }

        public MedicalInsurancePremium80D() { }
    }

    public class DisabledIndividual80U : IDisabledIndividual80U
    {
        public int Disable80UId { get; set; }
        public string DisabilityType { get; set; }
        public int DisabilityPercentage { get; set; }
        public bool IsSevereDisability { get; set; }
        public decimal DeductionClaimed { get; set; }
        public string MedicalAuthority { get; set; }
        public int ConnectionDetailsID { get; set; }

        public DisabledIndividual80U(int disable80UId, string disabilityType, int disabilityPercentage, bool isSevereDisability, decimal deductionClaimed, string medicalAuthority, int connectionDetailsID)
        {
            Disable80UId = disable80UId;
            DisabilityType = disabilityType;
            DisabilityPercentage = disabilityPercentage;
            IsSevereDisability = isSevereDisability;
            DeductionClaimed = deductionClaimed;
            MedicalAuthority = medicalAuthority;
            ConnectionDetailsID = connectionDetailsID;
        }

        public DisabledIndividual80U() { }
    }

    public class InterestOnElectricVehicleLoan : IInterestOnElectricVehicleLoan
    {
        public int ElectricLoanInterestId { get; set; }
        public string LenderName { get; set; }
        public string LoanAccountNumber { get; set; }
        public DateTime LoanSanctionDate { get; set; }
        public string ElectricVehicleMakeModel { get; set; }
        public decimal InterestAmountPaid { get; set; }
        public decimal DeductionClaimed { get; set; }
        public int ConnectionDetailsID { get; set; }

        public InterestOnElectricVehicleLoan(int electricLoanInterestId, string lenderName, string loanAccountNumber, DateTime loanSanctionDate, string electricVehicleMakeModel, decimal interestAmountPaid, decimal deductionClaimed, int connectionDetailsID)
        {
            ElectricLoanInterestId = electricLoanInterestId;
            LenderName = lenderName;
            LoanAccountNumber = loanAccountNumber;
            LoanSanctionDate = loanSanctionDate;
            ElectricVehicleMakeModel = electricVehicleMakeModel;
            InterestAmountPaid = interestAmountPaid;
            DeductionClaimed = deductionClaimed;
            ConnectionDetailsID = connectionDetailsID;
        }

        public InterestOnElectricVehicleLoan() { }
    }

    public class EmployerContributionToNPS : IEmployerContributionToNPS
    {
        public int EmployerNPSId { get; set; }
        public string EmployerName { get; set; }
        public decimal ContributionAmount { get; set; }
        public int ConnectionDetailsID { get; set; }

        public EmployerContributionToNPS(int employerNPSId, string employerName, decimal contributionAmount, int connectionDetailsID)
        {
            EmployerNPSId = employerNPSId;
            EmployerName = employerName;
            ContributionAmount = contributionAmount;
            ConnectionDetailsID = connectionDetailsID;
        }

        public EmployerContributionToNPS() { }
    }

    public class InterestOnHomeLoanSelf : IInterestOnHomeLoanSelf
    {
        public int InterestHomeLoanSelfId { get; set; }
        public string LenderName { get; set; }
        public string PropertyAddress { get; set; }
        public decimal InterestAmountPaid { get; set; }
        public decimal ClaimedAmount { get; set; }
        public int ConnectionDetailsID { get; set; }

        public InterestOnHomeLoanSelf(int interestHomeLoanSelfId, string lenderName, string propertyAddress, decimal interestAmountPaid, decimal claimedAmount, int connectionDetailsID)
        {
            InterestHomeLoanSelfId = interestHomeLoanSelfId;
            LenderName = lenderName;
            PropertyAddress = propertyAddress;
            InterestAmountPaid = interestAmountPaid;
            ClaimedAmount = claimedAmount;
            ConnectionDetailsID = connectionDetailsID;
        }

        public InterestOnHomeLoanSelf() { }
    }

    public class InterestOnHomeLoanLetOut : IInterestOnHomeLoanLetOut
    {
        public int InterestHomeLoanLetOutId { get; set; }
        public string LenderName { get; set; }
        public string PropertyAddress { get; set; }
        public decimal InterestAmountPaid { get; set; }
        public decimal RentReceived { get; set; }
        public int ConnectionDetailsID { get; set; }

        public InterestOnHomeLoanLetOut(int interestHomeLoanLetOutId, string lenderName, string propertyAddress, decimal interestAmountPaid, decimal rentReceived, int connectionDetailsID)
        {
            InterestHomeLoanLetOutId = interestHomeLoanLetOutId;
            LenderName = lenderName;
            PropertyAddress = propertyAddress;
            InterestAmountPaid = interestAmountPaid;
            RentReceived = rentReceived;
            ConnectionDetailsID = connectionDetailsID;
        }

        public InterestOnHomeLoanLetOut() { }
    }

    public class DonationToPoliticalPartyTrust : IDonationToPoliticalPartyTrust
    {
        //private int v1;
        //private string? v2;
        //private string? v3;
        //private decimal v4;
        //private DateTime dateTime;
        //private string? v5;
        //private string? v6;

        public int DonationId { get; set; }
        public string RecipientType { get; set; }
        public string RecipientName { get; set; }
        public decimal DonationAmount { get; set; }
        public DateTime DonationDate { get; set; }
        public string PaymentMode { get; set; }
        public string ReceiptNumber { get; set; }
        public int ConnectionDetailsID { get; set; }

        public DonationToPoliticalPartyTrust(int donationId, string recipientType, string recipientName, decimal donationAmount, DateTime donationDate, string paymentMode, string receiptNumber, int connectionDetailsID)
        {
            DonationId = donationId;
            RecipientType = recipientType;
            RecipientName = recipientName;
            DonationAmount = donationAmount;
            DonationDate = donationDate;
            PaymentMode = paymentMode;
            ReceiptNumber = receiptNumber;
            ConnectionDetailsID = connectionDetailsID;
        }

        //public DonationToPoliticalPartyTrust() { }

        //public DonationToPoliticalPartyTrust(int v1, string? v2, string? v3, decimal v4, DateTime dateTime, string? v5, string? v6)
        //{
        //    this.v1 = v1;
        //    this.v2 = v2;
        //    this.v3 = v3;
        //    this.v4 = v4;
        //    this.dateTime = dateTime;
        //    this.v5 = v5;
        //    this.v6 = v6;
        //}
    }

    public class ConveyanceAllowance : IConveyanceAllowance
    {
        public int CAId { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal ExemptAmount { get; set; }
        public bool IsSpeciallyAbled { get; set; }
        public int ConnectionDetailsID { get; set; }

        public ConveyanceAllowance(int caId, decimal amountReceived, decimal exemptAmount, bool isSpeciallyAbled, int connectionDetailsID)
        {
            CAId = caId;
            AmountReceived = amountReceived;
            ExemptAmount = exemptAmount;
            IsSpeciallyAbled = isSpeciallyAbled;
            ConnectionDetailsID = connectionDetailsID;
        }

        public ConveyanceAllowance() { }
    }

    public class TransportAllowanceSpeciallyAbled : ITransportAllowanceSpeciallyAbled
    {
        public int TASpeciallyAbledId { get; set; }
        public string DisabilityType { get; set; }
        public decimal AmountReceived { get; set; }     
        public decimal ExemptAmount { get; set; }
        public int ConnectionDetailsID { get; set; }

        public TransportAllowanceSpeciallyAbled(int taSpeciallyAbledId, string disabilityType, decimal amountReceived, decimal exemptAmount, int connectionDetailsID)
        {
            TASpeciallyAbledId = taSpeciallyAbledId;
            DisabilityType = disabilityType;
            AmountReceived = amountReceived;
            ExemptAmount = exemptAmount;
            ConnectionDetailsID = connectionDetailsID;
        }

        public TransportAllowanceSpeciallyAbled() { }
    }

    public class AgniveerCorpusFund : IAgniveerCorpusFund
    {
        public int AgniveerFundId { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal GovtContribution { get; set; }
        public decimal TotalDeductionClaimed { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int ConnectionDetailsID { get; set; }

        public AgniveerCorpusFund(int agniveerFundId, decimal employeeContribution, decimal govtContribution, decimal totalDeductionClaimed, DateTime enrollmentDate, int connectionDetailsID)
        {
            AgniveerFundId = agniveerFundId;
            EmployeeContribution = employeeContribution;
            GovtContribution = govtContribution;
            TotalDeductionClaimed = totalDeductionClaimed;
            EnrollmentDate = enrollmentDate;
            ConnectionDetailsID = connectionDetailsID;
        }

        public AgniveerCorpusFund() { }
    }

    public class Exemption10C : IExemption10C
    {
        public int Exemption10CId { get; set; }
        public string EmployerName { get; set; }
        public decimal CompensationReceived { get; set; }
        public decimal ExemptAmount { get; set; }
        public DateTime RetirementDate { get; set; }
        public int ConnectionDetailsID { get; set; }

        public Exemption10C(int exemption10CId, string employerName, decimal compensationReceived, decimal exemptAmount, DateTime retirementDate, int connectionDetailsID)
        {
            Exemption10CId = exemption10CId;
            EmployerName = employerName;
            CompensationReceived = compensationReceived;
            ExemptAmount = exemptAmount;
            RetirementDate = retirementDate;
            ConnectionDetailsID = connectionDetailsID;
        }

        public Exemption10C() { }
    }

    public class SavingBankInterest : ISavingBankInterest
    {
        public int BankInterestId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public decimal InterestEarned { get; set; }
        public decimal DeductionClaimed { get; set; }
        public int ConnectionDetailsID { get; set; }

        public SavingBankInterest(int bankInterestId, string bankName, string accountNumber, decimal interestEarned, decimal deductionClaimed, int connectionDetailsID)
        {
            BankInterestId = bankInterestId;
            BankName = bankName;
            AccountNumber = accountNumber;
            InterestEarned = interestEarned;
            DeductionClaimed = deductionClaimed;
            ConnectionDetailsID = connectionDetailsID;
        }

        public SavingBankInterest() { }
    }

    public class Deduction80C : IDeduction80C
    {
        public int Deduction80CId { get; set; }
        public decimal Total80CDeductionClaimed { get; set; }
        public int ConnectionDetailsID { get; set; }

        public Deduction80C(int deduction80CId, decimal total80CDeductionClaimed, int connectionDetailsID)
        {
            Deduction80CId = deduction80CId;
            Total80CDeductionClaimed = total80CDeductionClaimed;
            ConnectionDetailsID = connectionDetailsID;
        }

        public Deduction80C() { }
    }
}
