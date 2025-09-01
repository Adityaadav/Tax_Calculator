using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.PortableExecutable;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Interfaces;
using Tax_Calculator.Models;


namespace Tax_Calculator.Api.Services
{

    public class HRAExemptionService : IHRAExemptionService
    {
        private readonly string _connectionString;

        public HRAExemptionService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //public IEnumerable<HRAExemption> GetAll()
        //{
        //    var list = new List<HRAExemption>();
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    using (SqlCommand cmd = new SqlCommand("TaxCal.GetAllHRAExemptions", conn))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ConnectionDetialsID", ConnectionDetailsID);
        //        conn.Open();
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            list.Add(new HRAExemption(
        //                Convert.ToInt32(reader["HRAExemptionID"]),
        //                Convert.ToDecimal(reader["ActualHRAReceived"]),
        //                Convert.ToDecimal(reader["RentPaidAnnually"]),
        //                Convert.ToInt32(reader["ConnectionDetailsID"])
        //            ));
        //        }
        //    }
        //    return list;
        //}

        public HRAExemption GetById(int ConnectionDetailsID)
        {
            HRAExemption hra = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetHRAExemptionById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hra = new HRAExemption(
                        Convert.ToInt32(reader["HRAExemptionID"]),
                        Convert.ToDecimal(reader["ActualHRAReceived"]),
                        Convert.ToDecimal(reader["RentPaidAnnually"]),
                        Convert.ToInt32(reader["ConnectionDetailsID"])
                    );
                }
            }
            return hra;
        }

