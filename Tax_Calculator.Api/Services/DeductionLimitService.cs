using Microsoft.Data.SqlClient;
using System.Data;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Services
{
    public class DeductionLimitService : IDeductionLimitService
    {
        private readonly string _connectionString;

        public DeductionLimitService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Dictionary<int, DeductionLimit>> GetDeductionLimitsAsync()
        {
            var limits = new Dictionary<int, DeductionLimit>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetStandardDeductionLimits", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var limit = new DeductionLimit
                        {
                            DeductionId = Convert.ToInt32(reader["DeductionId"]),
                            DeductionCode = reader["DeductionCode"]?.ToString()?.Trim(),
                            MaximumAmount = reader["MaximumAmount"] == DBNull.Value
                                ? null
                                : (decimal?)Convert.ToDecimal(reader["MaximumAmount"]),
                            PercentageOfSalary = reader["PercentageOfSalary"] == DBNull.Value
                                ? null
                                : (decimal?)Convert.ToDecimal(reader["PercentageOfSalary"])
                        };

                        limits[limit.DeductionId] = limit;
                    }
                }
            }

            return limits;
        }

    }


}
