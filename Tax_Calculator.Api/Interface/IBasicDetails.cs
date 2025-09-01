using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{
    public interface IBasicDetailsService
    {
        void AddBasicDetails(BasicDetails details);
        void UpdateBasicDetails(BasicDetails details);
        BasicDetails GetBasicDetailsById(int id);
    }

    public interface ITaxPayerBankAccountService
    {
        void AddTaxPayerBankAccount(TaxPayerBankAccount account);
        void UpdateTaxPayerBankAccount(TaxPayerBankAccount account);
        TaxPayerBankAccount GetTaxPayerBankAccountById(int id);
    }
    public interface ITaxPeriodService
    {
        void AddTaxPeriod(TaxPeriod period);
        void UpdateTaxPeriod(TaxPeriod period);
        TaxPeriod GetTaxPeriodById(int id);
    }

    public interface IResidentStatusService
    {
        void AddResidentStatus(ResidentStatus status);
        void UpdateResidentStatus(ResidentStatus status);
        ResidentStatus GetResidentStatusById(int id);
    }

    public interface ITaxRegimeService
    {
        void AddTaxRegime(TaxRegime regime);
        void UpdateTaxRegime(TaxRegime regime);
        TaxRegime GetTaxRegimeById(int id);
    }

}
