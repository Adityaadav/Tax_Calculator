namespace Tax_Calculator.Models
{
    public class DeductionLimit
    {
        public int DeductionId { get; set; }
        public string DeductionCode { get; set; }
        public decimal? MaximumAmount { get; set; }
        public decimal? PercentageOfSalary { get; set; }
        //public int? AlternativeAmount1 { get; set; }
    }

}
