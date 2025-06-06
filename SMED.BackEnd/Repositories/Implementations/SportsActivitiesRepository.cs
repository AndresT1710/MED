using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SportsActivitiesRepository : IRepository<SportsActivitiesDTO, int>
    {
        private readonly SGISContext _context;

        public SportsActivitiesRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SportsActivitiesDTO>> GetAllAsync()
        {
            var entities = await _context.SportsActivities.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SportsActivitiesDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SportsActivities.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SportsActivitiesDTO> AddAsync(SportsActivitiesDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SportsActivities.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SportsActivitiesDTO?> UpdateAsync(SportsActivitiesDTO dto)
        {
            var entity = await _context.SportsActivities.FindAsync(dto.SportActivityId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SportsActivities.FindAsync(id);
            if (entity == null) return false;

            _context.SportsActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SportsActivitiesDTO MapToDto(SportsActivities entity)
        {
            return new SportsActivitiesDTO
            {
                SportActivityId = entity.SportActivityId,
                Name = entity.Name
            };
        }

        private SportsActivities MapToEntity(SportsActivitiesDTO dto)
        {
            return new SportsActivities
            {
                SportActivityId = dto.SportActivityId,
                Name = dto.Name
            };
        }
    }
}
