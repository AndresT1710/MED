using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ShadeRepository : IRepository<ShadeDTO, int>
    {
        private readonly SGISContext _context;

        public ShadeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ShadeDTO>> GetAllAsync()
        {
            var entities = await _context.Shades.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ShadeDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Shades.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ShadeDTO> AddAsync(ShadeDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Shades.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ShadeDTO?> UpdateAsync(ShadeDTO dto)
        {
            var entity = await _context.Shades.FindAsync(dto.ShadeId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Shades.FindAsync(id);
            if (entity == null) return false;

            _context.Shades.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private ShadeDTO MapToDto(Shade entity)
        {
            return new ShadeDTO
            {
                ShadeId = entity.ShadeId,
                Name = entity.Name
            };
        }

        private Shade MapToEntity(ShadeDTO dto)
        {
            return new Shade
            {
                ShadeId = dto.ShadeId,
                Name = dto.Name
            };
        }
    }
}
