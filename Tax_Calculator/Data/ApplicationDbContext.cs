using Microsoft.EntityFrameworkCore;
using Tax_Calculator.Models;

namespace Tax_Calculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<IncomeFromSalary> IncomeFromSalaries { get; set; }
    }
}
//"ConnectionStrings": {
//    "DefaultConnectionString": "Data Source=ADITYA-PC\\SQLEXPRESS;Initial Catalog=Tax_Calculator;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True"

//  },