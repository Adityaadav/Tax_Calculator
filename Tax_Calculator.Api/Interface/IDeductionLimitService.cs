using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{
    
    public interface IDeductionLimitService
    {
        Task<Dictionary<int, DeductionLimit>> GetDeductionLimitsAsync();
    }



}
