using Tax_Calculator.Api.HelperClass;
using Tax_Calculator.Interfaces;
using Tax_Calculator.Models;
using Tax_Calculator.Api.HelperClass;
namespace Tax_Calculator.Api.Services
{
    public class DeductionCalculator
    {
        public decimal CalculateTotalDeduction(
            DeductionInputsDto dto,
            List<DeductionLimit> limits,
            decimal basicSalary,
            bool isSenior,
            int taxRegimeId)
        {
            if (dto == null || limits == null || limits.Count == 0)
                return 0m;

            decimal total = 0m;

            foreach (var limit in limits)
            {
                if (limit == null || string.IsNullOrWhiteSpace(limit.DeductionCode))
                    continue;

                switch (limit.DeductionCode.ToUpperInvariant())
                {
                    case "HRA_EXEMPTIONMETRO":
                    case "HRA_EXEMPTIONNONMETRO":
                        total += CalculateHRAExemption(dto, basicSalary, limit, taxRegimeId);
                        break;

                    case "EMPLOYEE_NPS":
                        total += CalculateEmployeeContributionToNPS(dto, limit, taxRegimeId);
                        break;

                    case "EMPLOYER_NPS_CONTRI":
                        total += CalculateEmployerContributionToNPS(dto, basicSalary, limit, taxRegimeId);
                        break;

                    case "MED_INS_80D":
                        total += CalculateMedicalInsurance80D(dto, isSenior, limit, taxRegimeId);
                        break;

                    case "LTA":
                        total += CalculateLTA(dto, limit, taxRegimeId);
                        break;

                    case "DISABLED_80U":
                        total += CalculateDisabledIndividual80U(dto, limit, taxRegimeId);
                        break;

                    case "EV_LOAN_80EEB":
                        total += CalculateEVLoanInterest80EEB(dto, limit, taxRegimeId);
                        break;

                    case "HOME_LOAN_SELF":
                        total += CalculateInterestHomeLoanSelf(dto, limit, taxRegimeId);
                        break;

                    case "HOME_LOAN_LET_OUT":
                        total += CalculateInterestHomeLoanLetOut(dto, limit, taxRegimeId);
                        break;

                    case "DONATION_80G":
                        total += CalculateDonation80G(dto, true, limit, taxRegimeId);
                        break;

                    case "CONVEYANCE_ALLOW":
                        total += CalculateConveyanceAllowance(dto, limit, taxRegimeId);
                        break;

                    case "TRANSPORT_SPECIAL":
                        total += CalculateTransportAllowanceSpeciallyAbled(dto, limit, taxRegimeId);
                        break;

                    case "AGNIVEER_CORPUS":
                        total += CalculateAgniveerCorpusFund(dto, limit, taxRegimeId);
                        break;

                    case "EXEMPTION_10C":
                        total += CalculateExemption10C(dto, limit, taxRegimeId);
                        break;

                    case "SB_INTEREST":
                        total += CalculateSavingBankInterest(dto, isSenior, limit, taxRegimeId);
                        break;

                    case "SECTION_80C":
                        total += CalculateDeduction80C(dto, limit, taxRegimeId);
                        break;

                    case "ENTERTAINMENT_PRO_TAX":
                        total += CalculateEntertainmentProfessionalTax(dto, limit, taxRegimeId);
                        break;
                }
            }

            return total;
        }

        public decimal CalculateHRAExemption(DeductionInputsDto dto, decimal basicSalary, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            
            decimal salaryPercentage = (limit.PercentageOfSalary ?? 0) / 100m;
            decimal rentExcess = dto.RentPaidAnnually - (basicSalary * 0.10m);
            decimal exemption = Math.Min(dto.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
            
            Console.WriteLine(exemption);
            return Math.Max(0, exemption);
        }

        public decimal CalculateEmployeeContributionToNPS(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            Console.WriteLine(dto.Amount80CCD1 + dto.Amount80CCD1B);
            return Math.Min(limit.MaximumAmount ?? 0, dto.Amount80CCD1 + dto.Amount80CCD1B);
        }

        public decimal CalculateEmployerContributionToNPS(DeductionInputsDto dto, decimal salary, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 2) return 0;
            decimal cap = (limit.PercentageOfSalary ?? 0) / 100m * salary;
            return Math.Min(dto.ContributionAmount, cap);
        }

        public decimal CalculateMedicalInsurance80D(DeductionInputsDto dto, bool isSenior, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal total = dto.PremiumPaidSelfFamily + dto.PreventiveHealthCheckup;
            return Math.Min(limit.MaximumAmount ?? 0, total);
        }

        public decimal CalculateLTA(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(dto.ClaimedAmount, dto.ActualTravelExpense);
        }

        public decimal CalculateDisabledIndividual80U(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return limit.MaximumAmount ?? 0;
        }

        public decimal CalculateEVLoanInterest80EEB(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(limit.MaximumAmount ?? 0, dto.InterestAmountPaid);
        }

        public decimal CalculateInterestHomeLoanSelf(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(limit.MaximumAmount ?? 0, dto.InterestAmountPaidSelf);
        }

        public decimal CalculateInterestHomeLoanLetOut(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return dto.InterestAmountPaidLetOut;
        }

        public decimal CalculateDonation80G(DeductionInputsDto dto, bool isFullExempt, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal percent = (limit.PercentageOfSalary ?? 0) / 100m;
            return dto.DonationAmount * percent;
        }

        public decimal CalculateConveyanceAllowance(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(dto.ConveyanceAmountReceived, dto.ConveyanceExemptAmount);
        }

        public decimal CalculateTransportAllowanceSpeciallyAbled(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(limit.MaximumAmount ?? 0, dto.TransportAllowanceReceived);
        }

        public decimal CalculateAgniveerCorpusFund(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 2) return 0;
            return dto.EmployeeCorpusContribution + dto.GovtCorpusContribution;
        }

        public decimal CalculateExemption10C(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(limit.MaximumAmount ?? 0, dto.ExemptAmount10C);
        }

        public decimal CalculateSavingBankInterest(DeductionInputsDto dto, bool isSenior, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(limit.MaximumAmount ?? 0, dto.SavingBankInterestEarned);
        }

        public decimal CalculateDeduction80C(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            return Math.Min(dto.Total80CDeductionClaimed, limit.MaximumAmount ?? 0);
        }

        public decimal CalculateEntertainmentProfessionalTax(DeductionInputsDto dto, DeductionLimit limit, int taxRegimeId)
        {
            if (taxRegimeId != 1) return 0;
            decimal entLimit = Math.Min(limit.MaximumAmount ?? 0, dto.EntertainmentAllowanceReceived);
            decimal proTaxLimit = Math.Min(limit.MaximumAmount ?? 0, dto.ProfessionalTaxPaid);
            return entLimit + proTaxLimit;
        }
    }

}






























