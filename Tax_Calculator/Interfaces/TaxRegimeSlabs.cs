namespace Tax_Calculator.Interfaces
{
    public interface TaxRegimeSlabs
    {
        public decimal minRange {  get; set; }
        public decimal maxRange { get; set; }    
        public decimal TaxRate { get; set; }

    }
}
