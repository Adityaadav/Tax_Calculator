namespace Tax_Calculator.Interfaces
{

    public interface ITaxFilingMain :
        IBasicDetails,
        ITaxPayerBankAccount,

        // Income Interfaces
        IIncomeFromSalary,
        IIncomeFromInterest,
        IncomeFromDigital,
        RentalIncome,
        InterestHomeLoanLetout,

        // Deduction Interfaces
        IHRAExemption,
        IEntertainmentProfessionalTax,
        IEmployeeContributionToNPS,
        IEmployerContributionToNPS,
        ILTAAllowance,
        IMedicalInsurancePremium80D,
        IDisabledIndividual80U,
        IInterestOnElectricVehicleLoan,
        IInterestOnHomeLoanSelf,
        IInterestOnHomeLoanLetOut,
        IDonationToPoliticalPartyTrust,
        IConveyanceAllowance,
        ITransportAllowanceSpeciallyAbled,
        IAgniveerCorpusFund,
        IExemption10C,
        ISavingBankInterest,
        IDeduction80C
    {
        int FilingID { get; set; }
        int TaxYearID { get; set; }
        int ResidentStatusID { get; set; }
        int RegimeID { get; set; }
    }
}