        public void Add(HRAExemption hra)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.AddHRAExemption", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActualHRAReceived", hra.ActualHRAReceived);
                cmd.Parameters.AddWithValue("@RentPaidAnnually", hra.RentPaidAnnually);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", hra.ConnectionDetailsID); // Adjusted to match the interface
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(HRAExemption hra)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateHRAExemption", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@HRAExemptionID", hra.HRAExemptionID);
                cmd.Parameters.AddWithValue("@ActualHRAReceived", hra.ActualHRAReceived);
                cmd.Parameters.AddWithValue("@RentPaidAnnually", hra.RentPaidAnnually);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", hra.ConnectionDetailsID); // Adjusted to match the interface
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class EntertainmentProfessionalTaxService : IEntertainmentProfessionalTaxService
    {
        private readonly string _connectionString;

        public EntertainmentProfessionalTaxService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Add new entry
        public void Add(EntertainmentProfessionalTax eta)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.sp_InsertOrUpdate_EntertainmentProfessionalTax", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProfessionalTaxPaid", eta.ProfessionalTaxPaid);
                cmd.Parameters.AddWithValue("@EntertainmentAllowanceReceived", eta.EntertainmentAllowanceReceived);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", eta.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Get by ConnectionDetailsID
        public EntertainmentProfessionalTax GetById(int ConnectionDetailsID)
        {
            EntertainmentProfessionalTax EPT = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Get_Entertainment_Professional_Tax_ById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    EPT = new EntertainmentProfessionalTax(

                         Convert.ToInt32(reader["ProfessionalId"]),
                        Convert.ToDecimal(reader["ProfessionalTaxPaid"]),
                        Convert.ToDecimal(reader["EntertainmentAllowanceReceived"]),
                        Convert.ToInt32(reader["ConnectionDetailsID"])

                        );
                }
                //using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                //{
                //    //DataTable table = new DataTable();
                //    //adapter.Fill(table);
                //    //return table;

                //    if
                //}
            
            
            }
            return EPT;

        }

        // Insert or update based on existence
        public void Update(EntertainmentProfessionalTax eta)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.sp_InsertOrUpdate_EntertainmentProfessionalTax", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProfessionalTaxPaid", eta.ProfessionalTaxPaid);
                cmd.Parameters.AddWithValue("@EntertainmentAllowanceReceived", eta.EntertainmentAllowanceReceived);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", eta.ConnectionDetailsID); // note: no "s" at end of DetailId

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class EmployeeContributionToNPSService : IEmployeeContributionToNPSService
    {
        private readonly string _connectionString;

        public EmployeeContributionToNPSService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Add(EmployeeContributionToNPS emp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Add_Employee_Contribution_to_NPS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Amount80CCD1", emp.Amount80CCD1);
                cmd.Parameters.AddWithValue("@Amount80CCD1B", emp.Amount80CCD1B);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", emp.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public EmployeeContributionToNPS GetById(int ConnectionDetailsID)
        {
            EmployeeContributionToNPS ECN = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Get_Employee_Contribution_to_NPS_ById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ECN = new EmployeeContributionToNPS(
                         Convert.ToInt32(reader["EmployeeNPSID"]),
                        Convert.ToDecimal(reader["Amount80CCD1"]),
                        Convert.ToDecimal(reader["Amount80CCD1B"]),
                        Convert.ToInt32(reader["ConnectionDetailsID"])

                        );
                }
                
            }
            return ECN;
        }

        public void Update(EmployeeContributionToNPS emp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Update_Employee_Contribution_to_NPS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@EmployeeNPSID", emp.EmployeeNPSID);
                cmd.Parameters.AddWithValue("@Amount80CCD1", emp.Amount80CCD1);
                cmd.Parameters.AddWithValue("@Amount80CCD1B", emp.Amount80CCD1B);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", emp.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class LTAAllowanceService : ILTAAllowanceService
    {
        private readonly string _connectionString;

        public LTAAllowanceService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(LTAAllowance model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertLTAAllowance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@ClaimedAmount", model.ClaimedAmount);
                cmd.Parameters.AddWithValue("@ActualTravelExpense", model.ActualTravelExpense);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(LTAAllowance model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateLTAAllowance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@ClaimedAmount", model.ClaimedAmount);
                cmd.Parameters.AddWithValue("@ActualTravelExpense", model.ActualTravelExpense);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public LTAAllowance Get(int ConnectionDetailsID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetLTAAllowance", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID );

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new LTAAllowance
                        {
                            LTAId = Convert.ToInt32(reader["LTAId"]),
                            ConnectionDetailsID = Convert.ToInt32(reader["ConnectionDetailsId"]),
                            ClaimedAmount = Convert.ToDecimal(reader["ClaimedAmount"]),
                            ActualTravelExpense = Convert.ToDecimal(reader["ActualTravelExpense"])
                        };
                    }
                }
            }
            return null;
        }
    }

    public class MedicalInsurance80DService : IMedicalInsurance80DService
    {
        private readonly string _connectionString;

        public MedicalInsurance80DService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(MedicalInsurancePremium80D model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertMedicalInsurance80D", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailsId", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@PremiumPaidSelfFamily", model.PremiumPaidSelfFamily);
                cmd.Parameters.AddWithValue("@PreventiveHealthCheckup", model.PreventiveHealthCheckup);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(MedicalInsurancePremium80D model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateMedicalInsurance80D", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Medical80DID", model.Medical80DID);
                cmd.Parameters.AddWithValue("@ConnectionDetailsId", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@PremiumPaidSelfFamily", model.PremiumPaidSelfFamily);
                cmd.Parameters.AddWithValue("@PreventiveHealthCheckup", model.PreventiveHealthCheckup);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public MedicalInsurancePremium80D Get(int ConnectionDetailsID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetMedicalInsurance80D", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID );
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new MedicalInsurancePremium80D
                        {
                            Medical80DID = (int)reader["Medical80DID"],
                            ConnectionDetailsID = (int)reader["ConnectionDetailsId"],
                            PremiumPaidSelfFamily = (decimal)reader["PremiumPaidSelfFamily"],
                            PreventiveHealthCheckup = (decimal)reader["PreventiveHealthCheckup"]
                        };
                    }
                }
            }
            return null;
        }
    }

    public class DisabledIndividual80UService : IDisabledIndividual80UService
    {
        private readonly string _connectionString;

        public DisabledIndividual80UService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public int Add(DisabledIndividual80U model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.sp_Insert_DisabledIndividual80U", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailId", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@DisabilityType", model.DisabilityType);
                cmd.Parameters.AddWithValue("@DisabilityPercentage", model.DisabilityPercentage);
                cmd.Parameters.AddWithValue("@IsSevereDisability", model.IsSevereDisability);
                cmd.Parameters.AddWithValue("@DeductionClaimed", model.DeductionClaimed);
                cmd.Parameters.AddWithValue("@MedicalAuthority", model.MedicalAuthority);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(DisabledIndividual80U model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.sp_Update_DisabledIndividual80U", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Disable80UId", model.Disable80UId);
                //cmd.Parameters.AddWithValue("@ConnectionDetailsId", model.ConnectionDetailsId);
                cmd.Parameters.AddWithValue("@DisabilityType", model.DisabilityType);
                cmd.Parameters.AddWithValue("@DisabilityPercentage", model.DisabilityPercentage);
                cmd.Parameters.AddWithValue("@IsSevereDisability", model.IsSevereDisability);
                cmd.Parameters.AddWithValue("@DeductionClaimed", model.DeductionClaimed);
                cmd.Parameters.AddWithValue("@MedicalAuthority", model.MedicalAuthority);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DisabledIndividual80U Get(int ConnectionDetailsID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.sp_Get_DisabledIndividual80U_ById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID );
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DisabledIndividual80U
                        {
                            Disable80UId = (int)reader["Disable80UId"],
                            ConnectionDetailsID = (int)reader["ConnectionDetailsId"],
                            DisabilityType = reader["DisabilityType"].ToString(),
                            DisabilityPercentage = (int)reader["DisabilityPercentage"],
                            IsSevereDisability = (bool)reader["IsSevereDisability"],
                            DeductionClaimed = (decimal)reader["DeductionClaimed"],
                            MedicalAuthority = reader["MedicalAuthority"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }

    public class InterestOnElectricVehicleLoanService : IInterestOnElectricVehicleLoanService
    {
        private readonly string _connectionString;

        public InterestOnElectricVehicleLoanService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(InterestOnElectricVehicleLoan model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertElectricVehicleLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@LoanAccountNumber", model.LoanAccountNumber);
                cmd.Parameters.AddWithValue("@LoanSanctionDate", model.LoanSanctionDate);
                cmd.Parameters.AddWithValue("@ElectricVehicleMakeModel", model.ElectricVehicleMakeModel);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@DeductionClaimed", model.DeductionClaimed);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(InterestOnElectricVehicleLoan model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateInterestOnElectricVehicleLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ElectricLoanInterestId", model.ElectricLoanInterestId);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@LoanAccountNumber", model.LoanAccountNumber);
                cmd.Parameters.AddWithValue("@LoanSanctionDate", model.LoanSanctionDate);
                cmd.Parameters.AddWithValue("@ElectricVehicleMakeModel", model.ElectricVehicleMakeModel);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@DeductionClaimed", model.DeductionClaimed);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public InterestOnElectricVehicleLoan Get(int ConnectionDetailsID)
        {
            InterestOnElectricVehicleLoan model = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetElectricVehicleLoan", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID );

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.ElectricLoanInterestId = (int)reader["ElectricLoanInterestId"];
                        model.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
                        model.LenderName = reader["LenderName"].ToString();
                        model.LoanAccountNumber = reader["LoanAccountNumber"].ToString();
                        model.LoanSanctionDate = (DateTime)reader["LoanSanctionDate"];
                        model.ElectricVehicleMakeModel = reader["ElectricVehicleMakeModel"].ToString();
                        model.InterestAmountPaid = (decimal)reader["InterestAmountPaid"];
                        model.DeductionClaimed = (decimal)reader["DeductionClaimed"];
                    }
                }
            }

            return model;
        }
    }


    public class EmployerContributionToNPSService : IEmployerContributionToNPSService
    {
        private readonly string _connectionString;

        public EmployerContributionToNPSService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(EmployerContributionToNPS model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertEmployerNPS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@EmployerName", model.EmployerName);
                cmd.Parameters.AddWithValue("@ContributionAmount", model.ContributionAmount);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(EmployerContributionToNPS model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateEmployerContributionToNPS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@EmployerNPSId", model.EmployerNPSId);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@EmployerName", model.EmployerName);
                cmd.Parameters.AddWithValue("@ContributionAmount", model.ContributionAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public EmployerContributionToNPS Get(int ConnectionDetailsID)
        {
            EmployerContributionToNPS model = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetEmployerNPS", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.EmployerNPSId = (int)reader["EmployerNPSId"];
                        model.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
                        model.EmployerName = reader["EmployerName"].ToString();
                        model.ContributionAmount = (decimal)reader["ContributionAmount"];
                    }
                }
            }

            return model;
        }
    }

    public class InterestOnHomeLoanSelfService : IInterestOnHomeLoanSelfService
    {
        private readonly string _connectionString;

        public InterestOnHomeLoanSelfService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(InterestOnHomeLoanSelf model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertHomeLoanSelf", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@")
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@PropertyAddress", model.PropertyAddress);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@ClaimedAmount", model.ClaimedAmount);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(InterestOnHomeLoanSelf model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateHomeLoanSelf", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@InterestHomeLoanSelfId", model.InterestHomeLoanSelfId);
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@PropertyAddress", model.PropertyAddress);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@ClaimedAmount", model.ClaimedAmount);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public InterestOnHomeLoanSelf Get(int ConnectionDetailsID)
        {
            InterestOnHomeLoanSelf model = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetHomeLoanSelf", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model.InterestHomeLoanSelfId = (int)reader["InterestHomeLoanSelfId"];
                        model.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
                        model.LenderName = reader["LenderName"].ToString();
                        model.PropertyAddress = reader["PropertyAddress"].ToString();
                        model.InterestAmountPaid = (decimal)reader["InterestAmountPaid"];
                        model.ClaimedAmount = (decimal)reader["ClaimedAmount"];
                    }
                }
            }

            return model;
        }
    }


    public class InterestOnHomeLoanLetOutService : IInterestOnHomeLoanLetOutService
    {
        private readonly string _connectionString;

        public InterestOnHomeLoanLetOutService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public int Add(InterestOnHomeLoanLetOut model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.AddInterestOnHomeLoanLetOut", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailId", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@PropertyAddress", model.PropertyAddress);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@RentReceived", model.RentReceived);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public void Update(InterestOnHomeLoanLetOut model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateInterestOnHomeLoanLetOut", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@InterestHomeLoanLetOutId", model.InterestHomeLoanLetOutId);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@LenderName", model.LenderName);
                cmd.Parameters.AddWithValue("@PropertyAddress", model.PropertyAddress);
                cmd.Parameters.AddWithValue("@InterestAmountPaid", model.InterestAmountPaid);
                cmd.Parameters.AddWithValue("@RentReceived", model.RentReceived);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public InterestOnHomeLoanLetOut Get(int ConnectionDetailsID)
        {
            InterestOnHomeLoanLetOut result = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetInterestOnHomeLoanLetOutById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new InterestOnHomeLoanLetOut();
                        result.InterestHomeLoanLetOutId = (int)reader["InterestHomeLoanLetOutId"];
                        result.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
                        result.LenderName = reader["LenderName"].ToString();
                        result.PropertyAddress = reader["PropertyAddress"].ToString();
                        result.InterestAmountPaid = (decimal)reader["InterestAmountPaid"];
                        result.RentReceived = (decimal)reader["RentReceived"];
                    }

                }
            }

            return result;
        }
    }

    public class TransportAllowanceSpeciallyAbledService : ITransportAllowanceSpeciallyAbledService
    {
        private readonly string _connectionString;

        public TransportAllowanceSpeciallyAbledService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public int Add(TransportAllowanceSpeciallyAbled model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertTransportAllowanceSpeciallyAbled", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@DisabilityType", model.DisabilityType);
                cmd.Parameters.AddWithValue("@AmountReceived", model.AmountReceived);
                cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(TransportAllowanceSpeciallyAbled model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateTransportAllowanceSpeciallyAbled", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@TASpeciallyAbledId", model.TASpeciallyAbledId);
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@DisabilityType", model.DisabilityType);
                cmd.Parameters.AddWithValue("@AmountReceived", model.AmountReceived);
                cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TransportAllowanceSpeciallyAbled Get(int ConnectionDetailsID)
        {
            TransportAllowanceSpeciallyAbled model = null; // Initialize the model to null
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetTransportAllowanceSpeciallyAbled", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID); // Corrected parameter name to match the expected stored procedure input

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // Ensure 'reader' is declared in the correct context
                {
                    if (reader.Read())
                    {
                        model = new TransportAllowanceSpeciallyAbled
                        {
                            TASpeciallyAbledId = (int)reader["TASpeciallyAbledId"],
                            ConnectionDetailsID = (int)reader["ConnectionDetailsID"],
                            DisabilityType = reader["DisabilityType"].ToString(),
                            AmountReceived = (decimal)reader["AmountReceived"],
                            ExemptAmount = (decimal)reader["ExemptAmount"]
                        };
                    }
                }
            }
            return model;
        }
    }

    public class ConveyanceAllowanceService : IConveyanceAllowanceService
    {
        private readonly string _connectionString;

        public bool IsSpeciallyAbled { get; private set; }

        public ConveyanceAllowanceService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int Add(ConveyanceAllowance model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertConveyanceAllowance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AmountReceived", model.AmountReceived);
                cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);
                cmd.Parameters.AddWithValue("@IsSpeciallyAbled", model.IsSpeciallyAbled);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", model.ConnectionDetailsID);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(ConveyanceAllowance model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateConveyanceAllowance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@CAId", model.CAId);
                cmd.Parameters.AddWithValue("@AmountReceived", model.AmountReceived);
                cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);
                cmd.Parameters.AddWithValue("@IsSpeciallyAbled", model.IsSpeciallyAbled);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", model.ConnectionDetailsID);


                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ConveyanceAllowance Get(int ConnectionDetailsID)
        {
            ConveyanceAllowance model = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetConveyanceAllowance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model = new ConveyanceAllowance();
                        model.CAId = (int)reader["CAId"];
                        model.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
                        model.AmountReceived = (decimal)reader["AmountReceived"];
                        model.ExemptAmount = (decimal)reader["ExemptAmount"];
                        model.IsSpeciallyAbled = (bool)reader["IsSpeciallyAbled"];
                    }
                }
            }
            return model;
        }
    }
    //ConveyanceAllowance model = null;

    //if (reader.Read())
    //{
    //    model = new ConveyanceAllowance();
    //    model.CAId = (int)reader["CAId"];
    //    model.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
    //    model.AmountReceived = (decimal)reader["AmountReceived"];
    //    model.ExemptAmount = (decimal)reader["ExemptAmount"];
    //    model.IsSpeciallyAbled = (bool)reader["IsSpeciallyAbled"];
    //}


    public class DonationToPoliticalPartyTrustService : IDonationToPoliticalPartyTrustService
    {
        private readonly string _connectionString;

        public DonationToPoliticalPartyTrustService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public int Add(DonationToPoliticalPartyTrust model)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_InsertDonationToPoliticalPartyTrust", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@DonationId", model.DonationId);
                cmd.Parameters.AddWithValue("@RecipientType", model.RecipientType);
                cmd.Parameters.AddWithValue("@RecipientName", model.RecipientName);
                cmd.Parameters.AddWithValue("@DonationAmount", model.DonationAmount);
                cmd.Parameters.AddWithValue("@DonationDate", model.DonationDate);
                cmd.Parameters.AddWithValue("@PaymentMode", model.PaymentMode);
                cmd.Parameters.AddWithValue("@ReceiptNumber", model.ReceiptNumber);
                cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);

                con.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }

        public void Update(DonationToPoliticalPartyTrust model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_UpdateDonationToPoliticalPartyTrust", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecipientType", model.RecipientType);
                cmd.Parameters.AddWithValue("@RecipientName", model.RecipientName);
                cmd.Parameters.AddWithValue("@DonationAmount", model.DonationAmount);
                cmd.Parameters.AddWithValue("@DonationDate", model.DonationDate);
                cmd.Parameters.AddWithValue("@PaymentMode", model.PaymentMode);
                cmd.Parameters.AddWithValue("@ReceiptNumber", model.ReceiptNumber);
                cmd.Parameters.AddWithValue("ConnectionDetailID", model.ConnectionDetailsID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DonationToPoliticalPartyTrust Get(int ConnectionDetailsID)
        {
            DonationToPoliticalPartyTrust model = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_GetDonationToPoliticalPartyTrust", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        model = new DonationToPoliticalPartyTrust(
                            (int)reader["DonationId"],
                            reader["RecipientType"].ToString(),
                            reader["RecipientName"].ToString(),
                            (decimal)reader["DonationAmount"],
                            (DateTime)reader["DonationDate"],
                            reader["PaymentMode"].ToString(),
                            reader["ReceiptNumber"].ToString(),
                            (int)reader["ConnectionDetailsID"]
                        );
                    }
                }
            }
            return model;
        }
    }
    public class AgniveerCorpusFundService : IAgniveerCorpusFundService
    {
        private readonly string _connectionString;

        public AgniveerCorpusFundService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddAgniveerCorpusFund(AgniveerCorpusFund fund)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.AddAgniveerCorpusFund", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeContribution", fund.EmployeeContribution);
            cmd.Parameters.AddWithValue("@GovtContribution", fund.GovtContribution);
            cmd.Parameters.AddWithValue("@TotalDeductionClaimed", fund.TotalDeductionClaimed);
            cmd.Parameters.AddWithValue("@EnrollmentDate", fund.EnrollmentDate);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", fund.ConnectionDetailsID);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateAgniveerCorpusFund(AgniveerCorpusFund fund)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.UpdateAgniveerCorpusFund", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ConnectionDetailId", fund.ConnectionDetailsID);
            //cmd.Parameters.AddWithValue("@AgniveerFundId", fund.AgniveerFundId);
            cmd.Parameters.AddWithValue("@EmployeeContribution", fund.EmployeeContribution);
            cmd.Parameters.AddWithValue("@GovtContribution", fund.GovtContribution);
            cmd.Parameters.AddWithValue("@TotalDeductionClaimed", fund.TotalDeductionClaimed);
            cmd.Parameters.AddWithValue("@EnrollmentDate", fund.EnrollmentDate);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public AgniveerCorpusFund GetAgniveerCorpusFundById(int id)
        {
            AgniveerCorpusFund fund = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.GetAgniveerCorpusFundById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", id);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                fund = new AgniveerCorpusFund();
                fund.AgniveerFundId = (int)reader["AgniveerFundId"];
                fund.EmployeeContribution = (decimal)reader["EmployeeContribution"];
                fund.GovtContribution = (decimal)reader["GovtContribution"];
                fund.TotalDeductionClaimed = (decimal)reader["TotalDeductionClaimed"];
                fund.EnrollmentDate = (DateTime)reader["EnrollmentDate"];
                fund.ConnectionDetailsID = (int)reader["ConnectionDetailsID"];
            }

            return fund;
        }
    }

    public class Exemption10CService : IExemption10CService
    {
        private readonly string _connectionString;

        public Exemption10CService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddExemption10C(Exemption10C model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.AddExemption10C", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployerName", model.EmployerName);
            cmd.Parameters.AddWithValue("@CompensationReceived", model.CompensationReceived);
            cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);
            cmd.Parameters.AddWithValue("@RetirementDate", model.RetirementDate);
            cmd.Parameters.AddWithValue("@ConnectionDetailID", model.ConnectionDetailsID);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateExemption10C(Exemption10C model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.UpdateExemption10C", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Exemption10CId", model.Exemption10CId);
            cmd.Parameters.AddWithValue("@EmployerName", model.EmployerName);
            cmd.Parameters.AddWithValue("@CompensationReceived", model.CompensationReceived);
            cmd.Parameters.AddWithValue("@ExemptAmount", model.ExemptAmount);
            cmd.Parameters.AddWithValue("@RetirementDate", model.RetirementDate);
            cmd.Parameters.AddWithValue("@ConnectionDetailId", model.ConnectionDetailsID);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public Exemption10C GetExemption10CById(int ConnectionDetailsID)
        {
            Exemption10C model = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("TaxCal.GetExemption10CById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID );

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model = new Exemption10C();
                model.Exemption10CId = (int)reader["Exemption10CId"];
                model.EmployerName = reader["EmployerName"].ToString();
                model.CompensationReceived = (decimal)reader["CompensationReceived"];
                model.ExemptAmount = (decimal)reader["ExemptAmount"];
                model.RetirementDate = (DateTime)reader["RetirementDate"];
            }

            return model;
        }
    }


    public class SavingBankInterestService : ISavingBankInterestService
    {
        private readonly string _connectionString;

        public SavingBankInterestService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddSavingBankInterest(SavingBankInterest interest)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.AddSavingBankInterest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BankName", interest.BankName);
                cmd.Parameters.AddWithValue("@AccountNumber", interest.AccountNumber);
                cmd.Parameters.AddWithValue("@InterestEarned", interest.InterestEarned);
                cmd.Parameters.AddWithValue("@DeductionClaimed", interest.DeductionClaimed);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", interest.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSavingBankInterest(SavingBankInterest interest)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateSavingBankInterest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BankInterestId", interest.BankInterestId);
                cmd.Parameters.AddWithValue("@BankName", interest.BankName);
                cmd.Parameters.AddWithValue("@AccountNumber", interest.AccountNumber);
                cmd.Parameters.AddWithValue("@InterestEarned", interest.InterestEarned);
                cmd.Parameters.AddWithValue("@DeductionClaimed", interest.DeductionClaimed);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", interest.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public SavingBankInterest GetSavingBankInterestById(int ConnectionDetailsID)
        {
            SavingBankInterest interest = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetSavingBankInterestById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        interest = new SavingBankInterest();
                        interest.BankInterestId = Convert.ToInt32(reader["BankInterestId"]);
                        interest.BankName = reader["BankName"].ToString();
                        interest.AccountNumber = reader["AccountNumber"].ToString();
                        interest.InterestEarned = Convert.ToDecimal(reader["InterestEarned"]);
                        interest.DeductionClaimed = Convert.ToDecimal(reader["DeductionClaimed"]);
                        interest.ConnectionDetailsID = Convert.ToInt32(reader["ConnectionDetailsID"]);
                    }
                }
            }

            return interest;
        }
    }



    public class Deduction80CService : IDeduction80CService
    {
        private readonly string _connectionString;

        public Deduction80CService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public void AddDeduction80C(Deduction80C deduction)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.AddDeduction80C", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@")
                cmd.Parameters.AddWithValue("@Total80CDeductionClaimed", deduction.Total80CDeductionClaimed);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", deduction.ConnectionDetailsID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDeduction80C(Deduction80C deduction)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateDeduction80C", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Deduction80CId", deduction.Deduction80CId);
                cmd.Parameters.AddWithValue("@Total80CDeductionClaimed", deduction.Total80CDeductionClaimed);
                cmd.Parameters.AddWithValue("@ConnectionDetailId", deduction.ConnectionDetailsID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Deduction80C GetDeduction80CById(int ConnectionDetailsID)
        {
            Deduction80C deduction = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetDeduction80CById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        deduction = new Deduction80C();
                        deduction.Deduction80CId = Convert.ToInt32(reader["Deduction80CId"]);
                        deduction.Total80CDeductionClaimed = Convert.ToDecimal(reader["Total80CDeductionClaimed"]);
                        deduction.ConnectionDetailsID = Convert.ToInt32(reader["ConnectionDetailsID"]);
                    }
                }
            }

            return deduction;
        }
    } 
}











