using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.Linq;
using System;
using SMED.Shared.Entity;
using Infrastructure.Models;
namespace SGIS.Models
{
    public class SGISContext : DbContext
    {
        public SGISContext(DbContextOptions<SGISContext> options)
        : base(options)
        {
        }

        // Persona y relaciones
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<HealthProfessional> HealthProfessionals { get; set; }
        public DbSet<HealthProfessionalType> HealthProfessionalTypes { get; set; }
        public DbSet<User> Users { get; set; }

        // Ubicación y datos personales
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<PersonDocument> PersonDocuments { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<PersonMaritalStatus> PersonMaritalStatuses { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<PersonBloodGroup> PersonBloodGroups { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<PersonEducation> PersonEducations { get; set; }
        public DbSet<Laterality> LateralityTypes { get; set; }
        public DbSet<PersonLaterality> PersonLateralities { get; set; }
        public DbSet<PersonResidence> PersonResidences { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<PersonProfession> PersonProfessions { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<PersonReligion> PersonReligions { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<LaborActivity> LaborActivities { get; set; }
        public DbSet<PersonLaborActivity> PersonLaborActivities { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<PatientRelationship> PatientRelationships { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<MedicalInsurance> MedicalInsurances { get; set; }
        public DbSet<PersonMedicalInsurance> PersonMedicalInsurances { get; set; }


        // Historia clínica y antecedentes
        public DbSet<ClinicalHistory> ClinicalHistories { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<AllergyHistory> AllergyHistories { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<SurgeryHistory> SurgeryHistories { get; set; }
        public DbSet<DiseaseType> DiseaseTypes { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<FamilyHistoryDetail> FamilyHistoryDetails { get; set; }

        public DbSet<GynecologicalHistory> GynecologicalHistories { get; set; }
        public DbSet<ToxicHabit> ToxicHabits { get; set; }
        public DbSet<ToxicHabitBackground> ToxicHabitHistories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodIntoleranceHistory> FoodIntoleranceHistories { get; set; }
        public DbSet<ObstetricHistory> ObstetricHistories { get; set; }
        public DbSet<PersonalHistory> PersonalHistories { get; set; }
        public DbSet<Habits> Habits { get; set; }
        public DbSet<HabitHistory> HabitHistories { get; set; }

        // Atención médica
        public DbSet<MedicalVisit> MedicalVisits { get; set; }
        public DbSet<Progress> Progresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Patient>()
            .HasKey(p => p.PersonId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.PersonNavigation)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(p => p.PersonId);




            //PROVINCE - CITY (1:N)
            modelBuilder.Entity<Province>()
                .ToTable("Pronvince")
                .HasMany(e => e.Cities)
                .WithOne(c => c.ProvinceNavigation)
                .HasForeignKey(c => c.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            //CITY - PERSONRESIDENCE (1:N)
            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasMany(e => e.PersonResidences)
                .WithOne(c => c.CityNavigation)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            //PERSON - PERSONRESIDENCE (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(e => e.PersonResidence)
                .WithOne(c => c.PersonNavigation)
                .HasForeignKey<PersonResidence>(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            //PERSON - PERSONLATERALITY (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.PersonLaterality)
                .WithOne(pl => pl.PersonNavigation)
                .HasForeignKey<PersonLaterality>(pl => pl.PersonId);

            //PERSONLATERALITY - LATERALITY (N:1)
            modelBuilder.Entity<PersonLaterality>()
                .HasOne(pl => pl.LateralityNavigation)
                .WithMany(l => l.PersonLateralities)
                .HasForeignKey(pl => pl.LateralityId);

            //PERSON - PERSONRELIGION (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.PersonReligion)
                .WithOne(pr => pr.PersonNavigation)
                .HasForeignKey<PersonReligion>(pr => pr.PersonId);

            //RELIGION - PERSONRELIGION (1:N)
            modelBuilder.Entity<PersonReligion>()
                .HasOne(pr => pr.ReligionNavigation)
                .WithMany(r => r.PersonReligions)
                .HasForeignKey(pr => pr.ReligionId);

            // PERSON - PERSONDOCUMENT (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.PersonDocument)
                .WithOne(pd => pd.PersonNavigation)
                .HasForeignKey<PersonDocument>(pd => pd.PersonId);

            //DOCUMENTTYPE - PERSONDOCUMENT (1:N)
            modelBuilder.Entity<PersonDocument>()
                .HasOne(pd => pd.DocumentTypeNavigation)
                .WithMany(dt => dt.PersonDocuments)
                .HasForeignKey(pd => pd.DocumentTypeId);

            //PERSON - HEALTHPROFESSIONAL (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.HealthProfessional)
                .WithOne(hp => hp.PersonNavigation)
                .HasForeignKey<HealthProfessional>(hp => hp.HealthProfessionalId);

            //HEALTHPROFESSIONALTYPE - HEALTHPROFESSIONAL (1:N)
            modelBuilder.Entity<HealthProfessional>()
                .HasOne(hpt => hpt.HealthProfessionalTypeNavigation)
                .WithMany(hp => hp.HealthProfessionals)
                .HasForeignKey(hp => hp.HealthProfessionalTypeId);

            //PERSON - USER (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne(u => u.PersonNavigation)
                .HasForeignKey<User>(u => u.PersonId);

            //PERSON - PERSONEDUCATION (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.PersonEducation)
                .WithOne(pe => pe.PersonNavigation)
                .HasForeignKey<PersonEducation>(pe => pe.PersonId);

            //PERSONPROFESSION
            modelBuilder.Entity<PersonProfession>()
                .HasKey(pp => new { pp.PersonId, pp.ProfessionId });

            modelBuilder.Entity<PersonProfession>()
                .HasOne(pp => pp.Person)
                .WithMany(p => p.PersonProfessions)
                .HasForeignKey(pp => pp.PersonId);

            modelBuilder.Entity<PersonProfession>()
                .HasOne(pp => pp.ProfessionNavigation)
                .WithMany(p => p.PersonProfessions)
                .HasForeignKey(pp => pp.ProfessionId);

            //GENDER
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Gender)
                .WithMany(g => g.Persons)
                .HasForeignKey(p => p.GenderId);

            //PERSONMARITALSTATUS
            modelBuilder.Entity<PersonMaritalStatus>()
                .HasOne(p => p.MaritalStatusNavigation)
                .WithMany(m => m.PersonMaritalStatuses)
                .HasForeignKey(p => p.MaritalStatusId);

            modelBuilder.Entity<PersonMaritalStatus>()
                .HasOne(p => p.PersonNavigation)
                .WithOne(p => p.PersonMaritalStatus)
                .HasForeignKey<PersonMaritalStatus>(p => p.PersonId);

            //PERSONMEDICALINSURANCE
            modelBuilder.Entity<PersonMedicalInsurance>()
                .HasOne(p => p.MedicalInsuranceNavigation)
                .WithMany(m => m.PersonMedicalInsurances)
                .HasForeignKey(p => p.MedicalInsuranceId);

            modelBuilder.Entity<PersonMedicalInsurance>()
                .HasOne(p => p.PersonNavigation)
                .WithMany(p => p.PersonMedicalInsurances)
                .HasForeignKey(p => p.PersonId);

            //PERSONAPHONE
            modelBuilder.Entity<PersonPhone>()
                .HasOne(p => p.PersonNavigation)
                .WithOne(p => p.PersonPhone)
                .HasForeignKey<PersonPhone>(p => p.PersonId);

            //PERSONBLOODGROUP
            modelBuilder.Entity<PersonBloodGroup>()
                .HasKey(pb => pb.PersonId);

            modelBuilder.Entity<PersonBloodGroup>()
                .HasOne(p => p.PersonNavigation)
                .WithOne(b => b.PersonBloodGroup)
                .HasForeignKey<PersonBloodGroup>(p => p.PersonId);

            modelBuilder.Entity<PersonBloodGroup>()
                .HasOne(pb => pb.BloodGroupNavigation)
                .WithMany(bg => bg.PersonBloodGroups)
                .HasForeignKey(pb => pb.BloodGroupId);


            //PERSONLABORACTIVITY
            modelBuilder.Entity<PersonLaborActivity>()
    .HasKey(pl => new { pl.PersonId, pl.LaborActivityId }); // Clave compuesta

            modelBuilder.Entity<PersonLaborActivity>()
                .HasOne(pl => pl.PersonNavigation)
                .WithMany(p => p.PersonLaborActivity)
                .HasForeignKey(pl => pl.PersonId);

            modelBuilder.Entity<PersonLaborActivity>()
                .HasOne(pl => pl.LaborActivityNavigation)
                .WithMany(la => la.PersonLaborActivities)
                .HasForeignKey(pl => pl.LaborActivityId);


            //PERSONADDRESS
            modelBuilder.Entity<PersonAddress>()
                .HasKey(pa => pa.PersonId);

            modelBuilder.Entity<PersonAddress>()
                .HasOne(pa => pa.PersonNavigation)
                .WithOne(p => p.PersonAddress)
                .HasForeignKey<PersonAddress>(pa => pa.PersonId);

            //PANTIENT
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PersonId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.PersonNavigation)
                .WithOne(p => p.Patient)
                .HasForeignKey<Patient>(p => p.PersonId);

            //PROGRESS
            modelBuilder.Entity<Progress>()
                .HasKey(p => p.ProgressId);

            modelBuilder.Entity<Progress>()
                .HasOne(p => p.MedicalVisitNavigation)
                .WithMany(mv => mv.Progresses)
                .HasForeignKey(p => p.MedicalVisitId);

            //MEDICALVISIT
            modelBuilder.Entity<MedicalVisit>()
                .HasKey(mv => mv.MedicalVisitId);

            modelBuilder.Entity<MedicalVisit>()
                .HasOne(mv => mv.Patient)
                .WithMany(p => p.MedicalVisits)
                .HasForeignKey(mv => mv.PatientId);

            //EMERGENCYCONTACT
            modelBuilder.Entity<EmergencyContact>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<EmergencyContact>()
                .HasOne(e => e.Patient)
                .WithMany(p => p.EmergencyContacts)
                .HasForeignKey(e => e.PatientId);

            //PANTIENTRELATIONSHIP
            modelBuilder.Entity<PatientRelationship>()
                .HasKey(pr => pr.PatientId);

            modelBuilder.Entity<PatientRelationship>()
                .HasOne(pr => pr.Patient)
                .WithOne(p => p.PatientRelationship)
                .HasForeignKey<PatientRelationship>(pr => pr.PatientId);

            modelBuilder.Entity<PatientRelationship>()
                .HasOne(pr => pr.Relationship)
                .WithMany(r => r.PatientRelationships)
                .HasForeignKey(pr => pr.RelationshipId);

            //TOXICHABIT
            modelBuilder.Entity<ToxicHabit>(entity =>
            {
                entity.HasKey(e => e.ToxicHabitId);
                entity.Property(e => e.Name).HasMaxLength(100).IsUnicode(false);
            });

            modelBuilder.Entity<ToxicHabitBackground>(entity =>
            {
                entity.HasKey(e => e.ToxicHabitBackgroundId);
                entity.Property(e => e.HistoryNumber).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.HasOne(d => d.ToxicHabit)
                    .WithMany(p => p.ToxicHabitBackgrounds)
                    .HasForeignKey(d => d.ToxicHabitId)
                    .HasConstraintName("FK_ToxicHabitBackground_ToxicHabit");

                entity.HasOne(d => d.ClinicalHistory)
                    .WithMany(p => p.ToxicHabitBackgrounds)
                    .HasForeignKey(d => d.ClinicalHistoryId)
                    .HasConstraintName("FK_ToxicHabitBackground_ClinicalHistory");
            });

            modelBuilder.Entity<ClinicalHistory>(entity =>
            {
                entity.HasKey(e => e.ClinicalHistoryId);
                entity.HasIndex(e => e.HistoryNumber).IsUnique();

                entity.Property(e => e.HistoryNumber).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.GeneralObservations).IsUnicode(false);
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithOne(p => p.ClinicalHistory)
                    .HasForeignKey<ClinicalHistory>(d => d.PatientId)
                    .HasPrincipalKey<Patient>(p => p.PersonId)
                    .HasConstraintName("FK_ClinicalHistory_Patient");
            });


            //ALLERGYHISTORY
            modelBuilder.Entity<AllergyHistory>(entity =>
            {
                entity.HasKey(e => e.AllergyHistoryId);

                entity.Property(e => e.HistoryNumber)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                      .HasColumnType("datetime");

                entity.HasOne(d => d.AllergyNavigation)
                      .WithMany(p => p.AllergyHistories)
                      .HasForeignKey(d => d.AllergyId)
                      .HasConstraintName("FK_AllergyHistory_Allergy");

                entity.HasOne(d => d.HistoryNavigation)
                      .WithMany(p => p.AllergyHistories)
                      .HasForeignKey(d => d.ClinicalHistoryId)
                      .HasConstraintName("FK_AllergyHistory_ClinicalHistory");
            });

            //ALLERGY
            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.HasKey(e => e.AllergyId);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100)
                      .IsUnicode(false);
            });

            //FOOD
            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            //FOODINTOLERANCEHISTORY
            modelBuilder.Entity<FoodIntoleranceHistory>(entity =>
            {
                entity.HasKey(e => e.FoodIntoleranceHistoryId);

                entity.Property(e => e.HistoryNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FoodNavigation)
                    .WithMany(p => p.FoodIntoleranceHistories)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK_FoodIntoleranceHistory_Food");

                entity.HasOne(d => d.HistoryNavigation)
                    .WithMany(p => p.FoodIntoleranceHistories)
                    .HasForeignKey(d => d.ClinicalHistoryId)
                    .HasConstraintName("FK_FoodIntoleranceHistory_ClinicalHistory");
            });

            //SURGERY
            modelBuilder.Entity<Surgery>(entity =>
            {
                entity.HasKey(e => e.SurgeryId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            //SURGERYHISTORY
            modelBuilder.Entity<SurgeryHistory>(entity =>
            {
                entity.HasKey(e => e.SurgeryHistoryId);

                entity.Property(e => e.HistoryNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime");

                // Configuración añadida para SurgeryDate
                entity.Property(e => e.SurgeryDate)
                    .HasColumnType("datetime");  // Mismo tipo que RegistrationDate

                entity.HasOne(d => d.SurgeryNavigation)
                    .WithMany(p => p.SurgeryHistories)
                    .HasForeignKey(d => d.SurgeryId)
                    .HasConstraintName("FK_SurgeryHistory_Surgery");

                entity.HasOne(d => d.HistoryNavigation)
                    .WithMany(p => p.SurgeryHistories)
                    .HasForeignKey(d => d.ClinicalHistoryId)
                    .HasConstraintName("FK_SurgeryHistory_ClinicalHistory");
            });

            //OBSTETRICHISTORY
            modelBuilder.Entity<ObstetricHistory>(entity =>
            {
                entity.HasKey(e => e.ObstetricHistoryId);

                entity.Property(e => e.HistoryNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.HistoryNavigation)
                    .WithMany(p => p.ObstetricHistories)
                    .HasForeignKey(d => d.ClinicalHistoryId)
                    .HasConstraintName("FK_ObstetricHistory_ClinicalHistory");
            });

            //DISEASE
            modelBuilder.Entity<Disease>(entity =>
            {
                entity.HasKey(e => e.DiseaseId);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.DiseaseTypeNavigation)
                    .WithMany(p => p.Diseases)
                    .HasForeignKey(d => d.DiseaseTypeId)
                    .HasConstraintName("FK_Disease_DiseaseType");
            });


            //PersonalHistory
            modelBuilder.Entity<PersonalHistory>(entity =>
            {
                entity.HasOne(ph => ph.MedicalRecordNavigation)
                    .WithMany(mr => mr.PersonalHistories)
                    .HasForeignKey(ph => ph.ClinicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //FamilyHistoryDetail
            modelBuilder.Entity<FamilyHistoryDetail>(entity =>
            {
                entity.HasOne(f => f.MedicalRecordNavigation)
                    .WithMany(mr => mr.FamilyHistoryDetails)
                    .HasForeignKey(f => f.ClinicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.DiseaseNavigation)
                    .WithMany(d => d.FamilyHistoryDetails)
                    .HasForeignKey(f => f.DiseaseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            //GynecologicalHistory
            modelBuilder.Entity<GynecologicalHistory>(entity =>
            {
                entity.HasOne(gh => gh.MedicalRecordNavigation)
                    .WithMany(mr => mr.GynecologicalHistories)
                    .HasForeignKey(gh => gh.ClinicalHistoryId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<HabitHistory>(entity =>
            {
                entity.HasOne(d => d.Habit)
                    .WithMany(p => p.HabitHistories)
                    .HasForeignKey(d => d.HabitId)
                    .HasConstraintName("FK_HabitHistory_Habits");


                entity.HasOne(d => d.ClinicalHistory)
                    .WithMany(p => p.HabitHistories)
                    .HasForeignKey(d => d.ClinicalHistoryId)
                    .HasConstraintName("FK_HabitHistory_ClinicalHistory");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}