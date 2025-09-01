using Tax_Calculator.Interfaces;
namespace Tax_Calculator.Models
{
    public class TaxSlab : TaxRegimeSlabs
    {
        public decimal minRange { get; set; }
        public decimal maxRange { get; set; }
        public decimal TaxRate { get; set; }
    }

}
