using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SleepHabitRepository : IRepository<SleepHabitDTO, int>
    {
        private readonly SGISContext _context;

        public SleepHabitRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SleepHabitDTO>> GetAllAsync()
        {
            var entities = await _context.SleepHabits.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SleepHabitDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SleepHabits.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SleepHabitDTO> AddAsync(SleepHabitDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SleepHabits.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SleepHabitDTO?> UpdateAsync(SleepHabitDTO dto)
        {
            var entity = await _context.SleepHabits.FindAsync(dto.SleepHabitId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SleepHabits.FindAsync(id);
            if (entity == null) return false;

            _context.SleepHabits.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SleepHabitDTO MapToDto(SleepHabit entity)
        {
            return new SleepHabitDTO
            {
                SleepHabitId = entity.SleepHabitId,
                Name = entity.Name
            };
        }

        private SleepHabit MapToEntity(SleepHabitDTO dto)
        {
            return new SleepHabit
            {
                SleepHabitId = dto.SleepHabitId,
                Name = dto.Name
            };
        }
    }
}
