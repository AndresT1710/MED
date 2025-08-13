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
                NameLocation = m.PlaceOfAttentionNavigation?.Name,
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
                Area = m.Area,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetNursingCareAsync()
        {
            var medicalCares = await _context.MedicalCares
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.Area.ToLower() == "enfermería" || m.Area.ToLower() == "enfermeria")
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                NameLocation = m.PlaceOfAttentionNavigation?.Name,
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
                Area = m.Area,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByAreaAndDateAsync(string area, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.Area.ToLower() == area.ToLower());

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
                NameLocation = m.PlaceOfAttentionNavigation?.Name,
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
                Area = m.Area,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<MedicalCareDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalCares
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .FirstOrDefaultAsync(m => m.CareId == id);

            return entity == null ? null : new MedicalCareDTO
            {
                CareId = entity.CareId,
                LocationId = entity.LocationId,
                NameLocation = entity.PlaceOfAttentionNavigation?.Name,
                PatientId = entity.PatientId,
                NamePatient = entity.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        entity.Patient.PersonNavigation.FirstName,
                        entity.Patient.PersonNavigation.MiddleName,
                        entity.Patient.PersonNavigation.LastName,
                        entity.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                HealthProfessionalId = entity.HealthProfessionalId,
                NameHealthProfessional = entity.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        entity.HealthProfessional.PersonNavigation.FirstName,
                        entity.HealthProfessional.PersonNavigation.MiddleName,
                        entity.HealthProfessional.PersonNavigation.LastName,
                        entity.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                Area = entity.Area,
                CareDate = entity.CareDate
            };
        }

        public async Task<MedicalCareDTO> AddAsync(MedicalCareDTO dto)
        {
            var entity = new MedicalCare
            {
                LocationId = dto.LocationId,
                PatientId = dto.PatientId,
                HealthProfessionalId = dto.HealthProfessionalId,
                Area = dto.Area ?? string.Empty,
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
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.Area = dto.Area ?? string.Empty;
            entity.CareDate = dto.CareDate;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // ✅ Usar una transacción para asegurar consistencia
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
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
                    .Include(m => m.ReviewSystemDevices) // ✅ Incluir ReviewSystemDevices
                    .Include(m => m.MedicalReferral)
                    .Include(m => m.MedicalServices)
                    .Include(m => m.MedicalProcedures)
                    .Include(m => m.AdditionalData)
                    .FirstOrDefaultAsync(m => m.CareId == id);

                if (entity == null)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // ✅ Eliminar en el orden correcto para evitar violaciones de FK

                // 1. Eliminar Indications (relacionadas con Treatments)
                foreach (var diagnosis in entity.Diagnoses)
                {
                    foreach (var treatment in diagnosis.Treatments)
                    {
                        if (treatment.Indications.Any())
                        {
                            _context.Indications.RemoveRange(treatment.Indications);
                        }
                    }
                }

                // 2. Eliminar Treatments (relacionados con Diagnoses)
                foreach (var diagnosis in entity.Diagnoses)
                {
                    if (diagnosis.Treatments.Any())
                    {
                        _context.Treatments.RemoveRange(diagnosis.Treatments);
                    }
                }

                // 3. Eliminar OrderDiagnosis e Interconsultations (relacionados con Diagnoses)
                foreach (var diagnosis in entity.Diagnoses)
                {
                    if (diagnosis.OrderDiagnosis.Any())
                    {
                        _context.OrderDiagnosis.RemoveRange(diagnosis.OrderDiagnosis);
                    }
                    if (diagnosis.Interconsultations.Any())
                    {
                        _context.Interconsultations.RemoveRange(diagnosis.Interconsultations);
                    }
                }

                // 4. Eliminar Diagnoses
                if (entity.Diagnoses.Any())
                {
                    _context.Diagnosis.RemoveRange(entity.Diagnoses);
                }

                // 5. Eliminar otros registros relacionados directamente con MedicalCare
                if (entity.VitalSigns != null)
                {
                    _context.VitalSigns.Remove(entity.VitalSigns);
                }

                if (entity.Evolutions.Any())
                {
                    _context.Evolutions.RemoveRange(entity.Evolutions);
                }

                if (entity.ReasonsForConsultation.Any())
                {
                    _context.ReasonForConsultations.RemoveRange(entity.ReasonsForConsultation);
                }

                if (entity.ExamResults.Any())
                {
                    _context.ExamResults.RemoveRange(entity.ExamResults);
                }

                if (entity.IdentifiedDiseases.Any())
                {
                    _context.IdentifiedDiseases.RemoveRange(entity.IdentifiedDiseases);
                }

                if (entity.PhysicalExams.Any())
                {
                    _context.PhysicalExams.RemoveRange(entity.PhysicalExams);
                }

                // ✅ Eliminar ReviewSystemDevices (esta era la causa del error)
                if (entity.ReviewSystemDevices.Any())
                {
                    _context.ReviewSystemDevices.RemoveRange(entity.ReviewSystemDevices);
                }

                if (entity.MedicalReferral != null)
                {
                    _context.MedicalReferrals.Remove(entity.MedicalReferral);
                }

                if (entity.MedicalServices.Any())
                {
                    _context.MedicalServices.RemoveRange(entity.MedicalServices);
                }

                if (entity.MedicalProcedures.Any())
                {
                    _context.MedicalProcedures.RemoveRange(entity.MedicalProcedures);
                }

                if (entity.AdditionalData != null)
                {
                    _context.AdditionalData.Remove(entity.AdditionalData);
                }

                // 6. Finalmente, eliminar la MedicalCare
                _context.MedicalCares.Remove(entity);

                // ✅ Guardar todos los cambios
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // Re-lanzar la excepción para que sea manejada por el controlador
            }
        }

        public async Task<List<MedicalCareDTO>> GetByPatientDocumentAsync(string documentNumber)
        {
            var medicalCares = await _context.MedicalCares
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
                NameLocation = m.PlaceOfAttentionNavigation?.Name,
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
                Area = m.Area,
                CareDate = m.CareDate
            }).ToList();
        }

    }
}
