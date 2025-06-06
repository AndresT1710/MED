using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SleepHabitHistoryRepository : IRepository<SleepHabitHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public SleepHabitHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SleepHabitHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.SleepHabitHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SleepHabitHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SleepHabitHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SleepHabitHistoryDTO> AddAsync(SleepHabitHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SleepHabitHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SleepHabitHistoryDTO?> UpdateAsync(SleepHabitHistoryDTO dto)
        {
            var entity = await _context.SleepHabitHistories.FindAsync(dto.HabitSleepHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.RecordDate = dto.RecordDate;
            entity.SleepHabitId = dto.SleepHabitId;
            entity.Description = dto.Description;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SleepHabitHistories.FindAsync(id);
            if (entity == null) return false;

            _context.SleepHabitHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SleepHabitHistoryDTO MapToDto(SleepHabitHistory entity)
        {
            return new SleepHabitHistoryDTO
            {
                HabitSleepHistoryId = entity.HabitSleepHistoryId,
                HistoryNumber = entity.HistoryNumber,
                RecordDate = entity.RecordDate,
                SleepHabitId = entity.SleepHabitId,
                Description = entity.Description,
                ClinicalHistoryId = entity.ClinicalHistoryId
            };
        }

        private SleepHabitHistory MapToEntity(SleepHabitHistoryDTO dto)
        {
            return new SleepHabitHistory
            {
                HabitSleepHistoryId = dto.HabitSleepHistoryId,
                HistoryNumber = dto.HistoryNumber,
                RecordDate = dto.RecordDate,
                SleepHabitId = dto.SleepHabitId,
                Description = dto.Description,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };
        }
    }
}