//public class DeductionCalculator
//{

//    //public decimal CalculateHRAExemption(IHRAExemption hra, decimal basicSalary, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    decimal salaryPercentage = (limit.PercentageOfSalary ?? 0) / 100m;
//    //    decimal rentExcess = hra.RentPaidAnnually - (basicSalary * 0.10m); // 10% of salary
//    //    decimal exemption = Math.Min(hra.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
//    //    return Math.Max(0, exemption);
//    //}

//    //public decimal CalculateEntertainmentProfessionalTax(IEntertainmentProfessionalTax input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    decimal entLimit = Math.Min(limit.MaximumAmount ?? 0, input.EntertainmentAllowanceReceived);
//    //    decimal proTaxLimit = Math.Min(limit.MaximumAmount ?? 0, input.ProfessionalTaxPaid);
//    //    return entLimit + proTaxLimit;
//    //}

//    //public decimal CalculateEmployeeContributionToNPS(IEmployeeContributionToNPS input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.Amount80CCD1 + input.Amount80CCD1B);
//    //}

//    //public decimal CalculateLTA(ILTAAllowance input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(input.ClaimedAmount, input.ActualTravelExpense);
//    //}

//    //public decimal CalculateMedicalInsurance80D(IMedicalInsurancePremium80D input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    decimal total = input.PremiumPaidSelfFamily + input.PreventiveHealthCheckup;
//    //    return Math.Min(limit.MaximumAmount ?? 0, total);
//    //}

//    //public decimal CalculateDisabledIndividual80U(IDisabledIndividual80U input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return limit.MaximumAmount ?? 0;
//    //}

//    //public decimal CalculateEVLoanInterest80EEB(IInterestOnElectricVehicleLoan input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//    //}

//    //public decimal CalculateEmployerContributionToNPS(IEmployerContributionToNPS input, decimal salary, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 2 || limit == null) return 0;
//    //    decimal cap = (limit.PercentageOfSalary ?? 0) / 100m * salary;
//    //    return Math.Min(input.ContributionAmount, cap);
//    //}

//    //public decimal CalculateInterestHomeLoanSelf(IInterestOnHomeLoanSelf input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//    //}

//    //public decimal CalculateInterestHomeLoanLetOut(IInterestOnHomeLoanLetOut input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1) return 0;
//    //    return input.InterestAmountPaid;
//    //}

//    //public decimal CalculateDonation80G(IDonationToPoliticalPartyTrust input, bool isFullExempt, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    decimal percent = (limit.PercentageOfSalary ?? 0) / 100m;
//    //    return input.DonationAmount * percent;
//    //}

//    //public decimal CalculateConveyanceAllowance(IConveyanceAllowance input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1) return 0;
//    //    return Math.Min(input.AmountReceived, input.ExemptAmount);
//    //}

//    //public decimal CalculateTransportAllowanceSpeciallyAbled(ITransportAllowanceSpeciallyAbled input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.AmountReceived);
//    //}

//    //public decimal CalculateAgniveerCorpusFund(IAgniveerCorpusFund input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 2) return 0;
//    //    return input.EmployeeContribution + input.GovtContribution;
//    //}

//    //public decimal CalculateExemption10C(IExemption10C input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.ExemptAmount);
//    //}

//    //public decimal CalculateSavingBankInterest(ISavingBankInterest input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(limit.MaximumAmount ?? 0, input.InterestEarned);
//    //}

//    //public decimal CalculateDeduction80C(IDeduction80C input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1 || limit == null) return 0;
//    //    return Math.Min(input.Total80CDeductionClaimed, limit.MaximumAmount ?? 0);
//    //}
//    //public decimal CalculateDeduction80C(IDeduction80C input, DeductionLimit limit, int taxRegimeId)
//    //{
//    //    if (taxRegimeId != 1)
//    //        return 0;

//    //    if (limit == null || input == null)
//    //        return 0;

//    //    var claimed = input.Total80CDeductionClaimed ?? 0; // if it's nullable
//    //    return Math.Min(claimed, limit.MaximumAmount ?? 0);
//    //}



//    //public static  decimal CalculateTotalDeduction(Dictionary<string, object> deductions, List<DeductionLimit> limits, decimal basicSalary, bool isSenior, int taxRegimeId)
//    //    {
//    //        decimal total = 0;

//    //        foreach (DeductionLimit limit in limits)
//    //        {
//    //            string type = limit.DeductionCode;

//    //            switch (type)

//    //            {   case "HRA_EXEMPTIONNONMETRO":
//    //                case "HRA_ExemptionMETRO":
//    //                    //case "HRA":
//    //                    if (deductions.TryGetValue("HRAExemption", out var hraObj) && hraObj is IHRAExemption hra)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateHRAExemption(hra, basicSalary, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "ENTERTAINMENT_PRO_TAX":
//    //                    if (deductions.TryGetValue("Entertainment_Professional_Tax", out var eptObj) && eptObj is IEntertainmentProfessionalTax ept)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateEntertainmentProfessionalTax(ept, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "EMPLOYEE_NPS":
//    //                    if (deductions.TryGetValue("EmployeeContributionToNPS", out var empNpsObj) && empNpsObj is IEmployeeContributionToNPS empNps)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateEmployeeContributionToNPS(empNps, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "LTA":
//    //                    if (deductions.TryGetValue("LTAAllowance", out var ltaObj) && ltaObj is ILTAAllowance lta && lta != null)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateLTA(lta, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "MED_INS_80D":
//    //                    if (deductions.TryGetValue("MedicalInsurance80D", out var med80DObj) && med80DObj is IMedicalInsurancePremium80D med)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateMedicalInsurance80D(med, isSenior, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "DISABLED_80U":
//    //                    if (deductions.TryGetValue("DisabledIndividual80U", out var disObj) && disObj is IDisabledIndividual80U dis)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateDisabledIndividual80U(dis, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "EV_LOAN_80EEB":
//    //                    if (deductions.TryGetValue("ElectricVehicleLoan", out var evObj) && evObj is IInterestOnElectricVehicleLoan ev)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateEVLoanInterest80EEB(ev, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "EMPLOYER_NPS_CONTRI":
//    //                    if (deductions.TryGetValue("EmployerContributionToNPS", out var empObj) && empObj is IEmployerContributionToNPS emp)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateEmployerContributionToNPS(emp, basicSalary, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "HOME_LOAN_SELF":
//    //                    if (deductions.TryGetValue("InterestHomeLoanSelf", out var hlSelfObj) && hlSelfObj is IInterestOnHomeLoanSelf self)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateInterestHomeLoanSelf(self, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "HOME_LOAN_LET_OUT":
//    //                    if (deductions.TryGetValue("InterestHomeLoanLetOut", out var hlLetObj) && hlLetObj is IInterestOnHomeLoanLetOut letOut)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateInterestHomeLoanLetOut(letOut, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "DONATION_80G":
//    //                    if (deductions.TryGetValue("DonationToPoliticalParty", out var donObj) && donObj is IDonationToPoliticalPartyTrust don)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateDonation80G(don, true, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "CONVEYANCE_ALLOW":
//    //                    if (deductions.TryGetValue("ConveyanceAllowance", out var conObj) && conObj is IConveyanceAllowance con)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateConveyanceAllowance(con, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "TRANSPORT_SPECIAL":
//    //                    if (deductions.TryGetValue("TransportAllowanceSpeciallyAbled", out var taObj) && taObj is ITransportAllowanceSpeciallyAbled ta)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateTransportAllowanceSpeciallyAbled(ta, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "AGNIVEER_CORPUS":
//    //                    if (deductions.TryGetValue("AgniveerCorpusFund", out var agnObj) && agnObj is IAgniveerCorpusFund agn)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateAgniveerCorpusFund(agn, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "EXEMPTION_10C":
//    //                    if (deductions.TryGetValue("Exemption10C", out var ex10CObj) && ex10CObj is IExemption10C ex10c)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateExemption10C(ex10c, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "SB_INTEREST":
//    //                    if (deductions.TryGetValue("SavingBankInterest", out var sbiObj) && sbiObj is ISavingBankInterest sbi)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateSavingBankInterest(sbi, isSenior, limit, taxRegimeId);
//    //                    }
//    //                    break;

