using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ActivityRepository : IRepository<ActivityDTO, int>
    {
        private readonly SGISContext _context;

        public ActivityRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityDTO>> GetAllAsync()
        {
            var entities = await _context.Activities.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ActivityDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Activities.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ActivityDTO> AddAsync(ActivityDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Activities.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ActivityDTO?> UpdateAsync(ActivityDTO dto)
        {
            var entity = await _context.Activities.FindAsync(dto.ActivityId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.SessionId = dto.SessionId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Activities.FindAsync(id);
            if (entity == null) return false;

            _context.Activities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private ActivityDTO MapToDto(Activity entity)
        {
            return new ActivityDTO
            {
                ActivityId = entity.ActivityId,
                Name = entity.Name,
                SessionId = entity.SessionId
            };
        }

        private Activity MapToEntity(ActivityDTO dto)
        {
            return new Activity
            {
                ActivityId = dto.ActivityId,
                Name = dto.Name,
                SessionId = dto.SessionId
            };
        }
    }
}
