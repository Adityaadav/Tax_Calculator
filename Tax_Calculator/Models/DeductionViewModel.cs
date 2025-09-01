namespace Tax_Calculator.Models
{
    public class DeductionsViewModel
    {
        public HRAExemption HRA { get; set; } = new HRAExemption();
        public EntertainmentProfessionalTax Entertainment { get; set; } = new EntertainmentProfessionalTax();
        public EmployeeContributionToNPS EmployeeNPS { get; set; } = new EmployeeContributionToNPS();
        public LTAAllowance LTA { get; set; } = new LTAAllowance();
        public MedicalInsurancePremium80D Medical { get; set; } = new MedicalInsurancePremium80D();
        public DisabledIndividual80U Disabled { get; set; } = new DisabledIndividual80U();
        public InterestOnElectricVehicleLoan ElectricVehicleLoan { get; set; } = new InterestOnElectricVehicleLoan();
        public EmployerContributionToNPS EmployerNPS { get; set; } = new EmployerContributionToNPS();
        public InterestOnHomeLoanSelf HomeLoanSelf { get; set; } = new InterestOnHomeLoanSelf();
        public InterestOnHomeLoanLetOut HomeLoanLetOut { get; set; } = new InterestOnHomeLoanLetOut();
        public DonationToPoliticalPartyTrust Donation { get; set; } = new DonationToPoliticalPartyTrust(0, "", "", 0, DateTime.Now, "", "", 0);
        public ConveyanceAllowance Conveyance { get; set; } = new ConveyanceAllowance();
        public TransportAllowanceSpeciallyAbled TransportAllowance { get; set; } = new TransportAllowanceSpeciallyAbled();
        public AgniveerCorpusFund Agniveer { get; set; } = new AgniveerCorpusFund();
        public Exemption10C Exemption10C { get; set; } = new Exemption10C();
        public SavingBankInterest SavingBankInterest { get; set; } = new SavingBankInterest();
        public Deduction80C Deduction80C { get; set; } = new Deduction80C();
    }

}
