namespace Tax_Calculator.Api.HelperClass
{
    public class DeductionInputsDto
    {
        public decimal ActualHRAReceived { get; set; }
        public decimal RentPaidAnnually { get; set; }
        

        public decimal Amount80CCD1 { get; set; }
        public decimal Amount80CCD1B { get; set; }
        public decimal ContributionAmount { get; set; }

        public decimal PremiumPaidSelfFamily { get; set; }
        public decimal PremiumPaidParents { get; set; }
        public decimal PreventiveHealthCheckup { get; set; }

        public decimal InterestOnHousingLoan { get; set; }
        public decimal InterestOnEducationLoan { get; set; }
        public decimal Donations80G { get; set; }
        public decimal DisabledDependent80DD { get; set; }
        public decimal SelfDisability80U { get; set; }
        public decimal SavingsInterest80TTA { get; set; }
        public decimal SeniorCitizenInterest80TTB { get; set; }
        public decimal ProfessionalTaxPaid { get; set; }
        public decimal EntertainmentAllowanceReceived { get; set; }

        public decimal ClaimedAmount { get; set; }
        public decimal ActualTravelExpense { get; set; }
        public decimal InterestAmountPaid { get; set; }
        public decimal InterestAmountPaidSelf { get; set; }

    public decimal InterestAmountPaidLetOut { get; set; }
        public decimal DonationAmount { get; set; }
        public decimal ConveyanceAmountReceived { get; set; }

        public decimal TransportAllowanceReceived { get; set; }

        public decimal ConveyanceExemptAmount { get; set; }

        public decimal GovtCorpusContribution {get; set; }  
        public decimal EmployeeCorpusContribution {get; set; }  

        public decimal ExemptAmount10C { get; set; }    
        public decimal  SavingBankInterestEarned { get; set; }

        public decimal Total80CDeductionClaimed { get; set; }
    }

}
