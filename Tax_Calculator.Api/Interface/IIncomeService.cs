
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{
    public interface IIncomeFromSalaryService

    {
        IncomeFromSalary GetById(int id);
        void Add(IncomeFromSalary salary);
        void Update(IncomeFromSalary salary);
        void Delete(int id);

    }
    public interface IDigitalAssetIncomeService
    {
        DigitalAssetIncome GetById(int id);
        void Add(DigitalAssetIncome digitalIncome);
        void Update(DigitalAssetIncome digitalIncome);
        void Delete(int id);
    }

    public interface IIncomeFromRentService
    {
        IncomeFromRent GetById(int id);
        void Add(IncomeFromRent rent);
        void Update(IncomeFromRent rent);
        void Delete(int id);
    }

    public interface IHomeLoanLetoutService
    {
        HomeLoanLetoutInterest GetById(int id);
        void Add(HomeLoanLetoutInterest homeLoan);
        void Update(HomeLoanLetoutInterest homeLoan);
        void Delete(int id);
    }
    public interface IIncomeFromInterestService
    {
        IncomeFromInterest GetById(int id);
        void Add(IncomeFromInterest interest);
        void Update(IncomeFromInterest interest);
        void Delete(int id);
    }   
}
