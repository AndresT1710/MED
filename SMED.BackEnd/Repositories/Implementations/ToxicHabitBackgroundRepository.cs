using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ToxicHabitBackgroundRepository : IRepository<ToxicHabitBackgroundDTO, int>
    {
        private readonly SGISContext _context;

        public ToxicHabitBackgroundRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ToxicHabitBackgroundDTO>> GetAllAsync()
        {
            var entities = await _context.ToxicHabitHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ToxicHabitBackgroundDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ToxicHabitHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ToxicHabitBackgroundDTO> AddAsync(ToxicHabitBackgroundDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ToxicHabitHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ToxicHabitBackgroundDTO?> UpdateAsync(ToxicHabitBackgroundDTO dto)
        {
            var entity = await _context.ToxicHabitHistories.FindAsync(dto.ToxicHabitBackgroundId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Description = dto.Description;
            entity.RecordDate = dto.RecordDate;
            entity.ToxicHabitId = dto.ToxicHabitId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ToxicHabitHistories.FindAsync(id);
            if (entity == null) return false;

            _context.ToxicHabitHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private ToxicHabitBackgroundDTO MapToDto(ToxicHabitBackground entity)
        {
            return new ToxicHabitBackgroundDTO
            {
                ToxicHabitBackgroundId = entity.ToxicHabitBackgroundId,
                HistoryNumber = entity.HistoryNumber,
                Description = entity.Description,
                RecordDate = entity.RecordDate,
                ToxicHabitId = entity.ToxicHabitId,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private ToxicHabitBackground MapToEntity(ToxicHabitBackgroundDTO dto)
        {
            return new ToxicHabitBackground
            {
                ToxicHabitBackgroundId = dto.ToxicHabitBackgroundId,
                HistoryNumber = dto.HistoryNumber,
                Description = dto.Description,
                RecordDate = dto.RecordDate,
                ToxicHabitId = dto.ToxicHabitId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }

}
