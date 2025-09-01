using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Interface
{
    public interface IConnectionDetailsService
    {
        Task<int> AddConnectionDetailsAsync(ConnectionDetails model);
        Task<ConnectionDetails> GetConnectionDetailsByIdAsync(int connectionDetailsId);
        Task UpdateConnectionDetailsAsync(ConnectionDetails model);
    }

}
