using Tax_Calculator.Api.Interface;
using Tax_Calculator.Api.Services;
using Tax_Calculator.Interfaces;
using Tax_Calculator.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<TaxComputationService>();
//builder.Services.AddScoped<IDeductionAggregatorService1, DeductionAggregatorService1>();
builder.Services.AddScoped<IDeductionAggregatorService1, DeductionAggregatorService1>();
builder.Services.AddScoped<IIncomeAggregatorService, IncomeAggregatorService>();
builder.Services.AddScoped<IDeductionLimitService, DeductionLimitService>();
builder.Services.AddScoped<IIncomeFromSalaryService, IncomeService>();
builder.Services.AddScoped<IDigitalAssetIncomeService, DigitalAssetIncomeService>();
builder.Services.AddScoped<IIncomeFromRentService, IncomeFromRentService>();
builder.Services.AddScoped<IHomeLoanLetoutService, HomeLoanLetoutService>();
builder.Services.AddScoped<IBasicDetailsService, BasicDetailsService>();
builder.Services.AddScoped<IHRAExemptionService, HRAExemptionService>();    
builder.Services.AddScoped<ITaxRegimeService, TaxRegimeService>();
builder.Services.AddScoped<IEntertainmentProfessionalTaxService, EntertainmentProfessionalTaxService>();
builder.Services.AddScoped<IEmployeeContributionToNPSService, EmployeeContributionToNPSService>();
builder.Services.AddScoped<ILTAAllowanceService, LTAAllowanceService>();
builder.Services.AddScoped<IMedicalInsurance80DService, MedicalInsurance80DService>();
builder.Services.AddScoped<IDisabledIndividual80UService, DisabledIndividual80UService>();  
builder.Services.AddScoped<IInterestOnElectricVehicleLoanService, InterestOnElectricVehicleLoanService>();  
builder.Services.AddScoped<IEmployerContributionToNPSService, EmployerContributionToNPSService>();
builder.Services.AddScoped<IInterestOnHomeLoanSelfService, InterestOnHomeLoanSelfService>();
builder.Services.AddScoped<IInterestOnHomeLoanLetOutService, InterestOnHomeLoanLetOutService>();
builder.Services.AddScoped<ITransportAllowanceSpeciallyAbledService, TransportAllowanceSpeciallyAbledService>();
builder.Services.AddScoped<IConveyanceAllowanceService, ConveyanceAllowanceService>();
builder.Services.AddScoped<IDonationToPoliticalPartyTrustService, DonationToPoliticalPartyTrustService>();
builder.Services.AddScoped<IAgniveerCorpusFundService, AgniveerCorpusFundService>();
builder.Services.AddScoped<IExemption10CService, Exemption10CService>();    
builder.Services.AddScoped<ISavingBankInterestService, SavingBankInterestService>();
builder.Services.AddScoped<IDeduction80CService, Deduction80CService>(); 
builder.Services.AddScoped<ITaxPayerBankAccountService, TaxPayerBankAccountService>();
builder.Services.AddScoped<ITaxPeriodService, TaxPeriodService>();
builder.Services.AddScoped<IResidentStatusService, ResidentStatusService>();
builder.Services.AddScoped<IIncomeFromInterestService,IncomeFromInterestService>();
builder.Services.AddScoped<IConnectionDetailsService, ConnectionDetailsService>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
builder.Services.AddSwaggerGen();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.MapRazorPages();    

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Or app.MapEndpoints() for Minimal APIs

app.UseHttpsRedirection();
app.UseRouting();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