//    //                case "SECTION_80C":
//    //                    if (deductions.TryGetValue("Deduction80C", out var d80CObj) && d80CObj is IDeduction80C d80c)
//    //                    {
//    //                        total += new DeductionCalculator().CalculateDeduction80C(d80c, limit, taxRegimeId);
//    //                    }
//    //                    break;
//    //            }
//    //        }

//    //        return total;
//    //    }
//    //    public decimal CalculateHRAExemption(IHRAExemption hra, decimal basicSalary, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || hra == null) return 0;
//    //        decimal salaryPercentage = (limit.PercentageOfSalary ?? 0) / 100m;
//    //        decimal rentExcess = hra.RentPaidAnnually - (basicSalary * 0.10m); // 10% of salary
//    //        decimal exemption = Math.Min(hra.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
//    //        return Math.Max(0, exemption);
//    //    }

//    //    public decimal CalculateEntertainmentProfessionalTax(IEntertainmentProfessionalTax input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        decimal entLimit = Math.Min(limit.MaximumAmount ?? 0, input.EntertainmentAllowanceReceived);
//    //        decimal proTaxLimit = Math.Min(limit.MaximumAmount ?? 0, input.ProfessionalTaxPaid);
//    //        return entLimit + proTaxLimit;
//    //    }

//    //    public decimal CalculateEmployeeContributionToNPS(IEmployeeContributionToNPS input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.Amount80CCD1 + input.Amount80CCD1B);
//    //    }

//    //    public decimal CalculateLTA(ILTAAllowance input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(input.ClaimedAmount, input.ActualTravelExpense);
//    //    }

//    //    public decimal CalculateMedicalInsurance80D(IMedicalInsurancePremium80D input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        decimal total = input.PremiumPaidSelfFamily + input.PreventiveHealthCheckup;
//    //        return Math.Min(limit.MaximumAmount ?? 0, total);
//    //    }

//    //    public decimal CalculateDisabledIndividual80U(IDisabledIndividual80U input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return limit.MaximumAmount ?? 0;
//    //    }

//    //    public decimal CalculateEVLoanInterest80EEB(IInterestOnElectricVehicleLoan input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//    //    }

//    //    public decimal CalculateEmployerContributionToNPS(IEmployerContributionToNPS input, decimal salary, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 2 || limit == null || input == null) return 0;
//    //        decimal cap = (limit.PercentageOfSalary ?? 0) / 100m * salary;
//    //        return Math.Min(input.ContributionAmount, cap);
//    //    }

//    //    public decimal CalculateInterestHomeLoanSelf(IInterestOnHomeLoanSelf input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//    //    }

//    //    public decimal CalculateInterestHomeLoanLetOut(IInterestOnHomeLoanLetOut input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || input == null) return 0;
//    //        return input.InterestAmountPaid;
//    //    }

//    //    public decimal CalculateDonation80G(IDonationToPoliticalPartyTrust input, bool isFullExempt, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        decimal percent = (limit.PercentageOfSalary ?? 0) / 100m;
//    //        return input.DonationAmount * percent;
//    //    }

//    //    public decimal CalculateConveyanceAllowance(IConveyanceAllowance input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || input == null) return 0;
//    //        return Math.Min(input.AmountReceived, input.ExemptAmount);
//    //    }

//    //    public decimal CalculateTransportAllowanceSpeciallyAbled(ITransportAllowanceSpeciallyAbled input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.AmountReceived);
//    //    }

//    //    public decimal CalculateAgniveerCorpusFund(IAgniveerCorpusFund input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 2 || input == null) return 0;
//    //        return input.EmployeeContribution + input.GovtContribution;
//    //    }

//    //    public decimal CalculateExemption10C(IExemption10C input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.ExemptAmount);
//    //    }

//    //    public decimal CalculateSavingBankInterest(ISavingBankInterest input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(limit.MaximumAmount ?? 0, input.InterestEarned);
//    //    }

//    //    public decimal CalculateDeduction80C(IDeduction80C input, DeductionLimit limit, int taxRegimeId)
//    //    {
//    //        if (taxRegimeId != 1 || limit == null || input == null) return 0;
//    //        return Math.Min(input.Total80CDeductionClaimed, limit.MaximumAmount ?? 0);
//    //    }


