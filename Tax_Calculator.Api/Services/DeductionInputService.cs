using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Api.Interface;
namespace Tax_Calculator.Api.Services
{
    public class DeductionAggregatorService1 : IDeductionAggregatorService1
    {
        private readonly string _connectionString;

        public DeductionAggregatorService1(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DeductionInputsDto> GetDeductionsAsync(int connectionDetailsId)
        {
            DeductionInputsDto deduction = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("usp_GetEmployeeDeductionsByConnectionsByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ConnectionDetailsID", connectionDetailsId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            deduction = new DeductionInputsDto
                            {
                                
                                ActualHRAReceived = reader["ActualHRAReceived"] as decimal? ?? 0m,
                                RentPaidAnnually = reader["RentPaidAnnually"] as decimal? ?? 0m,

                                
                                Amount80CCD1 = reader["Amount80CCD1"] as decimal? ?? 0m,
                                Amount80CCD1B = reader["Amount80CCD1B"] as decimal? ?? 0m,
                                ContributionAmount = reader["ContributionAmount"] as decimal? ?? 0m,

                                
                                PremiumPaidSelfFamily = reader["PremiumPaidSelfFamily"] as decimal? ?? 0m,
                                PreventiveHealthCheckup = reader["PreventiveHealthCheckup"] as decimal? ?? 0m,

                                
                                InterestAmountPaidSelf = reader["InterestAmountPaidSelf"] as decimal? ?? 0m,
                                InterestAmountPaidLetOut = reader["InterestAmountPaidLetOut"] as decimal? ?? 0m,

                                
                                Donations80G = reader["Donations80G"] as decimal? ?? 0m,
                                DisabledDependent80DD = reader["DisabledDependent80DD"] as decimal? ?? 0m,
                                SelfDisability80U = reader["SelfDisability80U"] as decimal? ?? 0m,

                                
                                SavingsInterest80TTA = reader["SavingsInterest80TTA"] as decimal? ?? 0m,
                                ProfessionalTaxPaid = reader["ProfessionalTaxPaid"] as decimal? ?? 0m,

                                
                                EntertainmentAllowanceReceived = reader["EntertainmentAllowanceReceived"] as decimal? ?? 0m,
                                ActualTravelExpense = reader["ActualTravelExpense"] as decimal? ?? 0m,
                                ConveyanceAmountReceived = reader["ConveyanceAmountReceived"] as decimal? ?? 0m,
                                ConveyanceExemptAmount = reader["ConveyanceExemptAmount"] as decimal? ?? 0m,
                                TransportAllowanceReceived = reader["TransportAllowanceReceived"] as decimal? ?? 0m,

                                GovtCorpusContribution = reader["GovtCorpusContribution"] as decimal? ?? 0m,
                                EmployeeCorpusContribution = reader["EmployeeCorpusContribution"] as decimal? ?? 0m,

                                ExemptAmount10C = reader["ExemptAmount10C"] as decimal? ?? 0m,

                                Total80CDeductionClaimed = reader["Total80CDeductionClaimed"] as decimal? ?? 0m
                            };
                        }
                    }
                }
            }

            return deduction;
        }
    }



}
