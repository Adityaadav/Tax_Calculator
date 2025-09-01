using Microsoft.Data.SqlClient;
using System.Data;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Services
{
    public class IncomeAggregatorService : IIncomeAggregatorService
    {
        private readonly string _connectionString;

        public IncomeAggregatorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Dictionary<string, decimal>> GetIncomeComponentsAsync(int connectionDetailsId)
        {
            var income = new Dictionary<string, decimal>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("TaxCal.GetIncomeComponentsByConnectionId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConnectionDetailsID", connectionDetailsId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string code = reader["IncomeCode"].ToString();
                            decimal amount = Convert.ToDecimal(reader["Amount"]);
                            income[code] = amount;
                        }
                    }
                }
            }

            return income;
        }


        public async Task<BasicDetails> GetBasicDetailsAsync(int Tax_Payer_Id)
        {
            BasicDetails details = new BasicDetails();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("TaxCal.GetBasicDetailsById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Tax_Payer_Id", Tax_Payer_Id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            details.Tax_Payer_Id = Tax_Payer_Id;
                            details.DOB = Convert.ToDateTime(reader["DOB"]);
                            details.First_Name = reader["First_Name"].ToString();
                            details.Last_Name = reader["Last_Name"].ToString();
                            details.PAN_NUMBER = reader["PAN_NUMBER"].ToString();
                        }
                    }
                }
            }

            return details;
        }

        public async Task<int> GetTaxRegimeIdAsync(int RegimeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("TaxCal.GetTaxRegimeById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RegimeID", RegimeID);

                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && int.TryParse(result.ToString(), out int regimeId))
                    {
                        return regimeId;
                    }
                    else
                    {
                        throw new Exception("TaxRegimeId not found for the given TaxFilingId.");
                    }
                }
            }
        }



    }
}