//    //    private static readonly IReadOnlyDictionary<string, string> CodeToDictKey =
//    //new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
//    //{
//    //    //[""] = "HRAExemption",
//    //    ["HRA_EXEMPTIONNONMETRO"] = "HRAExemption",
//    //    ["ENTERTAINMENT_PRO_TAX"] = "Entertainment_Professional_Tax",
//    //    ["EMPLOYEE_NPS"] = "EmployeeContributionToNPS",
//    //    ["LTA"] = "LTAAllowance",
//    //    ["MED_INS_80D"] = "MedicalInsurance80D",
//    //    ["DISABLED_80U"] = "DisabledIndividual80U",
//    //    ["EV_LOAN_80EEB"] = "ElectricVehicleLoan",
//    //    ["EMPLOYER_NPS_CONTRI"] = "EmployerContributionToNPS",
//    //    ["HOME_LOAN_SELF"] = "InterestHomeLoanSelf",
//    //    ["HOME_LOAN_LET_OUT"] = "InterestHomeLoanLetOut",
//    //    ["DONATION_HRA_EXEMPTIONMETRO80G"] = "DonationToPoliticalParty",
//    //    ["CONVEYANCE_ALLOW"] = "ConveyanceAllowance",
//    //    ["TRANSPORT_SPECIAL"] = "TransportAllowanceSpeciallyAbled",
//    //    ["AGNIVEER_CORPUS"] = "AgniveerCorpusFund",
//    //    ["EXEMPTION_10C"] = "Exemption10C",
//    //    ["SB_INTEREST"] = "SavingBankInterest",
//    //    ["SECTION_80C"] = "Deduction80C"
//    //};

//    //    public static decimal CalculateTotalDeduction(
//    //   Dictionary<string, object> rawDeductions,
//    //   List<DeductionLimit> limits,
//    //   decimal basicSalary,
//    //   bool isSenior,
//    //   int taxRegimeId)
//    //    {
//    //        decimal total = 0m;
//    //        var calc = new DeductionCalculator();

//    //        if (rawDeductions == null || rawDeductions.Count == 0 || limits == null || limits.Count == 0)
//    //            return 0m;

//    //        foreach (DeductionLimit limit in limits)
//    //        {
//    //            if (limit == null || string.IsNullOrWhiteSpace(limit.DeductionCode))
//    //                continue;

//    //            var code = limit.DeductionCode.Trim().ToUpperInvariant();

//    //            if (!CodeToDictKey.TryGetValue(code, out var dictKey))
//    //                continue; 

//    //            if (!rawDeductions.TryGetValue(dictKey, out var obj) || obj is null)
//    //                continue;

//    //            switch (code)
//    //            {
//    //                case "HRA_EXEMPTIONMETRO":
//    //                case "HRA_EXEMPTIONNONMETRO":
//    //                    if (obj is IHRAExemption hra)
//    //                        total += calc.CalculateHRAExemption(hra, basicSalary, limit, taxRegimeId);
//    //                    break;

//    //                case "ENTERTAINMENT_PRO_TAX":
//    //                    if (obj is IEntertainmentProfessionalTax ept)
//    //                        total += calc.CalculateEntertainmentProfessionalTax(ept, limit, taxRegimeId);
//    //                    break;

//    //                case "EMPLOYEE_NPS":
//    //                    if (obj is IEmployeeContributionToNPS empNps)
//    //                        total += calc.CalculateEmployeeContributionToNPS(empNps, limit, taxRegimeId);
//    //                    break;

//    //                case "LTA":
//    //                    if (obj is ILTAAllowance lta)
//    //                        total += calc.CalculateLTA(lta, limit, taxRegimeId);
//    //                    break;

//    //                case "MED_INS_80D":
//    //                    if (obj is IMedicalInsurancePremium80D med)
//    //                        total += calc.CalculateMedicalInsurance80D(med, isSenior, limit, taxRegimeId);
//    //                    break;

//    //                case "DISABLED_80U":
//    //                    if (obj is IDisabledIndividual80U dis)
//    //                        total += calc.CalculateDisabledIndividual80U(dis, limit, taxRegimeId);
//    //                    break;

//    //                case "EV_LOAN_80EEB":
//    //                    if (obj is IInterestOnElectricVehicleLoan ev)
//    //                        total += calc.CalculateEVLoanInterest80EEB(ev, limit, taxRegimeId);
//    //                    break;

//    //                case "EMPLOYER_NPS_CONTRI":
//    //                    if (obj is IEmployerContributionToNPS emp)
//    //                        total += calc.CalculateEmployerContributionToNPS(emp, basicSalary, limit, taxRegimeId);
//    //                    break;

//    //                case "HOME_LOAN_SELF":
//    //                    if (obj is IInterestOnHomeLoanSelf self)
//    //                        total += calc.CalculateInterestHomeLoanSelf(self, limit, taxRegimeId);
//    //                    break;

//    //                case "HOME_LOAN_LET_OUT":
//    //                    if (obj is IInterestOnHomeLoanLetOut letOut)
//    //                        total += calc.CalculateInterestHomeLoanLetOut(letOut, limit, taxRegimeId);
//    //                    break;

//    //                case "DONATION_80G":
//    //                    if (obj is IDonationToPoliticalPartyTrust don)
//    //                        total += calc.CalculateDonation80G(don, true, limit, taxRegimeId);
//    //                    break;

//    //                case "CONVEYANCE_ALLOW":
//    //                    if (obj is IConveyanceAllowance con)
//    //                        total += calc.CalculateConveyanceAllowance(con, limit, taxRegimeId);
//    //                    break;

//    //                case "TRANSPORT_SPECIAL":
//    //                    if (obj is ITransportAllowanceSpeciallyAbled ta)
//    //                        total += calc.CalculateTransportAllowanceSpeciallyAbled(ta, limit, taxRegimeId);
//    //                    break;

//    //                case "AGNIVEER_CORPUS":
//    //                    if (obj is IAgniveerCorpusFund agn)
//    //                        total += calc.CalculateAgniveerCorpusFund(agn, limit, taxRegimeId);
//    //                    break;

//    //                case "EXEMPTION_10C":
//    //                    if (obj is IExemption10C ex10c)
//    //                        total += calc.CalculateExemption10C(ex10c, limit, taxRegimeId);
//    //                    break;

//    //                case "SB_INTEREST":
//    //                    if (obj is ISavingBankInterest sbi)
//    //                        total += calc.CalculateSavingBankInterest(sbi, isSenior, limit, taxRegimeId);
//    //                    break;

//    //                case "SECTION_80C":
//    //                    if (obj is IDeduction80C d80c)
//    //                        total += calc.CalculateDeduction80C(d80c, limit, taxRegimeId);
//    //                    break;
//    //            }
//    //        }

//    //        return total;
//    //    }

//        public decimal CalculateHRAExemption(IHRAExemption hra, decimal basicSalary, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || hra == null) return 0;
//            decimal salaryPercentage = (limit.PercentageOfSalary ?? 0) / 100m;
//            decimal rentExcess = hra.RentPaidAnnually - (basicSalary * 0.10m); // 10% of salary
//            decimal exemption = Math.Min(hra.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
//            return Math.Max(0, exemption);
//        }

