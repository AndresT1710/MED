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
                PlaceOfAttentionId = m.PlaceOfAttentionId,
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
                            (m.LocationNavigation.Name.ToLower() == "enfermería" || m.LocationNavigation.Name.ToLower() == "enfermeria"))
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                NameLocation = m.LocationNavigation?.Name, // ahora sacamos el nombre de Location
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
                Area = m.LocationNavigation?.Name ?? "N/A", // cambiamos el área por el nombre del Location
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
                PlaceOfAttentionId = m.PlaceOfAttentionId,
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
                Area = m.PlaceOfAttentionNavigation?.Name ?? "N/A",
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByPlaceOfAttentionAndDateAsync(int? placeOfAttentionId = null, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.Area.ToLower() == "enfermería" || m.Area.ToLower() == "enfermeria");

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
                Area = m.PlaceOfAttentionNavigation?.Name ?? "N/A",
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
                PlaceOfAttentionId = entity.PlaceOfAttentionId,
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
                Area = entity.PlaceOfAttentionNavigation?.Name ?? "N/A",
                CareDate = entity.CareDate
            };
        }

        public async Task<MedicalCareDTO> AddAsync(MedicalCareDTO dto)
        {
            var entity = new MedicalCare
            {
                LocationId = dto.LocationId,
                PlaceOfAttentionId = dto.PlaceOfAttentionId,
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
            entity.PlaceOfAttentionId = dto.PlaceOfAttentionId;
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.Area = dto.Area ?? string.Empty;
            entity.CareDate = dto.CareDate;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
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
                    .Include(m => m.ReviewSystemDevices)
                    .Include(m => m.MedicalReferral)
                    .Include(m => m.MedicalServices)
                    .Include(m => m.MedicalProcedures)
                    .Include(m => m.AdditionalData)
                    .FirstOrDefaultAsync(m => m.CareId == id);

                if (entity == null) return false;

                // Eliminar en orden correcto para evitar conflictos de FK
                foreach (var diagnosis in entity.Diagnoses)
                {
                    foreach (var treatment in diagnosis.Treatments)
                    {
                        // Eliminar indicaciones del tratamiento
                        _context.Indications.RemoveRange(treatment.Indications);

                        // Eliminar tratamientos farmacológicos y no farmacológicos
                        var pharmacological = await _context.PharmacologicalTreatments
                            .Where(pt => pt.Id == treatment.Id)
                            .ToListAsync();
                        _context.PharmacologicalTreatments.RemoveRange(pharmacological);

                        var nonPharmacological = await _context.NonPharmacologicalTreatments
                            .Where(npt => npt.Id == treatment.Id)
                            .ToListAsync();
                        _context.NonPharmacologicalTreatments.RemoveRange(nonPharmacological);
                    }

                    // Eliminar tratamientos
                    _context.Treatments.RemoveRange(diagnosis.Treatments);

                    // Eliminar órdenes de diagnóstico e interconsultas
                    _context.OrderDiagnosis.RemoveRange(diagnosis.OrderDiagnosis);
                    _context.Interconsultations.RemoveRange(diagnosis.Interconsultations);
                }

                // Eliminar diagnósticos
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

                if (entity.MedicalReferral != null)
                    _context.MedicalReferrals.Remove(entity.MedicalReferral);

                _context.MedicalServices.RemoveRange(entity.MedicalServices);
                _context.MedicalProcedures.RemoveRange(entity.MedicalProcedures);

                if (entity.AdditionalData != null)
                    _context.AdditionalData.Remove(entity.AdditionalData);

                // Finalmente eliminar la atención médica
                _context.MedicalCares.Remove(entity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
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
                PlaceOfAttentionId = m.PlaceOfAttentionId,
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
                Area = m.PlaceOfAttentionNavigation?.Name ?? "N/A",
                CareDate = m.CareDate
            }).ToList();
        }
    }
}
