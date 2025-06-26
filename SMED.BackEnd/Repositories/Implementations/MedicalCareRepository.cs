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
                Area = m.Area
            }).ToList();
        }


        public async Task<MedicalCareDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalCares.FindAsync(id);
            return entity == null ? null : new MedicalCareDTO
            {
                CareId = entity.CareId,
                LocationId = entity.LocationId,
                PatientId = entity.PatientId,
                HealthProfessionalId = entity.HealthProfessionalId
            };
        }

        public async Task<MedicalCareDTO> AddAsync(MedicalCareDTO dto)
        {
            var entity = new MedicalCare
            {
                LocationId = dto.LocationId,
                PatientId = dto.PatientId,
                HealthProfessionalId = dto.HealthProfessionalId,
                Area = dto.Area ?? string.Empty // Ensure Area is not null

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
            entity.Area = dto.Area ?? string.Empty; // Ensure Area is not null
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalCares.FindAsync(id);
            if (entity == null) return false;
            _context.MedicalCares.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