//        public decimal CalculateEntertainmentProfessionalTax(IEntertainmentProfessionalTax input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal entLimit = Math.Min(limit.MaximumAmount ?? 0, input.EntertainmentAllowanceReceived);
//            decimal proTaxLimit = Math.Min(limit.MaximumAmount ?? 0, input.ProfessionalTaxPaid);
//            return entLimit + proTaxLimit;
//        }

//        public decimal CalculateEmployeeContributionToNPS(IEmployeeContributionToNPS input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.Amount80CCD1 + input.Amount80CCD1B);
//        }

//        public decimal CalculateLTA(ILTAAllowance input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(input.ClaimedAmount, input.ActualTravelExpense);
//        }

//        public decimal CalculateMedicalInsurance80D(IMedicalInsurancePremium80D input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal total = input.PremiumPaidSelfFamily + input.PreventiveHealthCheckup;
//            return Math.Min(limit.MaximumAmount ?? 0, total);
//        }

//        public decimal CalculateDisabledIndividual80U(IDisabledIndividual80U input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return limit.MaximumAmount ?? 0;
//        }

//        public decimal CalculateEVLoanInterest80EEB(IInterestOnElectricVehicleLoan input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//        }

//        public decimal CalculateEmployerContributionToNPS(IEmployerContributionToNPS input, decimal salary, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 2 || limit == null || input == null) return 0;
//            decimal cap = (limit.PercentageOfSalary ?? 0) / 100m * salary;
//            return Math.Min(input.ContributionAmount, cap);
//        }

//        public decimal CalculateInterestHomeLoanSelf(IInterestOnHomeLoanSelf input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//        }

//        public decimal CalculateInterestHomeLoanLetOut(IInterestOnHomeLoanLetOut input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || input == null) return 0;
//            return input.InterestAmountPaid;
//        }

//        public decimal CalculateDonation80G(IDonationToPoliticalPartyTrust input, bool isFullExempt, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal percent = (limit.PercentageOfSalary ?? 0) / 100m;
//            return input.DonationAmount * percent;
//        }

//        public decimal CalculateConveyanceAllowance(IConveyanceAllowance input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || input == null) return 0;
//            return Math.Min(input.AmountReceived, input.ExemptAmount);
//        }

//        public decimal CalculateTransportAllowanceSpeciallyAbled(ITransportAllowanceSpeciallyAbled input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.AmountReceived);
//        }

//        public decimal CalculateAgniveerCorpusFund(IAgniveerCorpusFund input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 2 || input == null) return 0;
//            return input.EmployeeContribution + input.GovtContribution;
//        }

//        public decimal CalculateExemption10C(IExemption10C input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.ExemptAmount);
//        }

//        public decimal CalculateSavingBankInterest(ISavingBankInterest input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestEarned);
//        }

//        public decimal CalculateDeduction80C(IDeduction80C input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(input.Total80CDeductionClaimed, limit.MaximumAmount ?? 0);
//        }
//    }

//    // Corrected CodeToDictKey dictionary
//    private static readonly IReadOnlyDictionary<string, string> CodeToDictKey =
//        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
//        {
//            ["HRA_EXEMPTIONNONMETRO"] = "HRAExemption",
//            ["HRA_EXEMPTIONMETRO"] = "HRAExemption",
//            ["ENTERTAINMENT_PRO_TAX"] = "Entertainment_Professional_Tax",
//            ["EMPLOYEE_NPS"] = "EmployeeContributionToNPS",
//            ["LTA"] = "LTAAllowance",
//            ["MED_INS_80D"] = "MedicalInsurance80D",
//            ["DISABLED_80U"] = "DisabledIndividual80U",
//            ["EV_LOAN_80EEB"] = "ElectricVehicleLoan",
//            ["EMPLOYER_NPS_CONTRI"] = "EmployerContributionToNPS",
//            ["HOME_LOAN_SELF"] = "InterestHomeLoanSelf",
//            ["HOME_LOAN_LET_OUT"] = "InterestHomeLoanLetOut",
//            ["DONATION_80G"] = "DonationToPoliticalPartyTrust",
//            ["CONVEYANCE_ALLOW"] = "ConveyanceAllowance",
//            ["TRANSPORT_SPECIAL"] = "TransportAllowanceSpeciallyAbled",
//            ["AGNIVEER_CORPUS"] = "AgniveerCorpusFund",
//            ["EXEMPTION_10C"] = "Exemption10C",
//            ["SB_INTEREST"] = "SavingBankInterest",
//            ["SECTION_80C"] = "Deduction80C"
//        };

//    public static decimal CalculateTotalDeduction(
//        Dictionary<string, object> rawDeductions,
//        List<DeductionLimit> limits,
//        decimal basicSalary,
//        bool isSenior,
//        int taxRegimeId)
//    {
//        decimal total = 0m;
//        var calc = new DeductionCalculator();

//        if (rawDeductions == null || rawDeductions.Count == 0 || limits == null || limits.Count == 0)
//            return 0m;

//        foreach (DeductionLimit limit in limits)
//        {
//            if (limit == null || string.IsNullOrWhiteSpace(limit.DeductionCode))
//                continue;

//            var code = limit.DeductionCode.Trim().ToUpperInvariant();

//            if (!CodeToDictKey.TryGetValue(code, out var dictKey))
//                continue;

//            if (!rawDeductions.TryGetValue(dictKey, out var obj) || obj is null)
//                continue;

//            switch (code)
//            {
//                case "HRA_EXEMPTIONMETRO":
//                case "HRA_EXEMPTIONNONMETRO":
//                    if (obj is IHRAExemption hra)
//                        total += calc.CalculateHRAExemption(hra, basicSalary, limit, taxRegimeId);
//                    break;

//                case "ENTERTAINMENT_PRO_TAX":
//                    if (obj is IEntertainmentProfessionalTax ept)
//                        total += calc.CalculateEntertainmentProfessionalTax(ept, limit, taxRegimeId);
//                    break;

//                case "EMPLOYEE_NPS":
//                    if (obj is IEmployeeContributionToNPS empNps)
//                        total += calc.CalculateEmployeeContributionToNPS(empNps, limit, taxRegimeId);
//                    break;

//                case "LTA":
//                    if (obj is ILTAAllowance lta)
//                        total += calc.CalculateLTA(lta, limit, taxRegimeId);
//                    break;

//                case "MED_INS_80D":
//                    if (obj is IMedicalInsurancePremium80D med)
//                        total += calc.CalculateMedicalInsurance80D(med, isSenior, limit, taxRegimeId);
//                    break;

//                case "DISABLED_80U":
//                    if (obj is IDisabledIndividual80U dis)
//                        total += calc.CalculateDisabledIndividual80U(dis, limit, taxRegimeId);
//                    break;

