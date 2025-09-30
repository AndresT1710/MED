using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{

    public class ColorRepository : IRepository<ColorDTO, int>
    {
        private readonly SGISContext _context;

        public ColorRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ColorDTO>> GetAllAsync()
        {
            var entities = await _context.Colors.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ColorDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Colors.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ColorDTO> AddAsync(ColorDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Colors.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ColorDTO?> UpdateAsync(ColorDTO dto)
        {
            var entity = await _context.Colors.FindAsync(dto.ColorId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Colors.FindAsync(id);
            if (entity == null) return false;

            _context.Colors.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private ColorDTO MapToDto(Color entity) => new ColorDTO
        {
            ColorId = entity.ColorId,
            Name = entity.Name
        };

        private Color MapToEntity(ColorDTO dto) => new Color
        {
            ColorId = dto.ColorId,
            Name = dto.Name
        };
    }
}