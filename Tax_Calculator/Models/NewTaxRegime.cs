using Tax_Calculator.Interfaces;
using System.Collections.Generic;

namespace Tax_Calculator.Models
{
    public class NewTaxRegimeSlab : TaxSlab
    {
        public int SlabId { get; set; }

        public static readonly double StandardDeduction = 50000;
        public static readonly double Rebate87A = 25000;
        public static readonly double EffectiveTaxFreeIncome = 700000;

        public static List<NewTaxRegimeSlab> GetSlabs()
        {
            return new List<NewTaxRegimeSlab>
        {
            new NewTaxRegimeSlab { SlabId = 1, minRange = 0, maxRange = 300000, TaxRate = 0.0m },
            new NewTaxRegimeSlab { SlabId = 2, minRange = 300000, maxRange = 600000, TaxRate = 0.05m },
            new NewTaxRegimeSlab { SlabId = 3, minRange = 600000, maxRange = 900000, TaxRate = 0.10m },
            new NewTaxRegimeSlab { SlabId = 4, minRange = 900000, maxRange = 1200000, TaxRate = 0.15m },
            new NewTaxRegimeSlab { SlabId = 5, minRange = 1200000, maxRange = 1500000, TaxRate = 0.20m },
            new NewTaxRegimeSlab { SlabId = 6, minRange = 1500000, maxRange = decimal.MaxValue, TaxRate = 0.30m }
        };
        }
    }

}

