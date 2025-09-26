using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ScaleRepository : IRepository<ScaleDTO, int>
    {
        private readonly SGISContext _context;

        public ScaleRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ScaleDTO>> GetAllAsync()
        {
            var entities = await _context.Scales.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ScaleDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Scales.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ScaleDTO> AddAsync(ScaleDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Scales.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ScaleDTO?> UpdateAsync(ScaleDTO dto)
        {
            var entity = await _context.Scales.FindAsync(dto.ScaleId);
            if (entity == null) return null;

            entity.Value = dto.Value;
            entity.Description = dto.Description;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Scales.FindAsync(id);
            if (entity == null) return false;

            _context.Scales.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private ScaleDTO MapToDto(Scale entity) => new ScaleDTO
        {
            ScaleId = entity.ScaleId,
            Value = entity.Value,
            Description = entity.Description
        };

        private Scale MapToEntity(ScaleDTO dto) => new Scale
        {
            ScaleId = dto.ScaleId,
            Value = dto.Value,
            Description = dto.Description
        };
    }
}
