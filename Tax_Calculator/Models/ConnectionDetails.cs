namespace Tax_Calculator.Models
{
    public class ConnectionDetails
    {
        public int ConnectionDetailsID { get; set; }
        public int TaxPayerID { get; set; }
        public int TaxYearID { get; set; }
        public int ResidentStatusID { get; set; }
        public int TaxRegimeID { get; set; }
        // Add navigation properties if needed
    }
}