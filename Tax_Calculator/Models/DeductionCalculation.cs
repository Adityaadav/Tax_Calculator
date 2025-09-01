using Tax_Calculator.Interfaces;

namespace Tax_Calculator.Models
{
    public class DeductionCalculator
    {
        public decimal CalculateHRAExemption(IHRAExemption hra, decimal basicSalary, bool isMetro, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal salaryPercentage = isMetro ? 0.5m : 0.4m;
            decimal rentExcess = hra.RentPaidAnnually - 0.1m * basicSalary;
            decimal exemption = Math.Min(hra.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
            return Math.Max(0, exemption);
        }

        public decimal CalculateEntertainmentProfessionalTax(IEntertainmentProfessionalTax input, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal entertainmentLimit = Math.Min(5000, input.EntertainmentAllowanceReceived);
            decimal professionalTaxLimit = Math.Min(2500, input.ProfessionalTaxPaid);
            return entertainmentLimit + professionalTaxLimit;
        }

        public decimal CalculateEmployeeContributionToNPS(IEmployeeContributionToNPS nps, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(150000, nps.Amount80CCD1) + Math.Min(50000, nps.Amount80CCD1B);
        }

        public decimal CalculateLTA(ILTAAllowance lta, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(lta.ClaimedAmount, lta.ActualTravelExpense);
        }

        public decimal CalculateMedicalInsurance80D(IMedicalInsurancePremium80D med, bool isSeniorCitizen, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal limit = isSeniorCitizen ? 50000 : 25000;
            decimal total = med.PremiumPaidSelfFamily + med.PreventiveHealthCheckup;
            return Math.Min(limit, total);
        }

        public decimal CalculateDisabledIndividual80U(IDisabledIndividual80U dis, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return dis.IsSevereDisability ? 125000 : 75000;
        }

        public decimal CalculateEVLoanInterest80EEB(IInterestOnElectricVehicleLoan ev, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(150000, ev.InterestAmountPaid);
        }

        public decimal CalculateEmployerContributionToNPS(IEmployerContributionToNPS employer, decimal salary, int taxRegimeId)
        {
            if (taxRegimeId != 2) return 0;
            return Math.Min(employer.ContributionAmount, 0.10m * salary);
        }

        public decimal CalculateInterestHomeLoanSelf(IInterestOnHomeLoanSelf homeLoan, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(200000, homeLoan.InterestAmountPaid);
        }

        public decimal CalculateInterestHomeLoanLetOut(IInterestOnHomeLoanLetOut letOut, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return letOut.InterestAmountPaid;
        }

        public decimal CalculateDonation80G(IDonationToPoliticalPartyTrust donation, bool isFullExempt, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return isFullExempt ? donation.DonationAmount : 0.5m * donation.DonationAmount;
        }

        public decimal CalculateConveyanceAllowance(IConveyanceAllowance ca, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(ca.AmountReceived, ca.ExemptAmount);
        }

        public decimal CalculateTransportAllowanceSpeciallyAbled(ITransportAllowanceSpeciallyAbled ta, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(3200 * 12, ta.AmountReceived);
        }

        public decimal CalculateAgniveerCorpusFund(IAgniveerCorpusFund fund, int taxRegimeId)
        {
            if (taxRegimeId != 2) return 0;
            return fund.EmployeeContribution + fund.GovtContribution;
        }

        public decimal CalculateExemption10C(IExemption10C ex, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(500000, ex.ExemptAmount);
        }

       

        public decimal CalculateSavingBankInterest(ISavingBankInterest interest, bool isSeniorCitizen, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(isSeniorCitizen ? 50000 : 10000, interest.InterestEarned);
        }

        public decimal CalculateDeduction80C(IDeduction80C d, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(150000, d.Total80CDeductionClaimed);
        }
    }
}
