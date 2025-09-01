using Tax_Calculator.Interfaces;
using System.Collections.Generic;

namespace Tax_Calculator.Models
{
    public class OldTaxRegimeSlab : TaxSlab
    {
        public int SlabId { get; set; }

        public static readonly double StandardDeduction = 50000;
        public static readonly double Rebate87A = 12500;
        public static readonly double EffectiveTaxFreeIncome = 550000;

        public List<OldTaxRegimeSlab> GetSlabs_Junior()
        {
            return new List<OldTaxRegimeSlab>
        {
            new OldTaxRegimeSlab { SlabId = 1, minRange = 0, maxRange = 250000, TaxRate = 0.0m },
            new OldTaxRegimeSlab { SlabId = 2, minRange = 250000, maxRange = 500000, TaxRate = 0.05m },
            new OldTaxRegimeSlab { SlabId = 3, minRange = 500000, maxRange = 1000000, TaxRate = 0.20m },
            new OldTaxRegimeSlab { SlabId = 4, minRange = 1000000, maxRange = decimal.MaxValue, TaxRate = 0.30m }
        };
        }

        public List<OldTaxRegimeSlab> GetSlabs_Senior()
        {
            return new List<OldTaxRegimeSlab>
        {
            new OldTaxRegimeSlab { SlabId = 1, minRange = 0, maxRange = 300000, TaxRate = 0.0m },
            new OldTaxRegimeSlab { SlabId = 2, minRange = 300000, maxRange = 500000, TaxRate = 0.05m },
            new OldTaxRegimeSlab { SlabId = 3, minRange = 500000, maxRange = 1000000, TaxRate = 0.20m },
            new OldTaxRegimeSlab { SlabId = 4, minRange = 1000000, maxRange = decimal.MaxValue, TaxRate = 0.30m }
        };
        }

        public List<OldTaxRegimeSlab> GetSlabs_SuperSenior()
        {
            return new List<OldTaxRegimeSlab>
        {
            new OldTaxRegimeSlab { SlabId = 1, minRange = 0, maxRange = 500000, TaxRate = 0.0m },
            new OldTaxRegimeSlab { SlabId = 2, minRange = 500000, maxRange = 1000000, TaxRate = 0.20m },
            new OldTaxRegimeSlab { SlabId = 3, minRange = 1000000, maxRange = decimal.MaxValue, TaxRate = 0.30m }
        };
        }
    }

}