//                case "EV_LOAN_80EEB":
//                    if (obj is IInterestOnElectricVehicleLoan ev)
//                        total += calc.CalculateEVLoanInterest80EEB(ev, limit, taxRegimeId);
//                    break;

//                case "EMPLOYER_NPS_CONTRI":
//                    if (obj is IEmployerContributionToNPS emp)
//                        total += calc.CalculateEmployerContributionToNPS(emp, basicSalary, limit, taxRegimeId);
//                    break;

//                case "HOME_LOAN_SELF":
//                    if (obj is IInterestOnHomeLoanSelf self)
//                        total += calc.CalculateInterestHomeLoanSelf(self, limit, taxRegimeId);
//                    break;

//                case "HOME_LOAN_LET_OUT":
//                    if (obj is IInterestOnHomeLoanLetOut letOut)
//                        total += calc.CalculateInterestHomeLoanLetOut(letOut, limit, taxRegimeId);
//                    break;

//                case "DONATION_80G":
//                    if (obj is IDonationToPoliticalPartyTrust don)
//                        total += calc.CalculateDonation80G(don, true, limit, taxRegimeId);
//                    break;

//                case "CONVEYANCE_ALLOW":
//                    if (obj is IConveyanceAllowance con)
//                        total += calc.CalculateConveyanceAllowance(con, limit, taxRegimeId);
//                    break;

//                case "TRANSPORT_SPECIAL":
//                    if (obj is ITransportAllowanceSpeciallyAbled ta)
//                        total += calc.CalculateTransportAllowanceSpeciallyAbled(ta, limit, taxRegimeId);
//                    break;

//                case "AGNIVEER_CORPUS":
//                    if (obj is IAgniveerCorpusFund agn)
//                        total += calc.CalculateAgniveerCorpusFund(agn, limit, taxRegimeId);
//                    break;

//                case "EXEMPTION_10C":
//                    if (obj is IExemption10C ex10c)
//                        total += calc.CalculateExemption10C(ex10c, limit, taxRegimeId);
//                    break;

//                case "SB_INTEREST":
//                    if (obj is ISavingBankInterest sbi)
//                        total += calc.CalculateSavingBankInterest(sbi, isSenior, limit, taxRegimeId);
//                    break;

//                case "SECTION_80C":
//                    if (obj is IDeduction80C d80c)
//                        total += calc.CalculateDeduction80C(d80c, limit, taxRegimeId);
//                    break;
//            }
//        }

//        return total;
//    }
//}


//    public class DeductionCalculator
//    {
//        public decimal CalculateHRAExemption(IHRAExemption hra, decimal basicSalary, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || hra == null) return 0;
//            decimal salaryPercentage = (limit.PercentageOfSalary ?? 0) / 100m;
//            decimal rentExcess = hra.RentPaidAnnually - (basicSalary * 0.10m); // 10% of salary
//            decimal exemption = Math.Min(hra.ActualHRAReceived, Math.Min(salaryPercentage * basicSalary, rentExcess));
//            Console.WriteLine(exemption);
//            Console.WriteLine(limit.PercentageOfSalary);
//            return Math.Max(0, exemption);
//        }

//        public decimal CalculateEntertainmentProfessionalTax(IEntertainmentProfessionalTax input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal entLimit = Math.Min(limit.MaximumAmount ?? 0, input.EntertainmentAllowanceReceived);
//            decimal proTaxLimit = Math.Min(limit.MaximumAmount ?? 0, input.ProfessionalTaxPaid);
//            Console.WriteLine(entLimit + proTaxLimit);Console.WriteLine(limit.MaximumAmount);
//            return entLimit + proTaxLimit;
//        }

//        public decimal CalculateEmployeeContributionToNPS(IEmployeeContributionToNPS input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            Console.WriteLine(limit.MaximumAmount);
//            Console.WriteLine(input.Amount80CCD1 + input.Amount80CCD1B);
//            return Math.Min(limit.MaximumAmount ?? 0, input.Amount80CCD1 + input.Amount80CCD1B);
//        }

//        public decimal CalculateLTA(ILTAAllowance input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            Console.WriteLine(input.ActualTravelExpense);
//            Console.WriteLine(input.ClaimedAmount);
//            return Math.Min(input.ClaimedAmount, input.ActualTravelExpense);
//        }

//        public decimal CalculateMedicalInsurance80D(IMedicalInsurancePremium80D input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal total = input.PremiumPaidSelfFamily + input.PreventiveHealthCheckup;
//            return Math.Min(limit.MaximumAmount ?? 0, total);
//        }

//        public decimal CalculateDisabledIndividual80U(IDisabledIndividual80U input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return limit.MaximumAmount ?? 0;
//        }

//        public decimal CalculateEVLoanInterest80EEB(IInterestOnElectricVehicleLoan input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//        }

//        public decimal CalculateEmployerContributionToNPS(IEmployerContributionToNPS input, decimal salary, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 2 || limit == null || input == null) return 0;
//            decimal cap = (limit.PercentageOfSalary ?? 0) / 100m * salary;
//            return Math.Min(input.ContributionAmount, cap);
//        }

//        public decimal CalculateInterestHomeLoanSelf(IInterestOnHomeLoanSelf input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestAmountPaid);
//        }

//        public decimal CalculateInterestHomeLoanLetOut(IInterestOnHomeLoanLetOut input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || input == null) return 0;
//            return input.InterestAmountPaid;
//        }

//        public decimal CalculateDonation80G(IDonationToPoliticalPartyTrust input, bool isFullExempt, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            decimal percent = (limit.PercentageOfSalary ?? 0) / 100m;
//            return input.DonationAmount * percent;
//        }

//        public decimal CalculateConveyanceAllowance(IConveyanceAllowance input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || input == null) return 0;
//            return Math.Min(input.AmountReceived, input.ExemptAmount);
//        }

//        public decimal CalculateTransportAllowanceSpeciallyAbled(ITransportAllowanceSpeciallyAbled input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.AmountReceived);
//        }

//        public decimal CalculateAgniveerCorpusFund(IAgniveerCorpusFund input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 2 || input == null) return 0;
//            return input.EmployeeContribution + input.GovtContribution;
//        }

//        public decimal CalculateExemption10C(IExemption10C input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.ExemptAmount);
//        }

//        public decimal CalculateSavingBankInterest(ISavingBankInterest input, bool isSenior, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(limit.MaximumAmount ?? 0, input.InterestEarned);
//        }

//        public decimal CalculateDeduction80C(IDeduction80C input, DeductionLimit limit, int taxRegimeId)
//        {
//            if (taxRegimeId != 1 || limit == null || input == null) return 0;
//            return Math.Min(input.Total80CDeductionClaimed, limit.MaximumAmount ?? 0);
//        }


