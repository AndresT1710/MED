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
                .Include(m => m.Treatments)
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
                CareDate = m.CareDate,
                TreatmentIds = m.Treatments.Select(t => t.Id).ToList()
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
                .Include(m => m.Treatments)
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
                CareDate = m.CareDate,
                TreatmentIds = m.Treatments.Select(t => t.Id).ToList()
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
                .Include(m => m.Treatments)
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
                CareDate = m.CareDate,
                TreatmentIds = m.Treatments.Select(t => t.Id).ToList()
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
                .Include(m => m.Treatments)
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
                CareDate = entity.CareDate,
                TreatmentIds = entity.Treatments.Select(t => t.Id).ToList() 
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

            // ✅ Asignar tratamientos si se proporcionan
            if (dto.TreatmentIds.Any())
            {
                var treatments = await _context.Treatments
                    .Where(t => dto.TreatmentIds.Contains(t.Id))
                    .ToListAsync();
                entity.Treatments = treatments;
            }

            _context.MedicalCares.Add(entity);
            await _context.SaveChangesAsync();

            dto.CareId = entity.CareId;
            return dto;
        }

        public async Task<MedicalCareDTO?> UpdateAsync(MedicalCareDTO dto)
        {
            var entity = await _context.MedicalCares
                .Include(m => m.Treatments)
                .FirstOrDefaultAsync(m => m.CareId == dto.CareId);

            if (entity == null) return null;

            entity.LocationId = dto.LocationId;
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.Area = dto.Area ?? string.Empty;
            entity.CareDate = dto.CareDate;

            // ✅ Actualizar tratamientos
            entity.Treatments.Clear();
            if (dto.TreatmentIds.Any())
            {
                var treatments = await _context.Treatments
                    .Where(t => dto.TreatmentIds.Contains(t.Id))
                    .ToListAsync();
                foreach (var treatment in treatments)
                {
                    entity.Treatments.Add(treatment);
                }
            }

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalCares
                .Include(m => m.Treatments)
                .FirstOrDefaultAsync(m => m.CareId == id);

            if (entity == null) return false;

            _context.MedicalCares.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
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
                .Include(m => m.Treatments)
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
                CareDate = m.CareDate,
                TreatmentIds = m.Treatments.Select(t => t.Id).ToList()
            }).ToList();
        }

        // ✅ Método adicional para asignar tratamientos a una atención médica
        public async Task<bool> AssignTreatmentsAsync(int medicalCareId, List<int> treatmentIds)
        {
            var medicalCare = await _context.MedicalCares
                .Include(m => m.Treatments)
                .FirstOrDefaultAsync(m => m.CareId == medicalCareId);

            if (medicalCare == null) return false;

            var treatments = await _context.Treatments
                .Where(t => treatmentIds.Contains(t.Id))
                .ToListAsync();

            // Limpiar tratamientos existentes y agregar los nuevos
            medicalCare.Treatments.Clear();
            foreach (var treatment in treatments)
            {
                medicalCare.Treatments.Add(treatment);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
