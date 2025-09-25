using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SMED.BackEnd.Repositories.Implementations;
using SMED.BackEnd.Repositories.Interface;
using SMED.BackEnd.Services.Implementations;
using SMED.BackEnd.Services.Interface;
using SMED.Shared.DTOs;
using System.Text;
using SGIS.Models;
using SMED.Shared.Entity;
using SMED.BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// 2. Database
builder.Services.AddDbContext<SGISContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Services and repositories
builder.Services.AddScoped<IRepository<UserDTO, int>, UserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<ClinicalHistoryRepository>();
builder.Services.AddScoped<IRepository<AllergyDTO, int>, AllergyRepository>();
builder.Services.AddScoped<IRepository<AllergyHistoryDTO, int>, AllergyHistoryRepository>();
builder.Services.AddScoped<IRepository<DiseaseTypeDTO, int>, DiseaseTypeRepository>();
builder.Services.AddScoped<IRepository<DiseaseDTO, int>, DiseaseRepository>();
builder.Services.AddScoped<IRepository<PersonalHistoryDTO, int>, PersonalHistoryRepository>();
builder.Services.AddScoped<IRepository<ObstetricHistoryDTO, int>, ObstetricHistoryRepository>();
builder.Services.AddScoped<IRepository<FamilyHistoryDetailDTO, int>, FamilyHistoryDetailRepository>();
builder.Services.AddScoped<IRepository<GynecologicalHistoryDTO, int>, GynecologicalHistoryRepository>();
builder.Services.AddScoped<IRepository<ToxicHabitDTO, int>, ToxicHabitRepository>();
builder.Services.AddScoped<IRepository<ToxicHabitHistoryDTO, int>, ToxicHabitHistoryRepository>();
builder.Services.AddScoped<IRepository<SurgeryDTO, int>, SurgeryRepository>();
builder.Services.AddScoped<IRepository<SurgeryHistoryDTO, int>, SurgeryHistoryRepository>();
builder.Services.AddScoped<IRepository<FoodDTO, int>, FoodRepository>();
builder.Services.AddScoped<IRepository<FoodIntoleranceHistoryDTO, int>, FoodIntoleranceHistoryRepository>();
builder.Services.AddScoped<IClinicalHistoryRepository, ClinicalHistoryRepository>();
builder.Services.AddScoped<IRepository<PatientDTO, int>, PatientRepository>();
builder.Services.AddScoped<IClinicalHistoryPatientRepository, PatientRepository>();
builder.Services.AddScoped<IRepository<HabitsDTO, int>, HabitsRepository>();
builder.Services.AddScoped<IRepository<HabitHistoryDTO, int>, HabitHistoryRepository>();
builder.Services.AddScoped<IRepository<RelationshipDTO, int>, RelationshipRepository>();
builder.Services.AddScoped<IRepository<SportsActivitiesDTO, int>, SportsActivitiesRepository>();
builder.Services.AddScoped<IRepository<SportsActivitiesHistoryDTO, int>, SportsActivitiesHistoryRepository>();
builder.Services.AddScoped<IRepository<LifeStyleDTO, int>, LifeStyleRepository>();
builder.Services.AddScoped<IRepository<LifeStyleHistoryDTO, int>, LifeStyleHistoryRepository>();
builder.Services.AddScoped<IRepository<DietaryHabitsHistoryDTO, int>, DietaryHabitsHistoryRepository>();
builder.Services.AddScoped<IRepository<SleepHabitDTO, int>, SleepHabitRepository>();
builder.Services.AddScoped<IRepository<SleepHabitHistoryDTO, int>, SleepHabitHistoryRepository>();
builder.Services.AddScoped<IRepository<FoodConsumptionHistoryDTO, int>, FoodConsumptionHistoryRepository>();
builder.Services.AddScoped<IRepository<WaterConsumptionHistoryDTO, int>, WaterConsumptionHistoryRepository>();
builder.Services.AddScoped<IRepository<MedicationHistoryDTO, int>, MedicationHistoryRepository>();
builder.Services.AddScoped<IRepository<PsychopsychiatricHistoryDTO, int>, PsychopsychiatricHistoryRepository>();
builder.Services.AddScoped<IRepository<CurrentProblemHistoryDTO, int>, CurrentProblemHistoryRepository>();
builder.Services.AddScoped<IRepository<WorkHistoryDTO, int>, WorkHistoryRepository>();
builder.Services.AddScoped<IRepository<PsychosexualHistoryDTO, int>,PsychosexualHistoryRepository>();
builder.Services.AddScoped<IRepository<PrenatalHistoryDTO, int>, PrenatalHistoryRepository>();
builder.Services.AddScoped<IRepository<PostnatalHistoryDTO, int>, PostnatalHistoryRepository>();
builder.Services.AddScoped<IRepository<PerinatalHistoryDTO, int>, PerinatalHistoryRepository>();
builder.Services.AddScoped<IRepository<NeuropsychologicalHistoryDTO, int>, NeuropsychologicalHistoryRepository>();
builder.Services.AddScoped<IRepository<NeurologicalExamTypeDTO, int>, NeurologicalExamTypeRepository>();
builder.Services.AddScoped<IRepository<NeurologicalExamDTO, int>, NeurologicalExamRepository>();
builder.Services.AddScoped<IRepository<DevelopmentRecordDTO, int>, DevelopmentRecordRepository>();
builder.Services.AddScoped<IRepository<TypeOfServiceDTO, int>, TypeOfServiceRepository>();
builder.Services.AddScoped<IRepository<CostOfServiceDTO, int>, CostOfServiceRepository>();
builder.Services.AddScoped<IRepository<ServiceDTO, int>, ServiceRepository>();
builder.Services.AddScoped<IRepository<TypeOfProceduresDTO, int>, TypeOfProceduresRepository>();
builder.Services.AddScoped<IRepository<ProceduresDTO, int>, ProceduresRepository>();
builder.Services.AddScoped<IRepository<HealthProfessionalDTO, int>, HealthProfessionalRepository>();
builder.Services.AddScoped<IRepository<MedicalCareDTO, int>, MedicalCareRepository>();
builder.Services.AddScoped<IRepository<VitalSignsDTO, int>, VitalSignsRepository>();
builder.Services.AddScoped<IRepository<MedicalDiagnosisDTO, int>, MedicalDiagnosisRepository>();
builder.Services.AddScoped<IRepository<OrdersDTO, int>, OrdersRepository>();
builder.Services.AddScoped<IRepository<ImageOrdersDTO, int>, ImageOrdersRepository>();
builder.Services.AddScoped<IRepository<LaboratoryOrdersDTO, int>, LaboratoryOrdersRepository>();
builder.Services.AddScoped<IRepository<InterconsultationDTO, int>, InterconsultationRepository>();
builder.Services.AddScoped<IRepository<MedicalReferralDTO, int>, MedicalReferralRepository>();
builder.Services.AddScoped<IRepository<SystemsDevicesDTO, int>, SystemsDevicesRepository>();
builder.Services.AddScoped<IRepository<ReviewSystemDevicesDTO, int>, ReviewSystemDevicesRepository>();
builder.Services.AddScoped<IRepository<PlaceOfAttentionDTO, int>, PlaceOfAttentionRepository>();
builder.Services.AddScoped<IRepository<PhysicalExamDTO, int>, PhysicalExamRepository>();
builder.Services.AddScoped<PhysicalExamRepository>();
builder.Services.AddScoped<IRepository<PhysicalExamTypeDTO, int>, PhysicalExamTypeRepository>();
builder.Services.AddScoped<IRepository<IdentifiedDiseaseDTO, int>, IdentifiedDiseaseRepository>();
builder.Services.AddScoped<IRepository<ExamResultsDTO, int>, ExamResultsRepository>();
builder.Services.AddScoped<IRepository<ExamTypeDTO, int>, ExamTypeRepository>();
builder.Services.AddScoped<IRepository<EvolutionDTO, int>, EvolutionRepository>();
builder.Services.AddScoped<IRepository<MedicineDTO, int>, MedicineRepository>();
builder.Services.AddScoped<IRepository<TreatmentDTO, int>, TreatmentRepository>();
builder.Services.AddScoped<TreatmentRepository>();
builder.Services.AddScoped<IRepository<NonPharmacologicalTreatmentDTO, int>, NonPharmacologicalTreatmentRepository>();
builder.Services.AddScoped<IRepository<PharmacologicalTreatmentDTO, int>, PharmacologicalTreatmentRepository>();
builder.Services.AddScoped<IRepository<OrderDiagnosisDTO, int>, OrderDiagnosisRepository>();
builder.Services.AddScoped<IRepository<ReasonForConsultationDTO, int>, ReasonForConsultationRepository>();
builder.Services.AddScoped<IRepository<IndicationsDTO, int>, IndicationsRepository>();
builder.Services.AddScoped<IRepository<MedicalServiceDTO, int>, MedicalServiceRepository>();
builder.Services.AddScoped<IRepository<MedicalProcedureDTO, int>, MedicalProcedureRepository>();
builder.Services.AddScoped<MedicalCareRepository>();
builder.Services.AddScoped<IRepository<RegionDTO, int>, RegionRepository>();
builder.Services.AddScoped<IRepository<PathologicalEvidenceDTO, int>, PathologicalEvidenceRepository>();
builder.Services.AddScoped<IRepository<AdditionalDataDTO, int>, AdditionalDataRepository>();
builder.Services.AddScoped<IRepository<DiagnosticTypeDTO, int>, DiagnosticTypeRepository>();
builder.Services.AddScoped<MedicalDiagnosisRepository>();
builder.Services.AddScoped<PharmacologicalTreatmentRepository>();
builder.Services.AddScoped<NonPharmacologicalTreatmentRepository>();
builder.Services.AddScoped<IndicationsRepository>();
builder.Services.AddScoped<MedicineRepository>();
builder.Services.AddScoped<LocationRepository>();
builder.Services.AddScoped<IRepository<LocationDTO, int>, LocationRepository>();




// 4. Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// 5. CORS - CONFIGURACIÓN TEMPORAL PARA DESARROLLO
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS debe ir ANTES que todo lo demás
app.UseCors();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();