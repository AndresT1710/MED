using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalCareRepository : IRepository<MedicalCareDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalCareRepository(SGISContext context) => _context = context;

        public async Task<List<MedicalCareDTO>> GetAllAsync()
        {
            var medicalCares = await _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetNursingCareAsync()
        {
            var medicalCares = await _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.LocationNavigation != null &&
                            (m.LocationNavigation.Name.ToLower() == "enfermería" ||
                             m.LocationNavigation.Name.ToLower() == "enfermeria"))
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByAreaAndDateAsync(string area, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.LocationNavigation != null &&
                           m.LocationNavigation.Name.ToLower() == area.ToLower());

            if (date.HasValue)
            {
                query = query.Where(m => m.CareDate.Date == date.Value.Date);
            }

            var medicalCares = await query
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByPlaceOfAttentionAndDateAsync(int? placeOfAttentionId = null, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.LocationNavigation != null &&
                           (m.LocationNavigation.Name.ToLower() == "enfermería" ||
                            m.LocationNavigation.Name.ToLower() == "enfermeria"));

            if (placeOfAttentionId.HasValue)
            {
                query = query.Where(m => m.PlaceOfAttentionId == placeOfAttentionId.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(m => m.CareDate.Date == date.Value.Date);
            }

            var medicalCares = await query
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<MedicalCareDTO?> GetByIdAsync(int id)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Buscando MedicalCare ID: {id}");

                // 1) Obtener la información base del MedicalCare
                var result = await _context.MedicalCares
                    .Where(m => m.CareId == id)
                    .Select(m => new
                    {
                        m.CareId,
                        m.LocationId,
                        m.PlaceOfAttentionId,
                        m.PatientId,
                        m.HealthProfessionalId,
                        m.CareDate,
                        // Cargar nombres
                        LocationName = _context.Locations
                            .Where(l => l.Id == m.LocationId)
                            .Select(l => l.Name)
                            .FirstOrDefault(),
                        PlaceOfAttentionName = _context.PlaceOfAttentions
                            .Where(p => p.Id == m.PlaceOfAttentionId)
                            .Select(p => p.Name)
                            .FirstOrDefault(),
                        PatientName = _context.Patients
                            .Where(p => p.PersonId == m.PatientId)
                            .Join(_context.Persons,
                                patient => patient.PersonId,
                                person => person.Id,
                                (patient, person) => new
                                {
                                    person.FirstName,
                                    person.MiddleName,
                                    person.LastName,
                                    person.SecondLastName
                                })
                            .Select(p => (p.FirstName ?? "") + " " +
                                         (p.MiddleName ?? "") + " " +
                                         (p.LastName ?? "") + " " +
                                         (p.SecondLastName ?? ""))
                            .FirstOrDefault(),
                        ProfessionalName = _context.HealthProfessionals
                            .Where(hp => hp.HealthProfessionalId == m.HealthProfessionalId)
                            .Join(_context.Persons,
                                hp => hp.HealthProfessionalId,
                                person => person.Id,
                                (hp, person) => new
                                {
                                    person.FirstName,
                                    person.MiddleName,
                                    person.LastName,
                                    person.SecondLastName
                                })
                            .Select(p => (p.FirstName ?? "") + " " +
                                         (p.MiddleName ?? "") + " " +
                                         (p.LastName ?? "") + " " +
                                         (p.SecondLastName ?? ""))
                            .FirstOrDefault()
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    Console.WriteLine($"[DEBUG] No se encontró MedicalCare con ID: {id}");
                    return null;
                }

                // 2) Mapear las colecciones de fisioterapia manualmente
                var currentIllnesses = await _context.CurrentIllnesses
                    .Where(ci => ci.MedicalCareId == id)
                    .Select(ci => new CurrentIllnessDTO
                    {
                        CurrentIllnessId = ci.CurrentIllnessId,
                        EvolutionTime = ci.EvolutionTime,
                        Localization = ci.Localization,
                        Intensity = ci.Intensity,
                        AggravatingFactors = ci.AggravatingFactors,
                        MitigatingFactors = ci.MitigatingFactors,
                        NocturnalPain = ci.NocturnalPain,
                        Weakness = ci.Weakness,
                        Paresthesias = ci.Paresthesias,
                        ComplementaryExams = ci.ComplementaryExams,
                        MedicalCareId = ci.MedicalCareId
                    })
                    .ToListAsync();

                var physiotherapyDiagnostics = await _context.PhysiotherapyDiagnostics
                    .Where(pd => pd.MedicalCareId == id)
                    .Select(pd => new PhysiotherapyDiagnosticDTO
                    {
                        PhysiotherapyDiagnosticId = pd.PhysiotherapyDiagnosticId,
                        Description = pd.Description,
                        MedicalCareId = pd.MedicalCareId
                    })
                    .ToListAsync();

                var osteoarticularEvaluations = await _context.OsteoarticularEvaluations
                    .Where(oe => oe.MedicalCareId == id)
                    .Select(oe => new OsteoarticularEvaluationDTO
                    {
                        OsteoarticularEvaluationId = oe.OsteoarticularEvaluationId,
                        JointConditionId = oe.JointConditionId,
                        JointRangeOfMotionId = oe.JointRangeOfMotionId,
                        MedicalCareId = oe.MedicalCareId
                    })
                    .ToListAsync();


        var neuromuscularEvaluations = await _context.NeuromuscularEvaluations
                    .Where(ne => ne.MedicalCareId == id)
                    .Select(ne => new NeuromuscularEvaluationDTO
                    {
                        NeuromuscularEvaluationId = ne.NeuromuscularEvaluationId,
                        ShadeId = ne.ShadeId,
                        StrengthId = ne.StrengthId,
                        TrophismId = ne.TrophismId,
                        MedicalCareId = ne.MedicalCareId
                    })
                    .ToListAsync();

        var sensitivityEvaluations = await _context.SensitivityEvaluations
                    .Where(se => se.MedicalCareId == id)
                    .Select(se => new SensitivityEvaluationDTO
                    {
                        SensitivityEvaluationId = se.SensitivityEvaluationId,
                        Demandmas = se.Demandmas,
                        SensitivityLevelId = se.SensitivityLevelId,
                        BodyZoneId = se.BodyZoneId,
                        MedicalCareId = se.MedicalCareId
                    })
                    .ToListAsync();

                
        var skinEvaluations = await _context.SkinEvaluations
                    .Where(se => se.MedicalCareId == id)
                    .Select(se => new SkinEvaluationDTO
                    {
                        SkinEvaluationId = se.SkinEvaluationId,
                        ColorId = se.ColorId,
                        EdemaId = se.EdemaId,
                        StatusId = se.StatusId,
                        SwellingId = se.SwellingId,
                        MedicalCareId = se.MedicalCareId
                    })
                    .ToListAsync();

        var specialTests = await _context.SpecialTests
                    .Where(st => st.MedicalCareId == id)
                    .Select(st => new SpecialTestDTO
                    {
                        SpecialTestId = st.SpecialTestId,
                        Test = st.Test,
                        Observations = st.Observations,
                        ResultTypeId = st.ResultTypeId,
                        MedicalCareId = st.MedicalCareId
                    })
                    .ToListAsync();

        var painScales = await _context.PainScales
            .Where(ps => ps.MedicalCareId == id)
            .Select(ps => new PainScaleDTO
            {
                PainScaleId = ps.PainScaleId,
                Observation = ps.Observation,
                MedicalCareId = ps.MedicalCareId,

                ActionId = ps.ActionId,
                ActionName = ps.Action != null ? ps.Action.Name : null,

                ScaleId = ps.ScaleId,
                ScaleValue = ps.Scale != null ? ps.Scale.Value : null,
                ScaleDescription = ps.Scale != null ? ps.Scale.Description : null,

                PainMomentId = ps.PainMomentId,
                PainMomentName = ps.PainMoment != null ? ps.PainMoment.Name : null
            })
            .ToListAsync();


                var sessions = await _context.Sessions
                    .Where(s => s.MedicalCareId == id)
                    .Select(s => new SessionsDTO
                    {
                        SessionsId = s.SessionsId,
                        Description = s.Description,
                        Date = s.Date,
                        MedicalCareId = s.MedicalCareId
                    })
                    .ToListAsync();

                // 3) Crear DTO final
                var dto = new MedicalCareDTO
                {
                    CareId = result.CareId,
                    LocationId = result.LocationId,
                    PlaceOfAttentionId = result.PlaceOfAttentionId,
                    NameLocation = result.LocationName ?? "Sin ubicación",
                    PatientId = result.PatientId,
                    NamePatient = result.PatientName?.Trim() ?? "Sin paciente",
                    HealthProfessionalId = result.HealthProfessionalId,
                    NameHealthProfessional = result.ProfessionalName?.Trim() ?? "Sin profesional",
                    CareDate = result.CareDate,
                    Area = result.LocationName ?? "Sin área",
                    CurrentIllnesses = currentIllnesses,
                    PhysiotherapyDiagnostics = physiotherapyDiagnostics,
                    OsteoarticularEvaluations = osteoarticularEvaluations,
                    NeuromuscularEvaluations = neuromuscularEvaluations,
                    SensitivityEvaluations = sensitivityEvaluations,
                    SkinEvaluations = skinEvaluations,
                    SpecialTests = specialTests,
                    PainScales = painScales,
                    Sessions = sessions
                };

                Console.WriteLine($"[DEBUG] DTO creado exitosamente para CareId: {id}");
                Console.WriteLine($"[DEBUG] Patient: {dto.NamePatient}, Professional: {dto.NameHealthProfessional}");

                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception al obtener MedicalCare con ID {id}: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task<MedicalCareDTO> AddAsync(MedicalCareDTO dto)
        {
            var entity = new MedicalCare
            {
                LocationId = dto.LocationId,
                PlaceOfAttentionId = dto.PlaceOfAttentionId,
                PatientId = dto.PatientId,
                HealthProfessionalId = dto.HealthProfessionalId,
                CareDate = dto.CareDate == default ? DateTime.Now : dto.CareDate
            };

            _context.MedicalCares.Add(entity);
            await _context.SaveChangesAsync();

            dto.CareId = entity.CareId;
            return dto;
        }

        public async Task<MedicalCareDTO?> UpdateAsync(MedicalCareDTO dto)
        {
            var entity = await _context.MedicalCares.FindAsync(dto.CareId);
            if (entity == null) return null;

            entity.LocationId = dto.LocationId;
            entity.PlaceOfAttentionId = dto.PlaceOfAttentionId;
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.CareDate = dto.CareDate;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Console.WriteLine($"[DEBUG] Iniciando eliminación de MedicalCare ID: {id}");

                var entity = await _context.MedicalCares
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.Treatments)
                            .ThenInclude(t => t.Indications)
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.OrderDiagnosis)
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.Interconsultations)
                    .Include(m => m.VitalSigns)
                    .Include(m => m.Evolutions)
                    .Include(m => m.ReasonsForConsultation)
                    .Include(m => m.ExamResults)
                    .Include(m => m.IdentifiedDiseases)
                    .Include(m => m.PhysicalExams)
                    .Include(m => m.ReviewSystemDevices)
                    .Include(m => m.MedicalReferrals)
                    .Include(m => m.MedicalServices)
                    .Include(m => m.MedicalProcedures)
                    .Include(m => m.AdditionalData)
                    .Include(m => m.MedicalEvaluations)
                    // <AGREGAR> Incluir relaciones de fisioterapia para eliminar
                    .Include(m => m.CurrentIllnesses)
                    .Include(m => m.PhysiotherapyDiagnostics)
                    .Include(m => m.OsteoarticularEvaluations)
                    .Include(m => m.NeuromuscularEvaluations)
                    .Include(m => m.SensitivityEvaluations)
                    .Include(m => m.SkinEvaluations)
                    .Include(m => m.SpecialTests)
                    .Include(m => m.PainScales)
                    .Include(m => m.Sessions)
                    .FirstOrDefaultAsync(m => m.CareId == id);

                if (entity == null)
                {
                    Console.WriteLine($"[DEBUG] No se encontró MedicalCare con ID: {id}");
                    return false;
                }

                Console.WriteLine($"[DEBUG] Entidad encontrada, eliminando relaciones...");

                // Eliminar diagnósticos y tratamientos
                foreach (var diagnosis in entity.Diagnoses)
                {
                    foreach (var treatment in diagnosis.Treatments)
                    {
                        _context.Indications.RemoveRange(treatment.Indications);

                        var pharmacological = await _context.PharmacologicalTreatments
                            .Where(pt => pt.Id == treatment.Id)
                            .ToListAsync();
                        _context.PharmacologicalTreatments.RemoveRange(pharmacological);

                        var nonPharmacological = await _context.NonPharmacologicalTreatments
                            .Where(npt => npt.Id == treatment.Id)
                            .ToListAsync();
                        _context.NonPharmacologicalTreatments.RemoveRange(nonPharmacological);
                    }

                    _context.Treatments.RemoveRange(diagnosis.Treatments);
                    _context.OrderDiagnosis.RemoveRange(diagnosis.OrderDiagnosis);
                    _context.Interconsultations.RemoveRange(diagnosis.Interconsultations);
                }

                _context.Diagnosis.RemoveRange(entity.Diagnoses);

                // Eliminar otros registros relacionados
                if (entity.VitalSigns != null)
                    _context.VitalSigns.Remove(entity.VitalSigns);

                _context.Evolutions.RemoveRange(entity.Evolutions);
                _context.ReasonForConsultations.RemoveRange(entity.ReasonsForConsultation);
                _context.ExamResults.RemoveRange(entity.ExamResults);
                _context.IdentifiedDiseases.RemoveRange(entity.IdentifiedDiseases);
                _context.PhysicalExams.RemoveRange(entity.PhysicalExams);
                _context.ReviewSystemDevices.RemoveRange(entity.ReviewSystemDevices);

                if (entity.MedicalReferrals != null && entity.MedicalReferrals.Any())
                    _context.MedicalReferrals.RemoveRange(entity.MedicalReferrals);

                _context.MedicalServices.RemoveRange(entity.MedicalServices);
                _context.MedicalProcedures.RemoveRange(entity.MedicalProcedures);

                if (entity.AdditionalData != null)
                    _context.AdditionalData.Remove(entity.AdditionalData);

                _context.MedicalEvaluations.RemoveRange(entity.MedicalEvaluations);

                // <AGREGAR> Eliminar relaciones de fisioterapia
                Console.WriteLine($"[DEBUG] Eliminando relaciones de fisioterapia...");
                _context.CurrentIllnesses.RemoveRange(entity.CurrentIllnesses);
                _context.PhysiotherapyDiagnostics.RemoveRange(entity.PhysiotherapyDiagnostics);
                _context.OsteoarticularEvaluations.RemoveRange(entity.OsteoarticularEvaluations);
                _context.NeuromuscularEvaluations.RemoveRange(entity.NeuromuscularEvaluations);
                _context.SensitivityEvaluations.RemoveRange(entity.SensitivityEvaluations);
                _context.SkinEvaluations.RemoveRange(entity.SkinEvaluations);
                _context.SpecialTests.RemoveRange(entity.SpecialTests);
                _context.PainScales.RemoveRange(entity.PainScales);
                _context.Sessions.RemoveRange(entity.Sessions);

                // Finalmente eliminar la atención médica
                Console.WriteLine($"[DEBUG] Eliminando MedicalCare...");
                _context.MedicalCares.Remove(entity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                Console.WriteLine($"[DEBUG] MedicalCare ID {id} eliminado exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error al eliminar MedicalCare ID {id}: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<MedicalCareDTO>> GetPhysiotherapyCareAsync()
        {
            var allCares = await _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .ToListAsync();

            Console.WriteLine($"[DEBUG] Total MedicalCares en BD: {allCares.Count}");

            foreach (var care in allCares)
            {
                var locationName = care.LocationNavigation?.Name ?? "NULL";
                Console.WriteLine($"[DEBUG] CareId: {care.CareId}, LocationId: {care.LocationId}, LocationName: {locationName}");
            }

            var medicalCares = allCares
                .Where(m => m.LocationNavigation != null &&
                           (m.LocationNavigation.Name.ToLower() == "fisioterapia" ||
                            m.LocationNavigation.Name.ToLower().Contains("fisio")))
                .OrderByDescending(m => m.CareDate)
                .ToList();

            Console.WriteLine($"[DEBUG] MedicalCares de fisioterapia filtrados: {medicalCares.Count}");

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByPatientDocumentAsync(string documentNumber)
        {
            var medicalCares = await _context.MedicalCares
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(pn => pn.PersonDocument)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.Patient.PersonNavigation.PersonDocument.DocumentNumber == documentNumber)
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }


    }
}
