using System.Data;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{

    public interface IHRAExemptionService
    {
        //IEnumerable<HRAExemption> GetAll();
        HRAExemption GetById(int id);
        void Add(HRAExemption hra);
        void Update(HRAExemption hra);
    }

    public interface IEntertainmentProfessionalTaxService
    {
        void Add(EntertainmentProfessionalTax eta);
        EntertainmentProfessionalTax GetById(int professionalId);
        void Update(EntertainmentProfessionalTax eta);
    }

    public interface IEmployeeContributionToNPSService
    {
        void  Add(EmployeeContributionToNPS Emp);
        EmployeeContributionToNPS GetById(int employeeNpsId);
        void Update(EmployeeContributionToNPS Emp);
    }
    public interface ILTAAllowanceService
    {
        int Add(LTAAllowance model);
        void Update(LTAAllowance model);
        LTAAllowance Get(int id);
    }
    public interface IMedicalInsurance80DService
    {
        int Add(MedicalInsurancePremium80D model);
        void Update(MedicalInsurancePremium80D model);
        MedicalInsurancePremium80D Get(int id);
    }
    public interface IDisabledIndividual80UService
    {
        int Add(DisabledIndividual80U model);
        void Update(DisabledIndividual80U model);
        DisabledIndividual80U Get(int id);
    }
    public interface IInterestOnElectricVehicleLoanService
    {
        int Add(InterestOnElectricVehicleLoan model);
        void Update(InterestOnElectricVehicleLoan model);
        InterestOnElectricVehicleLoan Get(int id);
    }

    public interface IEmployerContributionToNPSService
    {
        int Add(EmployerContributionToNPS model);
        void Update(EmployerContributionToNPS model);
        EmployerContributionToNPS Get(int id);
    }

    public interface IInterestOnHomeLoanSelfService
    {
        int Add(InterestOnHomeLoanSelf model);
        void Update(InterestOnHomeLoanSelf model);
        InterestOnHomeLoanSelf Get(int id);
    }
    public interface IInterestOnHomeLoanLetOutService
    {
        int Add(InterestOnHomeLoanLetOut model);
        void Update(InterestOnHomeLoanLetOut model);
        InterestOnHomeLoanLetOut Get(int id);
    }
    public interface ITransportAllowanceSpeciallyAbledService
    {
        int Add(TransportAllowanceSpeciallyAbled model);
        void Update(TransportAllowanceSpeciallyAbled model);
        TransportAllowanceSpeciallyAbled Get(int id);
    }
    public interface IConveyanceAllowanceService
    {
        int Add(ConveyanceAllowance model);
        void Update(ConveyanceAllowance model);
        ConveyanceAllowance Get(int id);
    }
    public interface IDonationToPoliticalPartyTrustService
    {
        int Add(DonationToPoliticalPartyTrust model);
        void Update(DonationToPoliticalPartyTrust model);
        DonationToPoliticalPartyTrust Get(int id);
    }
    public interface IAgniveerCorpusFundService
    {
        void AddAgniveerCorpusFund(AgniveerCorpusFund fund);
        void UpdateAgniveerCorpusFund(AgniveerCorpusFund fund);
        AgniveerCorpusFund GetAgniveerCorpusFundById(int id);
    }
    public interface IExemption10CService
    {
        void AddExemption10C(Exemption10C model);
        void UpdateExemption10C(Exemption10C model);
        Exemption10C GetExemption10CById(int id);
    }
    public interface ISavingBankInterestService
    {
        void AddSavingBankInterest(SavingBankInterest interest);
        void UpdateSavingBankInterest(SavingBankInterest interest);
        SavingBankInterest GetSavingBankInterestById(int id);
    }
    public interface IDeduction80CService
    {
        void AddDeduction80C(Deduction80C deduction);
        void UpdateDeduction80C(Deduction80C deduction);
        Deduction80C GetDeduction80CById(int id);
    }






}
