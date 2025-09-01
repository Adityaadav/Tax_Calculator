using Tax_Calculator.Api.HelperClass;

namespace Tax_Calculator.Api.Interface
{
    public interface IDeductionAggregatorService1
    {
        Task<DeductionInputsDto> GetDeductionsAsync(int connectionDetailsId);
    }


}
