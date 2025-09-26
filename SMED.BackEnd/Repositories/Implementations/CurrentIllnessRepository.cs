using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class CurrentIllnessRepository : IRepository<CurrentIllnessDTO, int>
    {
        private readonly SGISContext _context;

        public CurrentIllnessRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<CurrentIllnessDTO>> GetAllAsync()
        {
            var entities = await _context.CurrentIllnesses.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<CurrentIllnessDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.CurrentIllnesses.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<CurrentIllnessDTO> AddAsync(CurrentIllnessDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.CurrentIllnesses.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<CurrentIllnessDTO?> UpdateAsync(CurrentIllnessDTO dto)
        {
            var entity = await _context.CurrentIllnesses.FindAsync(dto.CurrentIllnessId);
            if (entity == null) return null;

            entity.EvolutionTime = dto.EvolutionTime;
            entity.Localization = dto.Localization;
            entity.Intensity = dto.Intensity;
            entity.AggravatingFactors = dto.AggravatingFactors;
            entity.MitigatingFactors = dto.MitigatingFactors;
            entity.NocturnalPain = dto.NocturnalPain;
            entity.Weakness = dto.Weakness;
            entity.Paresthesias = dto.Paresthesias;
            entity.ComplementaryExams = dto.ComplementaryExams;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CurrentIllnesses.FindAsync(id);
            if (entity == null) return false;

            _context.CurrentIllnesses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private CurrentIllnessDTO MapToDto(CurrentIllness entity)
        {
            return new CurrentIllnessDTO
            {
                CurrentIllnessId = entity.CurrentIllnessId,
                EvolutionTime = entity.EvolutionTime,
                Localization = entity.Localization,
                Intensity = entity.Intensity,
                AggravatingFactors = entity.AggravatingFactors,
                MitigatingFactors = entity.MitigatingFactors,
                NocturnalPain = entity.NocturnalPain,
                Weakness = entity.Weakness,
                Paresthesias = entity.Paresthesias,
                ComplementaryExams = entity.ComplementaryExams,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private CurrentIllness MapToEntity(CurrentIllnessDTO dto)
        {
            return new CurrentIllness
            {
                CurrentIllnessId = dto.CurrentIllnessId,
                EvolutionTime = dto.EvolutionTime,
                Localization = dto.Localization,
                Intensity = dto.Intensity,
                AggravatingFactors = dto.AggravatingFactors,
                MitigatingFactors = dto.MitigatingFactors,
                NocturnalPain = dto.NocturnalPain,
                Weakness = dto.Weakness,
                Paresthesias = dto.Paresthesias,
                ComplementaryExams = dto.ComplementaryExams,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}