//// Corrected CodeToDictKey dictionary
//  public static readonly IReadOnlyDictionary<string, string> CodeToDictKey =
//    new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
//    {
//        ["HRA_EXEMPTIONNONMETRO"] = "HRAExemption",
//        ["HRA_EXEMPTIONMETRO"] = "HRAExemption",
//        ["ENTERTAINMENT_PRO_TAX"] = "Entertainment_Professional_Tax",
//        ["EMPLOYEE_NPS"] = "EmployeeContributionToNPS",
//        ["LTA"] = "LTAAllowance",
//        ["MED_INS_80D"] = "MedicalInsurance80D",
//        ["DISABLED_80U"] = "DisabledIndividual80U",
//        ["EV_LOAN_80EEB"] = "ElectricVehicleLoan",
//        ["EMPLOYER_NPS_CONTRI"] = "EmployerContributionToNPS",
//        ["HOME_LOAN_SELF"] = "InterestHomeLoanSelf",
//        ["HOME_LOAN_LET_OUT"] = "InterestHomeLoanLetOut",
//        ["DONATION_80G"] = "DonationToPoliticalPartyTrust",
//        ["CONVEYANCE_ALLOW"] = "ConveyanceAllowance",
//        ["TRANSPORT_SPECIAL"] = "TransportAllowanceSpeciallyAbled",
//        ["AGNIVEER_CORPUS"] = "AgniveerCorpusFund",
//        ["EXEMPTION_10C"] = "Exemption10C",
//        ["SB_INTEREST"] = "SavingBankInterest",
//        ["SECTION_80C"] = "Deduction80C",

//    };

//        //public static decimal CalculateTotalDeduction(
//        //    Dictionary<string, object> rawDeductions,
//        //    List<DeductionLimit> limits,
//        //    decimal basicSalary,
//        //    bool isSenior,
//        //    int taxRegimeId)
//        //{
//        //    decimal total = 0m;
//        //    var calc = new DeductionCalculator();

//        //    if (rawDeductions == null || rawDeductions.Count == 0 || limits == null || limits.Count == 0)
//        //        return 0m;

//        //    foreach (DeductionLimit limit in limits.Where(l => l != null))
//        //    {
//        //        if (limit == null || string.IsNullOrWhiteSpace(limit.DeductionCode))
//        //            continue;

//        //        var code = limit.DeductionCode.Trim().ToUpperInvariant();

//        //        if (!CodeToDictKey.TryGetValue(code, out var dictKey))
//        //            continue;

//        //        //if (!rawDeductions.TryGetValue(dictKey, out var obj) || obj is null)
//        //        //    continue;
//        //        if (!rawDeductions.TryGetValue(code, out var obj) || obj is null)
//        //        {
//        //            Console.WriteLine($"Missing or null key: {code}");
//        //            Console.WriteLine("Raw deductions keys: " + string.Join(", ", rawDeductions.Keys));

//        //            continue;
//        //        }


//        //        switch (code)
//        //        {
//        //            case "HRA_EXEMPTIONMETRO":
//        //            case "HRA_EXEMPTIONNONMETRO":
//        //                if (obj is IHRAExemption hra)
//        //                    total += calc.CalculateHRAExemption(hra, basicSalary, limit, taxRegimeId);
//        //                break;

//        //            case "ENTERTAINMENT_PRO_TAX":
//        //                if (obj is IEntertainmentProfessionalTax ept)
//        //                    total += calc.CalculateEntertainmentProfessionalTax(ept, limit, taxRegimeId);
//        //                break;

//        //            case "EMPLOYEE_NPS":
//        //                if (obj is IEmployeeContributionToNPS empNps)
//        //                    total += calc.CalculateEmployeeContributionToNPS(empNps, limit, taxRegimeId);
//        //                break;

//        //            case "LTA":
//        //                if (obj is ILTAAllowance lta)
//        //                    total += calc.CalculateLTA(lta, limit, taxRegimeId);
//        //                break;

//        //            case "MED_INS_80D":
//        //                if (obj is IMedicalInsurancePremium80D med)
//        //                    total += calc.CalculateMedicalInsurance80D(med, isSenior, limit, taxRegimeId);
//        //                break;

//        //            case "DISABLED_80U":
//        //                if (obj is IDisabledIndividual80U dis)
//        //                    total += calc.CalculateDisabledIndividual80U(dis, limit, taxRegimeId);
//        //                break;

//        //            case "EV_LOAN_80EEB":
//        //                if (obj is IInterestOnElectricVehicleLoan ev)
//        //                    total += calc.CalculateEVLoanInterest80EEB(ev, limit, taxRegimeId);
//        //                break;

//        //            case "EMPLOYER_NPS_CONTRI":
//        //                if (obj is IEmployerContributionToNPS emp)
//        //                    total += calc.CalculateEmployerContributionToNPS(emp, basicSalary, limit, taxRegimeId);
//        //                break;

//        //            case "HOME_LOAN_SELF":
//        //                if (obj is IInterestOnHomeLoanSelf self)
//        //                    total += calc.CalculateInterestHomeLoanSelf(self, limit, taxRegimeId);
//        //                break;

//        //            case "HOME_LOAN_LET_OUT":
//        //                if (obj is IInterestOnHomeLoanLetOut letOut)
//        //                    total += calc.CalculateInterestHomeLoanLetOut(letOut, limit, taxRegimeId);
//        //                break;

//        //            case "DONATION_80G":
//        //                if (obj is IDonationToPoliticalPartyTrust don)
//        //                    total += calc.CalculateDonation80G(don, true, limit, taxRegimeId);
//        //                break;

//        //            case "CONVEYANCE_ALLOW":
//        //                if (obj is IConveyanceAllowance con)
//        //                    total += calc.CalculateConveyanceAllowance(con, limit, taxRegimeId);
//        //                break;

//        //            case "TRANSPORT_SPECIAL":
//        //                if (obj is ITransportAllowanceSpeciallyAbled ta)
//        //                    total += calc.CalculateTransportAllowanceSpeciallyAbled(ta, limit, taxRegimeId);
//        //                break;

//        //            case "AGNIVEER_CORPUS":
//        //                if (obj is IAgniveerCorpusFund agn)
//        //                    total += calc.CalculateAgniveerCorpusFund(agn, limit, taxRegimeId);
//        //                break;

//        //            case "EXEMPTION_10C":
//        //                if (obj is IExemption10C ex10c)
//        //                    total += calc.CalculateExemption10C(ex10c, limit, taxRegimeId);
//        //                break;

//        //            case "SB_INTEREST":
//        //                if (obj is ISavingBankInterest sbi)
//        //                    total += calc.CalculateSavingBankInterest(sbi, isSenior, limit, taxRegimeId);
//        //                break;

