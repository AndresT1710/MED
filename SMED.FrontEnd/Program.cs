using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SMED.FrontEnd;
using SMED.FrontEnd.Services;
using Blazored.LocalStorage;
using System.Net.Http;
using SMED.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuración del HttpClient con URL del backend en Docker
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:59166/"),
        //BaseAddress = new Uri("https://localhost:7009/"),
        
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
builder.Services.AddScoped<ToxicHabitHistoryService>();
builder.Services.AddScoped<ToxicHabitService>();
builder.Services.AddScoped<MedicationHistoryService>();
builder.Services.AddScoped<PsychopsychiatricHistoryService>();
builder.Services.AddScoped<PsychosexualHistoryService>();
builder.Services.AddScoped<CurrentProblemHistoryService>();
builder.Services.AddScoped<WorkHistoryService>();
builder.Services.AddScoped<PrenatalHistoryService>();
builder.Services.AddScoped<PostnatalHistoryService>();
builder.Services.AddScoped<PerinatalHistoryService>();
builder.Services.AddScoped<NeuropsychologicalHistoryService>();
builder.Services.AddScoped<NeurologicalExamTypeService>();
builder.Services.AddScoped<NeurologicalExamService>();
builder.Services.AddScoped<DevelopmentRecordService>();
builder.Services.AddScoped<TraumaticHistoryService>();
builder.Services.AddScoped<HospitalizationsHistoryService>();
builder.Services.AddScoped<TransfusionsHistoryService>();
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
builder.Services.AddScoped<PhysicalExamService>();
builder.Services.AddScoped<RegionService>();
builder.Services.AddScoped<PathologicalEvidenceService>();
builder.Services.AddScoped<EvolutionService>();
builder.Services.AddScoped<AdditionalDataService>();
builder.Services.AddScoped<DiagnosticTypeService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<HealthProfessionalService>();
builder.Services.AddScoped<IndicationsService>();
builder.Services.AddScoped<NonPharmacologicalTreatmentService>();
builder.Services.AddScoped<TreatmentService>();
builder.Services.AddScoped<PharmacologicalTreatmentService>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<MedicalDiagnosisService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<MedicalReferralService>();
builder.Services.AddScoped<ActionFService>();
builder.Services.AddScoped<CurrentIllnessService>();
builder.Services.AddScoped<PainMomentService>();
builder.Services.AddScoped<PainScaleService>();
builder.Services.AddScoped<ScaleService>();
builder.Services.AddScoped<SkinEvaluationService>();
builder.Services.AddScoped<ColorService>();
builder.Services.AddScoped<EdemaService>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<SwellingService>();
builder.Services.AddScoped<OsteoarticularEvaluationService>();
builder.Services.AddScoped<JointConditionService>();
builder.Services.AddScoped<JointRangeOfMotionService>();
builder.Services.AddScoped<MedicalEvaluationService>();
builder.Services.AddScoped<TypeOfMedicalEvaluationService>();
builder.Services.AddScoped<MedicalEvaluationPositionService>();
builder.Services.AddScoped<MedicalEvaluationMembersService>();
builder.Services.AddScoped<NeuromuscularEvaluationService>();
builder.Services.AddScoped<ShadeService>();
builder.Services.AddScoped<TrophismService>();
builder.Services.AddScoped<StrengthService>();
builder.Services.AddScoped<PosturalEvaluationService>();
builder.Services.AddScoped<ViewService>();



builder.Services.AddLogging();



await builder.Build().RunAsync();