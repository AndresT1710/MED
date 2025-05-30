using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ToxicHabitRepository : IRepository<ToxicHabitDTO, int>
    {
        private readonly SGISContext _context;

        public ToxicHabitRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ToxicHabitDTO>> GetAllAsync()
        {
            var entities = await _context.ToxicHabits.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ToxicHabitDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ToxicHabits.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ToxicHabitDTO> AddAsync(ToxicHabitDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ToxicHabits.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ToxicHabitDTO?> UpdateAsync(ToxicHabitDTO dto)
        {
            var entity = await _context.ToxicHabits.FindAsync(dto.ToxicHabitId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ToxicHabits.FindAsync(id);
            if (entity == null) return false;

            _context.ToxicHabits.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private ToxicHabitDTO MapToDto(ToxicHabit entity)
        {
            return new ToxicHabitDTO
            {
                ToxicHabitId = entity.ToxicHabitId,
                Name = entity.Name
            };
        }

        private ToxicHabit MapToEntity(ToxicHabitDTO dto)
        {
            return new ToxicHabit
            {
                ToxicHabitId = dto.ToxicHabitId,
                Name = dto.Name
            };
        }
    }

}
