namespace Tax_Calculator.Interfaces
{

    

        public interface IHRAExemption
        {
            int HRAExemptionID { get; set; }
            decimal ActualHRAReceived { get; set; }
            decimal RentPaidAnnually { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IEntertainmentProfessionalTax
        {
            int ProfessionalId { get; set; }
            decimal ProfessionalTaxPaid { get; set; }
            decimal EntertainmentAllowanceReceived { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IEmployeeContributionToNPS
        {
            int EmployeeNPSID { get; set; }
            decimal Amount80CCD1 { get; set; }
            decimal Amount80CCD1B { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface ILTAAllowance
        {
            int LTAId { get; set; }
            decimal ClaimedAmount { get; set; }
            decimal ActualTravelExpense { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IMedicalInsurancePremium80D
        {
            int Medical80DID { get; set; }
            decimal PremiumPaidSelfFamily { get; set; }
            decimal PreventiveHealthCheckup { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IDisabledIndividual80U
        {
            int Disable80UId { get; set; }
            string DisabilityType { get; set; }
            int DisabilityPercentage { get; set; }
            bool IsSevereDisability { get; set; }
            decimal DeductionClaimed { get; set; }
            string MedicalAuthority { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IInterestOnElectricVehicleLoan
        {
            int ElectricLoanInterestId { get; set; }
            string LenderName { get; set; }
            string LoanAccountNumber { get; set; }
            DateTime LoanSanctionDate { get; set; }
            string ElectricVehicleMakeModel { get; set; }
            decimal InterestAmountPaid { get; set; }
            decimal DeductionClaimed { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IEmployerContributionToNPS
        {
            int EmployerNPSId { get; set; }
            string EmployerName { get; set; }
            decimal ContributionAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IInterestOnHomeLoanSelf
        {
            int InterestHomeLoanSelfId { get; set; }
            string LenderName { get; set; }
            string PropertyAddress { get; set; }
            decimal InterestAmountPaid { get; set; }
            decimal ClaimedAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IInterestOnHomeLoanLetOut
        {
            int InterestHomeLoanLetOutId { get; set; }
            string LenderName { get; set; }
            string PropertyAddress { get; set; }
            decimal InterestAmountPaid { get; set; }
            decimal RentReceived { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IDonationToPoliticalPartyTrust
        {
            int DonationId { get; set; }
            string RecipientType { get; set; }
            string RecipientName { get; set; }
            decimal DonationAmount { get; set; }
            DateTime DonationDate { get; set; }
            string PaymentMode { get; set; }
            string ReceiptNumber { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IConveyanceAllowance
        {
            int CAId { get; set; }
            decimal AmountReceived { get; set; }
            decimal ExemptAmount { get; set; }
            bool IsSpeciallyAbled { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface ITransportAllowanceSpeciallyAbled
        {
            int TASpeciallyAbledId { get; set; }
            string DisabilityType { get; set; }
            decimal AmountReceived { get; set; }
            decimal ExemptAmount { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IAgniveerCorpusFund
        {
            int AgniveerFundId { get; set; }
            decimal EmployeeContribution { get; set; }
            decimal GovtContribution { get; set; }
            decimal TotalDeductionClaimed { get; set; }
            DateTime EnrollmentDate { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IExemption10C
        {
            int Exemption10CId { get; set; }
            string EmployerName { get; set; }
            decimal CompensationReceived { get; set; }
            decimal ExemptAmount { get; set; }
            DateTime RetirementDate { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface ISavingBankInterest
        {
            int BankInterestId { get; set; }
            string BankName { get; set; }
            string AccountNumber { get; set; }
            decimal InterestEarned { get; set; }
            decimal DeductionClaimed { get; set; }
            int ConnectionDetailsID { get; set; }
        }

        public interface IDeduction80C
        {
            int Deduction80CId { get; set; }
            decimal Total80CDeductionClaimed { get; set; }
            int ConnectionDetailsID { get; set; }
        }
    
}
