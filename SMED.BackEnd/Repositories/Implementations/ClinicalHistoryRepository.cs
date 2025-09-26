using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ClinicalHistoryRepository : IClinicalHistoryRepository
    {
        private readonly SGISContext _context;

        public ClinicalHistoryRepository(SGISContext context)
        {
            _context = context;
        }


        public async Task<bool> PatientHasClinicalHistoryAsync(int personId)
        {
            // Verificar si existe un Patient con ese PersonId
            var patientExists = await _context.Patients
                .AnyAsync(p => p.PersonId == personId);

            if (!patientExists)
                return false;

            // Verificar si ya existe una historia clínica para ese PersonId
            // PatientId en ClinicalHistory apunta directamente a Patient.PersonId
            return await _context.ClinicalHistories
                .AnyAsync(ch => ch.PatientId == personId && ch.IsActive == true);
        }


        public async Task<ClinicalHistoryDTO?> GetByPatientIdAsync(int personId)
        {
            var history = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(p => p.PersonDocument)
                .Include(ch => ch.PersonalHistories)
                    .ThenInclude(ph => ph.DiseaseNavigation)
                        .ThenInclude(d => d.DiseaseTypeNavigation)
                .Include(ch => ch.SurgeryHistories)
                    .ThenInclude(sh => sh.SurgeryNavigation)
                .Include(ch => ch.AllergyHistories)
                    .ThenInclude(ah => ah.AllergyNavigation)
                .Include(ch => ch.HabitHistories)
                    .ThenInclude(hh => hh.Habit)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.RelationshipNavigation)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.DiseaseNavigation)
                        .ThenInclude(t => t.DiseaseTypeNavigation)
                .Include(ch => ch.ObstetricHistories)
                .Include(ch => ch.GynecologicalHistories)
                .Include(ch => ch.SportsActivitiesHistories)
                    .ThenInclude(sp => sp.SportActivityNavigation)
                .Include(ch => ch.LifeStyleHistories)
                    .ThenInclude(lsh => lsh.LifeStyleNavigation)
                .Include(ch => ch.DietaryHabitHistories)
                .Include(ch => ch.SleepHabitHistories)
                    .ThenInclude(sh => sh.SleepHabitNavigation)
                .Include(ch => ch.FoodConsumptionHistories)
                    .ThenInclude(fd => fd.FoodNavigation)
                .Include(ch => ch.WaterConsumptionHistories)
                .Include(ch => ch.MedicationHistories)
                .ThenInclude(mh => mh.Medicine)
                .Include(ch => ch.PsychopsychiatricHistories)
                .Include(ch => ch.CurrentProblemHistories)
                .Include(ch => ch.WorkHistories)
                .Include(ch => ch.PsychosexualHistories)
                .Include(ch => ch.PrenatalHistories)
                .Include(ch => ch.PostnatalHistories)
                .Include(ch => ch.PerinatalHistories)
                .Include(ch => ch.NeuropsychologicalHistories)
                .Include(ch => ch.NeurologicalExams)
                    .ThenInclude(ne => ne.NeurologicalExamType)
                .Include(ch => ch.DevelopmentRecords)
                .FirstOrDefaultAsync(ch => ch.PatientId == personId && ch.IsActive == true);

            return history == null ? null : MapToDTO(history);
        }


        public async Task<ClinicalHistoryDTO> AddAsync(ClinicalHistoryCreateDTO createDto)
        {
            if (createDto.Patient == null || createDto.Patient.PersonId == 0)
                throw new Exception("Patient information is required.");

            // Verificar que existe el paciente
            var patientExists = await _context.Patients
                .AnyAsync(p => p.PersonId == createDto.Patient.PersonId);

            if (!patientExists)
                throw new Exception("Patient not found.");

            // VALIDACIÓN CORREGIDA: Verificar si ya tiene historia clínica
            var hasExistingHistory = await _context.ClinicalHistories
                .AnyAsync(ch => ch.PatientId == createDto.Patient.PersonId && ch.IsActive == true);

            if (hasExistingHistory)
                throw new InvalidOperationException("Este paciente ya tiene una historia clínica activa. No se puede crear otra.");

            // Crear la historia clínica
            var clinicalHistory = new ClinicalHistory
            {
                HistoryNumber = createDto.HistoryNumber,
                CreationDate = createDto.CreationDate ?? DateTime.Now,
                IsActive = createDto.IsActive ?? true,
                GeneralObservations = createDto.GeneralObservations,
                PatientId = createDto.Patient.PersonId  // PatientId apunta directamente a PersonId
            };

            _context.ClinicalHistories.Add(clinicalHistory);
            await _context.SaveChangesAsync();

            return new ClinicalHistoryDTO
            {
                ClinicalHistoryId = clinicalHistory.ClinicalHistoryId,
                HistoryNumber = clinicalHistory.HistoryNumber,
                CreationDate = clinicalHistory.CreationDate,
                IsActive = clinicalHistory.IsActive,
                GeneralObservations = clinicalHistory.GeneralObservations,
                Patient = new PatientDTO { PersonId = createDto.Patient.PersonId }
            };
        }


        public async Task<ClinicalHistoryDTO> AddAsync(ClinicalHistoryDTO dto)
        {
            // Verificar que existe el paciente
            var patientExists = await _context.Patients
                .AnyAsync(p => p.PersonId == dto.Patient.PersonId);

            if (!patientExists)
                throw new Exception("Patient not found.");

            // VALIDACIÓN: Verificar si ya tiene historia clínica
            var hasExistingHistory = await _context.ClinicalHistories
                .AnyAsync(ch => ch.PatientId == dto.Patient.PersonId && ch.IsActive == true);

            if (hasExistingHistory)
                throw new InvalidOperationException("Este paciente ya tiene una historia clínica activa. No se puede crear otra.");

            var clinicalHistory = new ClinicalHistory
            {
                HistoryNumber = dto.HistoryNumber,
                CreationDate = dto.CreationDate ?? DateTime.Now,
                IsActive = dto.IsActive ?? true,
                GeneralObservations = dto.GeneralObservations,
                PatientId = dto.Patient.PersonId
            };

            _context.ClinicalHistories.Add(clinicalHistory);
            await _context.SaveChangesAsync();

            return new ClinicalHistoryDTO
            {
                ClinicalHistoryId = clinicalHistory.ClinicalHistoryId,
                HistoryNumber = clinicalHistory.HistoryNumber,
                CreationDate = clinicalHistory.CreationDate,
                IsActive = clinicalHistory.IsActive,
                GeneralObservations = clinicalHistory.GeneralObservations,
                Patient = new PatientDTO { PersonId = clinicalHistory.PatientId ?? 0 }
            };
        }

        public async Task<List<ClinicalHistoryDTO>> GetAllAsync()
        {
            var histories = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(p => p.PersonDocument)
                .Include(ch => ch.PersonalHistories)
                    .ThenInclude(ph => ph.DiseaseNavigation)
                        .ThenInclude(d => d.DiseaseTypeNavigation)
                .Include(ch => ch.SurgeryHistories)
                    .ThenInclude(sh => sh.SurgeryNavigation)
                .Include(ch => ch.AllergyHistories)
                    .ThenInclude(ah => ah.AllergyNavigation)
                .Include(ch => ch.HabitHistories)
                    .ThenInclude(hh => hh.Habit)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.RelationshipNavigation)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.DiseaseNavigation)
                        .ThenInclude(t => t.DiseaseTypeNavigation)
                .Include(ch => ch.ObstetricHistories)
                .Include(ch => ch.GynecologicalHistories)
                .Include(ch => ch.SportsActivitiesHistories)
                    .ThenInclude(sp => sp.SportActivityNavigation)
                .Include(ch => ch.LifeStyleHistories)
                    .ThenInclude(lsh => lsh.LifeStyleNavigation)
                .Include(ch => ch.DietaryHabitHistories)
                .Include(ch => ch.SleepHabitHistories)
                    .ThenInclude(sh => sh.SleepHabitNavigation)
                .Include(ch => ch.FoodConsumptionHistories)
                    .ThenInclude(fd => fd.FoodNavigation)
                .Include(ch => ch.WaterConsumptionHistories)
                .Include(ch => ch.MedicationHistories)
                    .ThenInclude(mh => mh.Medicine)
                .Include(ch => ch.PsychopsychiatricHistories)
                .Include(ch => ch.CurrentProblemHistories)
                .Include(ch => ch.WorkHistories)
                .Include(ch => ch.PsychosexualHistories)
                .Include(ch => ch.PrenatalHistories)
                .Include(ch => ch.PostnatalHistories)
                .Include(ch => ch.PerinatalHistories)
                .Include(ch => ch.NeuropsychologicalHistories)
                .Include(ch => ch.NeurologicalExams)
                    .ThenInclude(ne => ne.NeurologicalExamType)
                .Include(ch => ch.DevelopmentRecords)
                .ToListAsync();

            return histories.Select(MapToDTO).ToList();
        }

        public async Task<ClinicalHistoryDTO?> GetByIdAsync(int id)
        {
            var history = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(p => p.PersonDocument)
                .Include(ch => ch.PersonalHistories)
                    .ThenInclude(ph => ph.DiseaseNavigation)
                        .ThenInclude(d => d.DiseaseTypeNavigation)
                .Include(ch => ch.SurgeryHistories)
                    .ThenInclude(sh => sh.SurgeryNavigation)
                .Include(ch => ch.AllergyHistories)
                    .ThenInclude(ah => ah.AllergyNavigation)
                .Include(ch => ch.HabitHistories)
                    .ThenInclude(hh => hh.Habit)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.RelationshipNavigation)
                .Include(ch => ch.FamilyHistoryDetails)
                    .ThenInclude(fh => fh.DiseaseNavigation)
                        .ThenInclude(t => t.DiseaseTypeNavigation)
                .Include(ch => ch.ObstetricHistories)
                .Include(ch => ch.GynecologicalHistories)
                .Include(ch => ch.SportsActivitiesHistories)
                    .ThenInclude(sp => sp.SportActivityNavigation)
                .Include(ch => ch.LifeStyleHistories)
                    .ThenInclude(lsh => lsh.LifeStyleNavigation)
                .Include(ch => ch.DietaryHabitHistories)
                .Include(ch => ch.SleepHabitHistories)
                    .ThenInclude(sh => sh.SleepHabitNavigation)
                .Include(ch => ch.FoodConsumptionHistories)
                    .ThenInclude(fd => fd.FoodNavigation)
                .Include(ch => ch.WaterConsumptionHistories)
                .Include(ch => ch.MedicationHistories)
                    .ThenInclude(mh => mh.Medicine)
                .Include(ch => ch.PsychopsychiatricHistories)
                .Include(ch => ch.CurrentProblemHistories)
                .Include(ch => ch.WorkHistories)
                .Include(ch => ch.PsychosexualHistories)
                .Include(ch => ch.PrenatalHistories)
                .Include(ch => ch.PostnatalHistories)
                .Include(ch => ch.PerinatalHistories)
                .Include(ch => ch.NeuropsychologicalHistories)
                .Include(ch => ch.NeurologicalExams)
                    .ThenInclude(ne => ne.NeurologicalExamType)
                .Include(ch => ch.DevelopmentRecords)
                .FirstOrDefaultAsync(ch => ch.ClinicalHistoryId == id);

            return history == null ? null : MapToDTO(history);
        }

        public async Task<ClinicalHistoryDTO?> UpdateAsync(BasicClinicalHistroyDTO dto)
        {
            var entity = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .FirstOrDefaultAsync(ch => ch.ClinicalHistoryId == dto.ClinicalHistoryId);

            if (entity == null)
                return null;

            // Validar duplicado
            bool duplicateExists = await _context.ClinicalHistories
                .AnyAsync(ch => ch.HistoryNumber == dto.HistoryNumber && ch.ClinicalHistoryId != dto.ClinicalHistoryId);

            if (duplicateExists)
                throw new InvalidOperationException("HistoryNumber already exists for another record.");

            // Actualizar campos
            entity.HistoryNumber = dto.HistoryNumber;
            entity.CreationDate = dto.CreationDate;
            entity.IsActive = dto.IsActive;
            entity.GeneralObservations = dto.GeneralObservations;
            entity.PatientId = dto.Patient.PersonId;

            await _context.SaveChangesAsync();

            return MapToDTO(entity);
        }

        // Método original
        public async Task<ClinicalHistoryDTO?> UpdateAsync(ClinicalHistoryDTO dto)
        {
            return await UpdateAsync(new BasicClinicalHistroyDTO
            {
                ClinicalHistoryId = dto.ClinicalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                CreationDate = dto.CreationDate,
                IsActive = dto.IsActive,
                GeneralObservations = dto.GeneralObservations,
                Patient = new PatientDTO
                {
                    PersonId = dto.Patient.PersonId
                }
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ClinicalHistories.FindAsync(id);
            if (entity == null)
                return false;

            _context.ClinicalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ClinicalHistoryDTO>> SearchAsync(string searchTerm, bool searchByIdNumber = false)
        {
            var query = _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(p => p.PersonDocument)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (searchByIdNumber)
                {
                    // Búsqueda por número de documento (cédula)
                    query = query.Where(ch =>
                        ch.Patient != null &&
                        ch.Patient.PersonNavigation != null &&
                        ch.Patient.PersonNavigation.PersonDocument != null &&
                        ch.Patient.PersonNavigation.PersonDocument.DocumentNumber.Contains(searchTerm));
                }
                else
                {
                    // Búsqueda por número de historia clínica
                    query = query.Where(ch => ch.HistoryNumber.Contains(searchTerm));
                }
            }

            return query.Select(MapToDTO).ToList();
        }

        private ClinicalHistoryDTO MapToDTO(ClinicalHistory entity)
        {
            return new ClinicalHistoryDTO
            {
                ClinicalHistoryId = entity.ClinicalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                CreationDate = entity.CreationDate,
                IsActive = entity.IsActive,
                GeneralObservations = entity.GeneralObservations,
                Patient = new PatientDTO
                {
                    PersonId = entity.Patient.PersonId,
                    Person = entity.Patient.PersonNavigation != null ? new PersonDTO
                    {
                        Id = entity.Patient.PersonNavigation.Id,
                        FirstName = entity.Patient.PersonNavigation.FirstName,
                        MiddleName = entity.Patient.PersonNavigation.MiddleName,
                        LastName = entity.Patient.PersonNavigation.LastName,
                        SecondLastName = entity.Patient.PersonNavigation.SecondLastName,
                        BirthDate = entity.Patient.PersonNavigation.BirthDate,
                        Email = entity.Patient.PersonNavigation.Email,
                        Document = entity.Patient.PersonNavigation.PersonDocument != null ? new PersonDocumentDTO
                        {
                            DocumentNumber = entity.Patient.PersonNavigation.PersonDocument.DocumentNumber,
                            DocumentTypeId = entity.Patient.PersonNavigation.PersonDocument.DocumentTypeId
                        } : null,
                    } : null
                },
                PatientFullName = entity.Patient?.PersonNavigation != null
                    ? $"{entity.Patient.PersonNavigation.FirstName} {entity.Patient.PersonNavigation.MiddleName} {entity.Patient.PersonNavigation.LastName} {entity.Patient.PersonNavigation.SecondLastName}"
                    : string.Empty,
                DocumentNumber = entity.Patient?.PersonNavigation?.PersonDocument?.DocumentNumber,
                PersonalHistories = entity.PersonalHistories.Select(ph => new PersonalHistoryDTO
                {
                    MedicalRecordNumber = ph.MedicalRecordNumber,
                    Description = ph.Description,
                    RegistrationDate = ph.RegistrationDate,
                    DiseaseTypeName = ph.DiseaseNavigation.DiseaseTypeNavigation.Name,
                    DiseaseName = ph.DiseaseNavigation.Name,
                }).ToList(),
                SurgeryHistories = entity.SurgeryHistories.Select(sh => new SurgeryHistoryDTO
                {
                    HistoryNumber = sh.HistoryNumber,
                    Description = sh.Description,
                    RegistrationDate = sh.RegistrationDate,
                    SurgeryName = sh.SurgeryNavigation.Name,
                    SurgeryDate = sh.SurgeryDate
                }).ToList(),
                AllergyHistories = entity.AllergyHistories.Select(ah => new AllergyHistoryDTO
                {
                    AllergyName = ah.AllergyNavigation.Name,
                    RegistrationDate = ah.RegistrationDate
                }).ToList(),
                HabitHistories = entity.HabitHistories.Select(hh => new HabitHistoryDTO
                {
                    HabitName = hh.Habit.Name,
                    RecordDate = hh.RecordDate
                }).ToList(),
                FamilyHistories = entity.FamilyHistoryDetails.Select(fh => new FamilyHistoryDetailDTO
                {
                    RelationshipName = fh.RelationshipNavigation?.Name ?? "N/D",
                    DiseaseName = fh.DiseaseNavigation?.Name ?? "N/D",
                    DiseaseTypeName = fh.DiseaseNavigation?.DiseaseTypeNavigation?.Name ?? "N/D",
                    appearanceAge = fh.appearanceAge,
                    Description = fh.Description,
                    RegistrationDate = fh.RegistrationDate
                }).ToList(),
                ObstetricHistory = entity.ObstetricHistories.FirstOrDefault() != null ? new ObstetricHistoryDTO
                {
                    ObstetricHistoryId = entity.ObstetricHistories.First().ObstetricHistoryId,
                    HistoryNumber = entity.ObstetricHistories.First().HistoryNumber,
                    ClinicalHistoryId = entity.ObstetricHistories.First().ClinicalHistoryId,
                    CurrentPregnancy = entity.ObstetricHistories.First().CurrentPregnancy,
                    PreviousPregnancies = entity.ObstetricHistories.First().PreviousPregnancies,
                    Deliveries = entity.ObstetricHistories.First().Deliveries,
                    Abortions = entity.ObstetricHistories.First().Abortions,
                    CSections = entity.ObstetricHistories.First().CSections,
                    LiveBirths = entity.ObstetricHistories.First().LiveBirths,
                    Stillbirths = entity.ObstetricHistories.First().Stillbirths,
                    LivingChildren = entity.ObstetricHistories.First().LivingChildren,
                    Breastfeeding = entity.ObstetricHistories.First().Breastfeeding,
                    GestionalAge = entity.ObstetricHistories.First().GestionalAge,
                    ExpectedDeliveryDate = entity.ObstetricHistories.First().ExpectedDeliveryDate
                } : null,
                GynecologicalHistory = entity.GynecologicalHistories.FirstOrDefault() != null ? new GynecologicalHistoryDTO
                {
                    GynecologicalHistoryId = entity.GynecologicalHistories.First().GynecologicalHistoryId,
                    MedicalRecordNumber = entity.GynecologicalHistories.First().MedicalRecordNumber,
                    GynecologicalDevelopment = entity.GynecologicalHistories.First().GynecologicalDevelopment,
                    Menarche = entity.GynecologicalHistories.First().Menarche,
                    Pubarche = entity.GynecologicalHistories.First().Pubarche,
                    MenstrualCycles = entity.GynecologicalHistories.First().MenstrualCycles,
                    LastMenstruation = entity.GynecologicalHistories.First().LastMenstruation,
                    ContraceptiveMethods = entity.GynecologicalHistories.First().ContraceptiveMethods,
                    ClinicalHistoryId = entity.GynecologicalHistories.First().ClinicalHistoryId
                } : null,
                SportsActivitiesHistory = entity.SportsActivitiesHistories.FirstOrDefault() != null ? new SportsActivitiesHistoryDTO
                {
                    SportActivityHistoryId = entity.SportsActivitiesHistories.First().SportActivityHistoryId,
                    HistoryNumber = entity.SportsActivitiesHistories.First().HistoryNumber,
                    ClinicalHistoryId = entity.SportsActivitiesHistories.First().ClinicalHistoryId,
                    MinutesPerDay = entity.SportsActivitiesHistories.First().MinutesPerDay,
                    NumberOfDays = entity.SportsActivitiesHistories.First().NumberOfDays,
                    Description = entity.SportsActivitiesHistories.First().Description,
                    RegistrationDate = entity.SportsActivitiesHistories.First().RegistrationDate,
                    SportActivityId = entity.SportsActivitiesHistories.First().SportActivityId,
                    SportActivityName = entity.SportsActivitiesHistories.First().SportActivityNavigation?.Name ?? string.Empty
                } : null,
                LifeStyleHistory = entity.LifeStyleHistories.FirstOrDefault() != null ? new LifeStyleHistoryDTO
                {
                    LifeStyleHistoryId = entity.LifeStyleHistories.First().LifeStyleHistoryId,
                    ClinicalHistoryId = entity.LifeStyleHistories.First().ClinicalHistoryId,
                    LifeStyleName = entity.LifeStyleHistories.First().LifeStyleNavigation?.Name ?? string.Empty,
                    Description = entity.LifeStyleHistories.First().Description,
                    RegistrationDate = entity.LifeStyleHistories.First().RegistrationDate
                } : null,
                DietaryHabitsHistory = entity.DietaryHabitHistories.FirstOrDefault() != null ? new DietaryHabitsHistoryDTO
                {
                    DietaryHabitHistoryId = entity.DietaryHabitHistories.First().DietaryHabitHistoryId,
                    ClinicalHistoryId = entity.DietaryHabitHistories.First().ClinicalHistoryId,
                    Description = entity.DietaryHabitHistories.First().Description,
                    RegistrationDate = entity.DietaryHabitHistories.First().RegistrationDate
                } : null,
                SleepHabitHistory = entity.SleepHabitHistories.FirstOrDefault() != null ? new SleepHabitHistoryDTO
                {
                    HabitSleepHistoryId = entity.SleepHabitHistories.First().HabitSleepHistoryId,
                    ClinicalHistoryId = entity.SleepHabitHistories.First().ClinicalHistoryId,
                    SleepHabitName = entity.SleepHabitHistories.First().SleepHabitNavigation?.Name ?? string.Empty,
                    Description = entity.SleepHabitHistories.First().Description,
                    RecordDate = entity.SleepHabitHistories.First().RecordDate
                } : null,
                FoodConsumptionHistory = entity.FoodConsumptionHistories.FirstOrDefault() != null ? new FoodConsumptionHistoryDTO
                {
                    FoodConsumptionHistoryId = entity.FoodConsumptionHistories.First().FoodConsumptionHistoryId,
                    ClinicalHistoryId = entity.FoodConsumptionHistories.First().ClinicalHistoryId,
                    FoodName = entity.FoodConsumptionHistories.First().FoodNavigation?.Name ?? string.Empty,
                    Hour = entity.FoodConsumptionHistories.First().Hour,
                    Place = entity.FoodConsumptionHistories.First().Place,
                    Amount = entity.FoodConsumptionHistories.First().Amount,
                    Description = entity.FoodConsumptionHistories.First().Description,
                    RegistrationDate = entity.FoodConsumptionHistories.First().RegistrationDate
                } : null,
                WaterConsumptionHistory = entity.WaterConsumptionHistories.FirstOrDefault() != null ? new WaterConsumptionHistoryDTO
                {
                    WaterConsumptionHistoryId = entity.WaterConsumptionHistories.First().WaterConsumptionHistoryId,
                    ClinicalHistoryId = entity.WaterConsumptionHistories.First().ClinicalHistoryId,
                    Amount = entity.WaterConsumptionHistories.First().Amount,
                    Frequency = entity.WaterConsumptionHistories.First().Frequency,
                    Description = entity.WaterConsumptionHistories.First().Description,
                    RegistrationDate = entity.WaterConsumptionHistories.First().RegistrationDate
                } : null,
                MedicationHistories = entity.MedicationHistories.Select(mh => new MedicationHistoryDTO
                {
                    MedicationHistoryId = mh.MedicationHistoryId,
                    HistoryNumber = mh.HistoryNumber,
                    MedicineId = mh.MedicineId,
                    ClinicalHistoryId = mh.ClinicalHistoryId,
                    ConsumptionDate = mh.ConsumptionDate,
                    MedicineName = mh.Medicine?.Name,
                    MedicineWeight = mh.Medicine?.Weight
                }).ToList(),

                PsychopsychiatricHistories = entity.PsychopsychiatricHistories.Select(pp => new PsychopsychiatricHistoryDTO
                {
                    PsychopsychiatricHistoryId = pp.PsychopsychiatricHistoryId,
                    HistoryNumber = pp.HistoryNumber,
                    ClinicalHistoryId = pp.ClinicalHistoryId,
                    Type = pp.Type,
                    Actor = pp.Actor,
                    HistoryDate = pp.HistoryDate,
                    HistoryState = pp.HistoryState
                }).ToList(),

                CurrentProblemHistories = entity.CurrentProblemHistories.Select(cp => new CurrentProblemHistoryDTO
                {
                    CurrentProblemHistoryId = cp.CurrentProblemHistoryId,
                    HistoryNumber = cp.HistoryNumber,
                    ClinicalHistoryId = cp.ClinicalHistoryId,
                    AppearanceEvolution = cp.AppearanceEvolution,
                    TriggeringFactors = cp.TriggeringFactors,
                    FrequencyIntensitySymptoms = cp.FrequencyIntensitySymptoms,
                    Impact = cp.Impact
                }).ToList(),

                WorkHistories = entity.WorkHistories.Select(w => new WorkHistoryDTO
                {
                    WorkHistoryId = w.WorkHistoryId,
                    HistoryNumber = w.HistoryNumber,
                    ClinicalHistoryId = w.ClinicalHistoryId,
                    Experience = w.Experience,
                    Stability = w.Stability,
                    SatisfactionLevel = w.SatisfactionLevel
                }).ToList(),

                PsychosexualHistories = entity.PsychosexualHistories.Select(ps => new PsychosexualHistoryDTO
                {
                    PsychosexualHistoryId = ps.PsychosexualHistoryId,
                    HistoryNumber = ps.HistoryNumber,
                    ClinicalHistoryId = ps.ClinicalHistoryId,
                    Description = ps.Description
                }).ToList(),

                // Agregar esto en el método MapToDTO del ClinicalHistoryRepository, después de las colecciones existentes:

                PrenatalHistories = entity.PrenatalHistories.Select(ph => new PrenatalHistoryDTO
                {
                    PrenatalHistoryId = ph.PrenatalHistoryId,
                    HistoryNumber = ph.HistoryNumber,
                    ClinicalHistoryId = ph.ClinicalHistoryId,
                    Description = ph.Description
                }).ToList(),

                PostnatalHistories = entity.PostnatalHistories.Select(ph => new PostnatalHistoryDTO
                {
                    PostNatalId = ph.PostNatalId,
                    HistoryNumber = ph.HistoryNumber,
                    ClinicalHistoryId = ph.ClinicalHistoryId,
                    Description = ph.Description
                }).ToList(),

                PerinatalHistories = entity.PerinatalHistories.Select(ph => new PerinatalHistoryDTO
                {
                    PerinatalHistoryId = ph.PerinatalHistoryId,
                    HistoryNumber = ph.HistoryNumber,
                    ClinicalHistoryId = ph.ClinicalHistoryId,
                    Description = ph.Description
                }).ToList(),

                NeuropsychologicalHistories = entity.NeuropsychologicalHistories.Select(nh => new NeuropsychologicalHistoryDTO
                {
                    NeuropsychologicalHistoryId = nh.NeuropsychologicalHistoryId,
                    HistoryNumber = nh.HistoryNumber,
                    ClinicalHistoryId = nh.ClinicalHistoryId,
                    Description = nh.Description
                }).ToList(),

                NeurologicalExams = entity.NeurologicalExams.Select(ne => new NeurologicalExamDTO
                {
                    NeurologicalExamId = ne.NeurologicalExamId,
                    HistoryNumber = ne.HistoryNumber,
                    ClinicalHistoryId = ne.ClinicalHistoryId,
                    Name = ne.Name,
                    LinkPdf = ne.LinkPdf,
                    ExamDate = ne.ExamDate,
                    Description = ne.Description,
                    NeurologicalExamTypeId = ne.NeurologicalExamTypeId,
                    NeurologicalExamTypeName = ne.NeurologicalExamType?.Name
                }).ToList(),

                DevelopmentRecords = entity.DevelopmentRecords.Select(dr => new DevelopmentRecordDTO
                {
                    DevelopmentRecordId = dr.DevelopmentRecordId,
                    HistoryNumber = dr.HistoryNumber,
                    ClinicalHistoryId = dr.ClinicalHistoryId,
                    DevelopmentMilestone = dr.DevelopmentMilestone,
                    AgeRange = dr.AgeRange,
                    Observations = dr.Observations
                }).ToList()
            };
        }

        private ClinicalHistory MapToEntity(ClinicalHistoryDTO dto)
        {
            return new ClinicalHistory
            {
                HistoryNumber = dto.HistoryNumber,
                CreationDate = dto.CreationDate ?? DateTime.Now,
                IsActive = dto.IsActive ?? true,
                GeneralObservations = dto.GeneralObservations,
                PatientId = dto.Patient.PersonId
            };
        }
    }
}