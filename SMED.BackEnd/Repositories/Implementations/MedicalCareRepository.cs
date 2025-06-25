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

        public async Task<List<MedicalCareDTO>> GetAllAsync() =>
            await _context.MedicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId
            }).ToListAsync();

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
                HealthProfessionalId = dto.HealthProfessionalId
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
