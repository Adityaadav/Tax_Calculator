using Microsoft.Data.SqlClient;
using System.Data;
using Tax_Calculator.Api.Interface;
using Tax_Calculator.Interfaces;
using Tax_Calculator.Models;


namespace Tax_Calculator.Api.Services
{
    public class IncomeService:IIncomeFromSalaryService
    {
        private readonly string _connectionString;

        public IncomeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        //public IncomeFromSalary GetById(int connectionDetailsId)
        //{

        //    //IncomeFromSalary income = null;
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Get_IncomeFromSalary_ById", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ConnectionDetailsID", connectionDetailsId);

        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    IncomeFromSalary income = new IncomeFromSalary(
        //                   (int)reader["SalaryIncomeID"],
        //                   (decimal)reader["TotalSalary"],
        //                   reader["EmployerName"].ToString(),
        //                   (int)reader["ConnectionDetailsID"]
        //                    );
        //                    return income;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        public IncomeFromSalary GetById(int ConnectionDetailsId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Get_Income_From_Salary_ById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new IncomeFromSalary(
        (int)reader["SalaryIncomeID"],
        (decimal)reader["TotalSalary"],
        reader["EmployerName"].ToString(),
        (int)reader["ConnectionDetailsID"]
    ); 
                        }
                    }
                }
            }

            return null;
        }





        public void Add(IncomeFromSalary salary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.Upsert_Income_From_Salary", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@ConnectionDetails_Id", salary.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@SalaryIncomeID", salary.SalaryIncomeID==0 ? null :salary.SalaryIncomeID);  
                cmd.Parameters.AddWithValue("@TotalSalary", salary.TotalSalary);
                cmd.Parameters.AddWithValue("@EmployerName", salary.EmployerName);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IncomeFromSalary salary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("TaxCal.Upsert_Income_From_Salary", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@SalaryIncomeID")
                cmd.Parameters.AddWithValue("@ConnectionDetails_Id", salary.ConnectionDetailsID);
                cmd.Parameters.AddWithValue("@SalaryIncomeID", salary.SalaryIncomeID);
                cmd.Parameters.AddWithValue("@totalSalary", salary.TotalSalary);
                cmd.Parameters.AddWithValue("@EmployerName", salary.EmployerName ?? (object)DBNull.Value);
                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int ConnectionDetailsID  )
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TaxCal.Income_From_Salary WHERE ConnectionDetailsID = @ConnectionDetailsID", conn);
                cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class IncomeFromInterestService : IIncomeFromInterestService
    {
        private readonly string _connectionString;


        public IncomeFromInterestService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        //public IEnumerable<IncomeFromInterest> GetAll()
        //{
        //    var list = new List<IncomeFromInterest>();
        //    using var conn = new SqlConnection(_connectionString);
        //    var cmd = new SqlCommand("SELECT * FROM TaxCal.Income_From_Interest", conn);
        //    conn.Open();
        //    using var reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //        list.Add(new IncomeFromInterest((int)reader["InterestID"], reader["IncomeSource"].ToString(), (decimal)reader["InterestAmount"]));
        //    return list;
        //}

        public IncomeFromInterest GetById(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.GetIncome_From_Interest", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new IncomeFromInterest((int)reader["InterestIncomeId"], reader["IncomeSource"].ToString(), (decimal)reader["InterestAmount"], (int)reader["ConnectionDetailsID"]) : null;
        }

        public void Add(IncomeFromInterest interest)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Interest", conn);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@ConnectionDetails_Id", interest.ConnectionDetailsID);
            cmd.Parameters.AddWithValue("@InterestIncomeId", interest.InterestID==0 ? null: interest.InterestID );    
            cmd.Parameters.AddWithValue("@IncomeSource", interest.IncomeSource);
            cmd.Parameters.AddWithValue("@InterestAmount", interest.InterestAmount);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(IncomeFromInterest interest)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Interest", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@id", interest.InterestID);
            cmd.Parameters.AddWithValue("@ConnectionDetails_Id", interest.ConnectionDetailsID);
            cmd.Parameters.AddWithValue("@InterestIncomeId", interest.InterestID);
            cmd.Parameters.AddWithValue("@IncomeSource", interest.IncomeSource);
            cmd.Parameters.AddWithValue("@InterestAmount", interest.InterestAmount);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM TaxCal.Income_From_Interest WHERE ConnectionDetailsID = @ConnectionDetailsID", conn);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public class DigitalAssetIncomeService : IDigitalAssetIncomeService
    {
        private readonly string _connectionString;
        public DigitalAssetIncomeService(IConfiguration config) => _connectionString = config.GetConnectionString("DefaultConnection");

      

        public DigitalAssetIncome GetById(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.GetIncomeFromDigital", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);   
            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new DigitalAssetIncome((int)reader["DigitalAssetIncomeID"], reader["DigitalSource"].ToString(), reader["TransactionType"].ToString(), (decimal)reader["IncomeAmount"],(int)reader["ConnectionDetailsID"]) : null;
        }

        public void Add(DigitalAssetIncome item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Digital", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@DigitalAssetIncomeId", item.DigitaLAssetIncomeId==0 ? null : item.DigitaLAssetIncomeId);
            cmd.Parameters.AddWithValue("@DigitalSource", item.DigitalSource);
            cmd.Parameters.AddWithValue("@TranscationType", item.TranscationType);
            cmd.Parameters.AddWithValue("@IncomeAmount", item.TranscationAmount);
            cmd.Parameters.AddWithValue("@ConnectionDetailId", item.ConnectionDetailsID);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(DigitalAssetIncome item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Digital", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DigitalAssetIncomeId", item.DigitaLAssetIncomeId);
            cmd.Parameters.AddWithValue("@DigitalSource", item.DigitalSource);
            cmd.Parameters.AddWithValue("@TranscationType", item.TranscationType);
            cmd.Parameters.AddWithValue("@IncomeAmount", item.TranscationAmount);
            cmd.Parameters.AddWithValue("@ConnectionDetailId", item.ConnectionDetailsID);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM TaxCal.Income_From_Digital WHERE ConnectionDetailsID = @ConnectionDetailsID   ", conn);

            cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public class IncomeFromRentService : IIncomeFromRentService
    {
        private readonly string _connectionString;
        public IncomeFromRentService(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection");


        public IncomeFromRent GetById(int ConnectionDetailsID)
        {
            IncomeFromRent Re = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("TaxCal.usp_Get_Income_From_Rent_ById", conn)) 
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConnectionDetailID", ConnectionDetailsID);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Re = new IncomeFromRent(
                            (int)reader["RentalIncomeID"],
                            (decimal)reader["MonthlyRent"],
                            reader["PropertyAddress"].ToString(),
                            (int)reader["ConnectionDetailsID"]
                        );
                    }
                }
            }
            return Re;
        }



        public void Add(IncomeFromRent item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Rent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RentalIncomeId", item.RentalID == 0 ? null : item.RentalID);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", item.ConnectionDetailsID);  
            cmd.Parameters.AddWithValue("@PropertyAddress", item.PropertyAddress);
            cmd.Parameters.AddWithValue("@MonthlyRent", item.MonthlyRent);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(IncomeFromRent item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_Rent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RentalIncomeId", item.RentalID);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", item.ConnectionDetailsID);
            cmd.Parameters.AddWithValue("@PropertyAddress", item.PropertyAddress);
            cmd.Parameters.AddWithValue("@MonthlyRent", item.MonthlyRent);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM TaxCal.Income_From_Rent WHERE ConnectionDetailsID = @ConnectionDetailsID", conn);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public class HomeLoanLetoutService : IHomeLoanLetoutService
    {
        private readonly string _connectionString;
        public HomeLoanLetoutService(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection");

      

        public HomeLoanLetoutInterest GetById(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Get_IncomeFromHomeLetOut", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ConnectionDetailsId", ConnectionDetailsID);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new HomeLoanLetoutInterest((int)reader["LoanInterestId"], reader["LenderName"].ToString(), (decimal)reader["InterestAmount"], (int)reader["ConnectionDetailsID"]) : null;
        }

        public void Add(HomeLoanLetoutInterest item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_HomeLoan_LetOut", conn);
            cmd.CommandType = CommandType.StoredProcedure;      conn.Open();    
            cmd.Parameters.AddWithValue("@LenderName", item.LenderName);
            cmd.Parameters.AddWithValue("@InterestAmount", item.interestAmount);
            cmd.Parameters.AddWithValue("@ConnectionDetails_Id", item.ConnectionDetailsID);
            //cmd.Parameters.AddWithValue("@LonaInterestId", item.LoanInterestId);
            cmd.ExecuteNonQuery();
        }

        public void Update(HomeLoanLetoutInterest item)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("TaxCal.Upsert_Income_From_HomeLoan_LetOut", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LoanInterestId", item.LoanInterestId);            
            cmd.Parameters.AddWithValue("@ConnectionDetails_Id", item.ConnectionDetailsID);
            cmd.Parameters.AddWithValue("@LenderName", item.LenderName);
            cmd.Parameters.AddWithValue("@InterestAmount", item.interestAmount);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int ConnectionDetailsID)
        {
            using var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM TaxCal.Income_From_HomeLoan_LetOut WHERE ConnectionDetailsID = @ConnectionDetailsID", conn);
            cmd.Parameters.AddWithValue("@ConnectionDetailsID", ConnectionDetailsID);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
