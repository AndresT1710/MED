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

                .FirstOrDefaultAsync(ch => ch.ClinicalHistoryId == id);

            return history == null ? null : MapToDTO(history);
        }

        public async Task<ClinicalHistoryDTO> AddAsync(ClinicalHistoryDTO dto)
        {
            if (dto.Patient == null || dto.Patient.PersonId == 0)
                throw new Exception("Patient information is required.");

            // Validar que exista el paciente con ese PersonId
            var patientExists = await _context.Patients.AnyAsync(p => p.PersonId == dto.Patient.PersonId);
            if (!patientExists)
                throw new Exception("Patient not found.");

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

            // Mapear entidad guardada a DTO para devolver
            var createdDto = new ClinicalHistoryDTO
            {
                ClinicalHistoryId = clinicalHistory.ClinicalHistoryId,
                HistoryNumber = clinicalHistory.HistoryNumber,
                CreationDate = clinicalHistory.CreationDate,
                IsActive = clinicalHistory.IsActive,
                GeneralObservations = clinicalHistory.GeneralObservations,
                Patient = new PatientDTO
                {
                    PersonId = clinicalHistory.PatientId ?? 0
                    // Si quieres, puedes mapear más propiedades
                }
            };

            return createdDto;
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
                        // Agrega otras propiedades según necesites
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
                } :null,
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
                }:null,
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
                } : null


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
