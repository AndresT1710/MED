using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class RestrictionRepository : IRepository<RestrictionDTO, int>
    {
        private readonly SGISContext _context;

        public RestrictionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<RestrictionDTO>> GetAllAsync()
        {
            var entities = await _context.Restrictions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<RestrictionDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Restrictions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<RestrictionDTO> AddAsync(RestrictionDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Restrictions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<RestrictionDTO?> UpdateAsync(RestrictionDTO dto)
        {
            var entity = await _context.Restrictions.FindAsync(dto.RestrictionId);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.FoodId = dto.FoodId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Restrictions.FindAsync(id);
            if (entity == null) return false;

            _context.Restrictions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RestrictionDTO>> GetByFoodIdAsync(int foodId)
        {
            var entities = await _context.Restrictions
                .Where(r => r.FoodId == foodId)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        private RestrictionDTO MapToDto(Restriction entity)
        {
            return new RestrictionDTO
            {
                RestrictionId = entity.RestrictionId,
                Description = entity.Description,
                FoodId = entity.FoodId
            };
        }

        private Restriction MapToEntity(RestrictionDTO dto)
        {
            return new Restriction
            {
                RestrictionId = dto.RestrictionId,
                Description = dto.Description,
                FoodId = dto.FoodId
            };
        }
    }
}