//        //            case "SECTION_80C":
//        //                if (obj is IDeduction80C d80c)
//        //                    total += calc.CalculateDeduction80C(d80c, limit, taxRegimeId);
//        //                break;
//        //        }
//        //    }

//        //    return total;

//        public static decimal CalculateTotalDeduction(
//     Dictionary<string, object> deductions,
//     List<DeductionLimit> limits,
//     decimal basicSalary,
//     bool isSenior,
//     int taxRegimeId)
//        {
//            decimal total = 0m;
//            var calc = new DeductionCalculator();

//            if (deductions == null || deductions.Count == 0 || limits == null || limits.Count == 0)
//                return 0m;

//            var normalized = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

//            foreach (var kv in deductions)
//            {
//                var incomingKey = (kv.Key ?? string.Empty).Trim();

//                if (CodeToDictKey.TryGetValue(incomingKey, out var dictKey))
//                {
//                    normalized[dictKey] = kv.Value; 
//                }
//                else
//                {
//                    normalized[incomingKey] = kv.Value;
//                }
//            }

//            deductions = normalized;

//            Console.WriteLine("== Keys after normalization ==");
//            foreach (var k in deductions.Keys)
//                Console.WriteLine($"'{k}'");

//            foreach (DeductionLimit limit in limits.Where(l => l != null))
//            {
//                if (limit == null || string.IsNullOrWhiteSpace(limit.DeductionCode))
//                    continue;

//                var code = (limit.DeductionCode ?? string.Empty).Trim().ToUpperInvariant();

//                string expectedKey;
//                if (!CodeToDictKey.TryGetValue(code, out expectedKey))
//                {
//                    expectedKey = (limit.DeductionCode ?? string.Empty).Trim();
//                }

//                if (!deductions.TryGetValue(expectedKey, out var obj) || obj is null)
//                {
//                    if (!deductions.TryGetValue(code, out obj) || obj is null)
//                    {
//                        Console.WriteLine($"[MISS] Key not found or null. Tried: '{expectedKey}' then '{code}'.");
//                        continue;
//                    }
//                }

//                switch (code)
//                {
//                    case "HRA_EXEMPTIONMETRO":
//                    case "HRA_EXEMPTIONNONMETRO":
//                        if (obj is IHRAExemption hraObj)
//                        {
//                            total += calc.CalculateHRAExemption(hraObj, basicSalary, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "ENTERTAINMENT_PRO_TAX":
//                        if (obj is IEntertainmentProfessionalTax ept)
//                        {
//                            total += calc.CalculateEntertainmentProfessionalTax(ept, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "EMPLOYEE_NPS":
//                        if (obj is IEmployeeContributionToNPS empNps)
//                        {
//                            total += calc.CalculateEmployeeContributionToNPS(empNps, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "LTA":
//                        if (obj is ILTAAllowance lta)
//                        {
//                            total += calc.CalculateLTA(lta, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "MED_INS_80D":
//                        if (obj is IMedicalInsurancePremium80D med)
//                        {
//                            total += calc.CalculateMedicalInsurance80D(med, isSenior, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "DISABLED_80U":
//                        if (obj is IDisabledIndividual80U dis)
//                        {
//                            total += calc.CalculateDisabledIndividual80U(dis, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "EV_LOAN_80EEB":
//                        if (obj is IInterestOnElectricVehicleLoan ev)
//                        {
//                            total += calc.CalculateEVLoanInterest80EEB(ev, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "EMPLOYER_NPS_CONTRI":
//                        if (obj is IEmployerContributionToNPS emp)
//                        {
//                            total += calc.CalculateEmployerContributionToNPS(emp, basicSalary, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "HOME_LOAN_SELF":
//                        if (obj is IInterestOnHomeLoanSelf self)
//                        {
//                            total += calc.CalculateInterestHomeLoanSelf(self, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "HOME_LOAN_LET_OUT":
//                        if (obj is IInterestOnHomeLoanLetOut letOut)
//                        {
//                            total += calc.CalculateInterestHomeLoanLetOut(letOut, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "DONATION_80G":
//                        if (obj is IDonationToPoliticalPartyTrust don)
//                        {
//                            total += calc.CalculateDonation80G(don, true, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "CONVEYANCE_ALLOW":
//                        if (obj is IConveyanceAllowance con)
//                        {
//                            total += calc.CalculateConveyanceAllowance(con, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "TRANSPORT_SPECIAL":
//                        if (obj is ITransportAllowanceSpeciallyAbled ta)
//                        {
//                            total += calc.CalculateTransportAllowanceSpeciallyAbled(ta, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "AGNIVEER_CORPUS":
//                        if (obj is IAgniveerCorpusFund agn)
//                        {
//                            total += calc.CalculateAgniveerCorpusFund(agn, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "EXEMPTION_10C":
//                        if (obj is IExemption10C ex10c)
//                        {
//                            total += calc.CalculateExemption10C(ex10c, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "SB_INTEREST":
//                        if (obj is ISavingBankInterest sbi)
//                        {
//                            total += calc.CalculateSavingBankInterest(sbi, isSenior, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;

//                    case "SECTION_80C":
//                        if (obj is IDeduction80C d80c)
//                        {
//                            total += calc.CalculateDeduction80C(d80c, limit, taxRegimeId);
//                            continue;
//                        }
//                        break;
//                }

//                // 4) …otherwise, fall back to numeric amounts (what your SP returns).
//                if (TryToDecimal(obj, out var amount))
//                {
//                    // If you have per-code limit logic, apply it here.
//                    // For now, we just add the amount. You can cap with limit fields if available.
//                    total += amount;
//                    Console.WriteLine(total);
//                }
//                else
//                {
//                    Console.WriteLine($"[TYPE MISMATCH] Key '{expectedKey}' resolved from code '{code}' is type '{obj?.GetType().FullName}', not a supported interface nor numeric.");
//                }
//            }

//            return total;
//        }

//        // Helper: convert various number-like objects to decimal
//        private static bool TryToDecimal(object obj, out decimal value)
//        {
//            switch (obj)
//            {
//                case null:
//                    value = 0m;
//                    return false;
//                case decimal d:
//                    value = d;
//                    return true;
//                case int i:
//                    value = i;
//                    return true;
//                case long l:
//                    value = l;
//                    return true;
//                case double db:
//                    value = (decimal)db;
//                    return true;
//                case float f:
//                    value = (decimal)f;
//                    return true;
//                case string s when decimal.TryParse(s, out var parsed):
//                    value = parsed;
//                    return true;
//                default:
//                    value = 0m;
//                    return false;
//            }
//        }



//    }