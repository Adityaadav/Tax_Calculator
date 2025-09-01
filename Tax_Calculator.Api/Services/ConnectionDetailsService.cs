using Microsoft.Data.SqlClient;
using System.Data;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Services
{
    public class ConnectionDetailsService : IConnectionDetailsService
    {
        private readonly string _connectionString;

        public ConnectionDetailsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddConnectionDetailsAsync(ConnectionDetails model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                if (!await ExistsAsync(conn, "TaxCal.Basic_Details", "Tax_Payer_Id", model.TaxPayerID))
                    throw new Exception("Invalid TaxPayerID");

                if (!await ExistsAsync(conn, "TaxCal.TaxPeriods", "TaxYearID", model.TaxYearID))
                    throw new Exception("Invalid TaxYearID");

                if (!await ExistsAsync(conn, "TaxCal.ResidentStatus", "ResidentStatusID", model.ResidentStatusID))
                    throw new Exception("Invalid ResidentStatusID");

                if (!await ExistsAsync(conn, "TaxCal.TaxRegimes", "RegimeID", model.TaxRegimeID))
                    throw new Exception("Invalid TaxRegimeID");

                using (SqlCommand cmd = new SqlCommand("TaxCal.AddConnectionDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaxPayerID", model.TaxPayerID);
                    cmd.Parameters.AddWithValue("@TaxYearID", model.TaxYearID);
                    cmd.Parameters.AddWithValue("@ResidentStatusID", model.ResidentStatusID);
                    cmd.Parameters.AddWithValue("@TaxRegimeID", model.TaxRegimeID);

                    var outputId = new SqlParameter("@ConnectionDetailsID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputId);

                    await cmd.ExecuteNonQueryAsync();
                    return (int)outputId.Value;
                }
            }
        }

        public async Task<ConnectionDetails> GetConnectionDetailsByIdAsync(int connectionDetailsId)
        {
            ConnectionDetails details = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("TaxCal.GetConnectionDetailsById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConnectionDetailsID", connectionDetailsId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            details = new ConnectionDetails
                            {
                                ConnectionDetailsID = Convert.ToInt32(reader["ConnectionDetailsID"]),
                                TaxPayerID = Convert.ToInt32(reader["TaxPayerID"]),
                                TaxYearID = Convert.ToInt32(reader["TaxYearID"]),
                                ResidentStatusID = Convert.ToInt32(reader["ResidentStatusID"]),
                                TaxRegimeID = Convert.ToInt32(reader["TaxRegimeID"])
                            };
                        }
                    }
                }
            }

            return details;
        }

        public async Task UpdateConnectionDetailsAsync(ConnectionDetails model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateConnectionDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConnectionDetailsID", model.ConnectionDetailsID);
                    cmd.Parameters.AddWithValue("@TaxPayerID", model.TaxPayerID);
                    cmd.Parameters.AddWithValue("@TaxYearID", model.TaxYearID);
                    cmd.Parameters.AddWithValue("@ResidentStatusID", model.ResidentStatusID);
                    cmd.Parameters.AddWithValue("@TaxRegimeID", model.TaxRegimeID);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<bool> ExistsAsync(SqlConnection conn, string table, string column, int value)
        {
            string query = $"SELECT COUNT(*) FROM {table} WHERE {column} = @Value";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Value", value);
                var count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
        }
    }

}
