using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class HabitHistoryRepository : IRepository<HabitHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public HabitHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<HabitHistoryDTO>> GetAllAsync()
        {
            return await _context.HabitHistories
                .Include(h => h.Habit)
                .Select(h => new HabitHistoryDTO
                {
                    HabitHistoryId = h.HabitHistoryId,
                    HistoryNumber = h.HistoryNumber,
                    RecordDate = h.RecordDate,
                    HabitId = h.HabitId,
                    ClinicalHistoryId = h.ClinicalHistoryId,
                    HabitName = h.Habit != null ? h.Habit.Name : null
                })
                .ToListAsync();
        }

        public async Task<HabitHistoryDTO?> GetByIdAsync(int id)
        {
            return await _context.HabitHistories
                .Include(h => h.Habit)
                .Where(h => h.HabitHistoryId == id)
                .Select(h => new HabitHistoryDTO
                {
                    HabitHistoryId = h.HabitHistoryId,
                    HistoryNumber = h.HistoryNumber,
                    RecordDate = h.RecordDate,
                    HabitId = h.HabitId,
                    ClinicalHistoryId = h.ClinicalHistoryId,
                    HabitName = h.Habit != null ? h.Habit.Name : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<HabitHistoryDTO> AddAsync(HabitHistoryDTO dto)
        {
            var entity = new HabitHistory
            {
                HistoryNumber = dto.HistoryNumber,
                RecordDate = dto.RecordDate,
                HabitId = dto.HabitId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };

            _context.HabitHistories.Add(entity);
            await _context.SaveChangesAsync();

            return new HabitHistoryDTO
            {
                HabitHistoryId = entity.HabitHistoryId,
                HistoryNumber = entity.HistoryNumber,
                RecordDate = entity.RecordDate,
                HabitId = entity.HabitId,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                HabitName = entity.Habit?.Name
            };
        }

        public async Task<HabitHistoryDTO?> UpdateAsync(HabitHistoryDTO dto)
        {
            var entity = await _context.HabitHistories.FindAsync(dto.HabitHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.RecordDate = dto.RecordDate;
            entity.HabitId = dto.HabitId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();

            return new HabitHistoryDTO
            {
                HabitHistoryId = entity.HabitHistoryId,
                HistoryNumber = entity.HistoryNumber,
                RecordDate = entity.RecordDate,
                HabitId = entity.HabitId,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                HabitName = entity.Habit?.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.HabitHistories.FindAsync(id);
            if (entity == null) return false;

            _context.HabitHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}