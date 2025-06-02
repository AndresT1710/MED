using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class HabitsRepository : IRepository<HabitsDTO, int>
    {
        private readonly SGISContext _context;

        public HabitsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<HabitsDTO>> GetAllAsync()
        {
            var entities = await _context.Habits.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<HabitsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Habits.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<HabitsDTO> AddAsync(HabitsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Habits.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<HabitsDTO?> UpdateAsync(HabitsDTO dto)
        {
            var entity = await _context.Habits.FindAsync(dto.HabitId);
            if (entity == null) return null;
            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Habits.FindAsync(id);
            if (entity == null) return false;
            _context.Habits.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private HabitsDTO MapToDto(Habits entity)
        {
            return new HabitsDTO
            {
                HabitId = entity.HabitId,
                Name = entity.Name
            };
        }

        private Habits MapToEntity(HabitsDTO dto)
        {
            return new Habits
            {
                HabitId = dto.HabitId,
                Name = dto.Name
            };


        }
    }
}
