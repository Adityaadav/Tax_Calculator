using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{
   
        public interface IIncomeAggregatorService
        {
            Task<Dictionary<string, decimal>> GetIncomeComponentsAsync(int connectionDetailsId);
            Task<BasicDetails> GetBasicDetailsAsync(int TaxPayerID);
            Task<int> GetTaxRegimeIdAsync(int RegimeID);


    }



}
