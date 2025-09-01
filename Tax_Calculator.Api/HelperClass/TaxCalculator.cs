using Tax_Calculator.Models;

namespace Tax_Calculator.Api.HelperClass
{
    public static class TaxCalculatorHelper
    {
        public static decimal CalculateTax(decimal taxableIncome, int taxRegimeId, bool isSenior, bool isSuperSenior)
        {
            decimal totalTax = 0;
            List<TaxSlab> slabs = new();

            if (taxRegimeId == 1) // Old Regime
            {
                OldTaxRegimeSlab oldTaxRegimeSlab = new();

                if (isSuperSenior)
                    slabs = oldTaxRegimeSlab.GetSlabs_SuperSenior().Cast<TaxSlab>().ToList();
                else if (isSenior)
                    slabs = oldTaxRegimeSlab.GetSlabs_Senior().Cast<TaxSlab>().ToList();
                else
                    slabs = oldTaxRegimeSlab.GetSlabs_Junior().Cast<TaxSlab>().ToList();

                totalTax = CalculateFromSlabs(taxableIncome, slabs);

                if (taxableIncome <= (decimal)OldTaxRegimeSlab.EffectiveTaxFreeIncome)
                    totalTax = Math.Max(0, totalTax - (decimal)OldTaxRegimeSlab.Rebate87A);
            }
            else if (taxRegimeId == 2) // New Regime
            {
                slabs = NewTaxRegimeSlab.GetSlabs().Cast<TaxSlab>().ToList();

                totalTax = CalculateFromSlabs(taxableIncome, slabs);

                if (taxableIncome <= (decimal)NewTaxRegimeSlab.EffectiveTaxFreeIncome)
                    totalTax = Math.Max(0, totalTax - (decimal)NewTaxRegimeSlab.Rebate87A);
            }

            return totalTax;


        }

        private static decimal CalculateFromSlabs(decimal income, List<TaxSlab> slabs)
        {
            decimal tax = 0;

            foreach (var slab in slabs)
            {
                if (income > slab.minRange)
                {
                    decimal slabIncome = Math.Min(income, slab.maxRange) - slab.minRange;
                    tax += slabIncome * slab.TaxRate;
                }
            }

            return tax;
        }
    }

}
