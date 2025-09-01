using Microsoft.Data.SqlClient;
using System.Data;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Models;

namespace Tax_Calculator.Api.Services
{
    public class BasicDetailsService : IBasicDetailsService
    {
        private readonly string _connectionString;

        public BasicDetailsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddBasicDetails(BasicDetails details)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("TaxCal.AddBasicDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@TaxPayerId", details.Tax_Payer_Id);
            cmd.Parameters.AddWithValue("@First_Name", details.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", details.Last_Name);
            cmd.Parameters.AddWithValue("@DOB", details.DOB);
            cmd.Parameters.AddWithValue("@PAN_NUMBER", details.PAN_NUMBER );

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateBasicDetails(BasicDetails details)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("TaxCal.UpdateBasicDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tax_Payer_Id", details.Tax_Payer_Id);
            cmd.Parameters.AddWithValue("@First_Name", details.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", details.Last_Name);
            cmd.Parameters.AddWithValue("@DOB", details.DOB);
            cmd.Parameters.AddWithValue("@PAN_NUMBER", details.PAN_NUMBER ?? (object)DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public BasicDetails GetBasicDetailsById(int id)
        {
            BasicDetails details = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("TaxCal.GetBasicDetailsById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Tax_Payer_Id", id);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                details = new BasicDetails
                {
                    Tax_Payer_Id = (int)reader["Tax_Payer_Id"],
                    First_Name = reader["First_Name"].ToString(),
                    Last_Name = reader["Last_Name"].ToString(),
                    DOB = (DateTime)reader["DOB"],
                    PAN_NUMBER = reader["PAN_NUMBER"]?.ToString()
                };
            }

            return details;
        }
    }
    public class TaxPayerBankAccountService : ITaxPayerBankAccountService
    {
        private readonly string _connectionString;

        public TaxPayerBankAccountService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddTaxPayerBankAccount(TaxPayerBankAccount account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.AddTaxPayerBankAccount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tax_Payer_Id", account.Tax_Payer_Id);
                cmd.Parameters.AddWithValue("@BankName", account.BankName);
                cmd.Parameters.AddWithValue("@BankAccountNumber", account.BankAccountNumber);
                cmd.Parameters.AddWithValue("@AccountType", account.AccountType);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTaxPayerBankAccount(TaxPayerBankAccount account)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.UpdateTaxPayerBankAccount", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BankAccountID", account.BankAccountID);
                cmd.Parameters.AddWithValue("@Tax_Payer_Id", account.Tax_Payer_Id);
                cmd.Parameters.AddWithValue("@BankName", account.BankName);
                cmd.Parameters.AddWithValue("@BankAccountNumber", account.BankAccountNumber);
                cmd.Parameters.AddWithValue("@AccountType", account.AccountType);

                conn.Open();
                cmd.ExecuteNonQuery();
            } 
        }

        public TaxPayerBankAccount GetTaxPayerBankAccountById(int id)
        {
            TaxPayerBankAccount account = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.GetTaxPayerBankAccountById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BankAccountID", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        account = new TaxPayerBankAccount();
                        account.BankAccountID = Convert.ToInt32(reader["BankAccountID"]);
                        account.Tax_Payer_Id = Convert.ToInt32(reader["Tax_Payer_Id"]);
                        account.BankName = reader["BankName"].ToString();
                        account.BankAccountNumber = reader["BankAccountNumber"].ToString();
                        account.AccountType = reader["AccountType"]?.ToString();
                    }
                }
            }

            return account;
        }
    }
    public class TaxPeriodService : ITaxPeriodService
    {
        private readonly string _connectionString;
        public TaxPeriodService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddTaxPeriod(TaxPeriod period)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.AddTaxPeriod", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaxYearStartDate", period.TaxYearStartDate);
                cmd.Parameters.AddWithValue("@TaxYearEndDate", period.TaxYearEndDate);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTaxPeriod(TaxPeriod period)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.UpdateTaxPeriod", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaxYearID", period.TaxYearID);
                cmd.Parameters.AddWithValue("@TaxYearStartDate", period.TaxYearStartDate);
                cmd.Parameters.AddWithValue("@TaxYearEndDate", period.TaxYearEndDate);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TaxPeriod GetTaxPeriodById(int id)
        {
            TaxPeriod period = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.GetTaxPeriodById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaxYearID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    period = new TaxPeriod();
                    period.TaxYearID = Convert.ToInt32(reader["TaxYearID"]);
                    period.TaxYearStartDate = Convert.ToDateTime(reader["TaxYearStartDate"]);
                    period.TaxYearEndDate = Convert.ToDateTime(reader["TaxYearEndDate"]);
                }
            }
            return period;
        }
    }
    public class ResidentStatusService : IResidentStatusService
    {
        private readonly string _connectionString;

        public ResidentStatusService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddResidentStatus(ResidentStatus status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.AddResidentStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateResidentStatus(ResidentStatus status)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.UpdateResidentStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ResidentStatusID", status.ResidentStatusID);
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ResidentStatus GetResidentStatusById(int id)
        {
            ResidentStatus status = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.GetResidentStatusById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ResidentStatusID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = new ResidentStatus();
                    status.ResidentStatusID = (int)reader["ResidentStatusID"];
                    status.StatusName = reader["StatusName"].ToString();
                }
            }
            return status;
        }
    }
    public class TaxRegimeService : ITaxRegimeService
    {
        private readonly string _connectionString;

        public TaxRegimeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddTaxRegime(TaxRegime regime)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.AddTaxRegime", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegimeName", regime.RegimeName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTaxRegime(TaxRegime regime)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.UpdateTaxRegime", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegimeID", regime.RegimeID);
                cmd.Parameters.AddWithValue("@RegimeName", regime.RegimeName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TaxRegime GetTaxRegimeById(int id)
        {
            TaxRegime regime = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.GetTaxRegimeById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegimeID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    regime = new TaxRegime();
                    regime.RegimeID = (int)reader["RegimeID"];
                    regime.RegimeName = reader["RegimeName"].ToString();
                }
            }
            return regime;
        }
    }

}
