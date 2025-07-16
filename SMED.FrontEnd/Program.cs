using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SMED.FrontEnd;
using SMED.FrontEnd.Services;
using Blazored.LocalStorage;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración del HttpClient con URL del backend en Docker
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        //BaseAddress = new Uri("https://localhost:59166/"),
        BaseAddress = new Uri("https://localhost:7009/"),
        
        //Dirección para Docker
        //BaseAddress = new Uri("http://localhost:2023/"),
        
        Timeout = TimeSpan.FromSeconds(30)
    };

    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

    return httpClient;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ClinicalHistoryService>();
builder.Services.AddScoped<DiseaseService>();
builder.Services.AddScoped<PersonalHistoryService>();
builder.Services.AddScoped<SurgeryService>();
builder.Services.AddScoped<SurgeryHistoryService>();
builder.Services.AddScoped<AllergyHistoryService>();
builder.Services.AddScoped<AllergyService>();
builder.Services.AddScoped<HabitService>();
builder.Services.AddScoped<HabitHistoryService>();
builder.Services.AddScoped<RelationshipService>();
builder.Services.AddScoped<FamilyHistoryDetailService>();
builder.Services.AddScoped<ObstetricHistoryService>();
builder.Services.AddScoped<GynecologicalHistoryService>();
builder.Services.AddScoped<SportsActivitiesService>();
builder.Services.AddScoped<SportsActivitiesHistoryService>();
builder.Services.AddScoped<LifeStyleService>();
builder.Services.AddScoped<LifeStyleHistoryService>();
builder.Services.AddScoped<DietaryHabitsHistoryService>();
builder.Services.AddScoped<SleepHabitService>();
builder.Services.AddScoped<SleepHabitHistoryService>();
builder.Services.AddScoped<FoodService>();
builder.Services.AddScoped<FoodConsumptionHistoryService>();
builder.Services.AddScoped<WaterConsumptionHistoryService>();
builder.Services.AddScoped<MedicalCareService>();
builder.Services.AddScoped<TypeOfServiceService>();
builder.Services.AddScoped<CostOfServiceService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<TypeOfProceduresService>();
builder.Services.AddScoped<ProceduresService>();
builder.Services.AddScoped<VitalSignsService>();
builder.Services.AddScoped<HealthProfessionalService>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<CostOfServiceService>();
builder.Services.AddScoped<TypeOfServiceService>();
builder.Services.AddScoped<TypeOfProceduresService>();
builder.Services.AddScoped<ProceduresService>();
builder.Services.AddScoped<MedicalServiceService>();
builder.Services.AddScoped<MedicalProcedureService>();
builder.Services.AddScoped<PlaceOfAttentionService>();
builder.Services.AddScoped<ReasonForConsultationService>();
builder.Services.AddScoped<ReviewSystemDevicesService>();
builder.Services.AddScoped<SystemsDevicesService>();

builder.Services.AddLogging();



await builder.Build().RunAsync();