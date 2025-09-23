using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class CurrentProblemHistoryRepository : IRepository<CurrentProblemHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public CurrentProblemHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<CurrentProblemHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.CurrentProblemHistories
                .Include(cp => cp.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<CurrentProblemHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.CurrentProblemHistories
                .Include(cp => cp.ClinicalHistory)
                .FirstOrDefaultAsync(cp => cp.CurrentProblemHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<CurrentProblemHistoryDTO> AddAsync(CurrentProblemHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.CurrentProblemHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<CurrentProblemHistoryDTO?> UpdateAsync(CurrentProblemHistoryDTO dto)
        {
            var entity = await _context.CurrentProblemHistories.FindAsync(dto.CurrentProblemHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.AppearanceEvolution = dto.AppearanceEvolution;
            entity.TriggeringFactors = dto.TriggeringFactors;
            entity.FrequencyIntensitySymptoms = dto.FrequencyIntensitySymptoms;
            entity.Impact = dto.Impact;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CurrentProblemHistories.FindAsync(id);
            if (entity == null) return false;

            _context.CurrentProblemHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private CurrentProblemHistoryDTO MapToDto(CurrentProblemHistory entity)
        {
            return new CurrentProblemHistoryDTO
            {
                CurrentProblemHistoryId = entity.CurrentProblemHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                AppearanceEvolution = entity.AppearanceEvolution,
                TriggeringFactors = entity.TriggeringFactors,
                FrequencyIntensitySymptoms = entity.FrequencyIntensitySymptoms,
                Impact = entity.Impact
            };
        }

        private CurrentProblemHistory MapToEntity(CurrentProblemHistoryDTO dto)
        {
            return new CurrentProblemHistory
            {
                CurrentProblemHistoryId = dto.CurrentProblemHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                AppearanceEvolution = dto.AppearanceEvolution,
                TriggeringFactors = dto.TriggeringFactors,
                FrequencyIntensitySymptoms = dto.FrequencyIntensitySymptoms,
                Impact = dto.Impact
            };
        }
    }
}