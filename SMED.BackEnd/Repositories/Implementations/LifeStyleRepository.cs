using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class LifeStyleRepository : IRepository<LifeStyleDTO, int>
    {
        private readonly SGISContext _context;

        public LifeStyleRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<LifeStyleDTO>> GetAllAsync()
        {
            var entities = await _context.LifeStyles.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<LifeStyleDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.LifeStyles.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<LifeStyleDTO> AddAsync(LifeStyleDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.LifeStyles.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<LifeStyleDTO?> UpdateAsync(LifeStyleDTO dto)
        {
            var entity = await _context.LifeStyles.FindAsync(dto.LifeStyleId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.LifeStyles.FindAsync(id);
            if (entity == null) return false;

            _context.LifeStyles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapeos
        private LifeStyleDTO MapToDto(LifeStyle entity)
        {
            return new LifeStyleDTO
            {
                LifeStyleId = entity.LifeStyleId,
                Name = entity.Name
            };
        }

        private LifeStyle MapToEntity(LifeStyleDTO dto)
        {
            return new LifeStyle
            {
                LifeStyleId = dto.LifeStyleId,
                Name = dto.Name
            };
        }
    }
}
