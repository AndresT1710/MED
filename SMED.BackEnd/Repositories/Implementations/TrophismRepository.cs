using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TrophismRepository : IRepository<TrophismDTO, int>
    {
        private readonly SGISContext _context;

        public TrophismRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TrophismDTO>> GetAllAsync()
        {
            var entities = await _context.Trophisms.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TrophismDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Trophisms.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TrophismDTO> AddAsync(TrophismDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Trophisms.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TrophismDTO?> UpdateAsync(TrophismDTO dto)
        {
            var entity = await _context.Trophisms.FindAsync(dto.TrophismId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Trophisms.FindAsync(id);
            if (entity == null) return false;

            _context.Trophisms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private TrophismDTO MapToDto(Trophism entity)
        {
            return new TrophismDTO
            {
                TrophismId = entity.TrophismId,
                Name = entity.Name
            };
        }

        private Trophism MapToEntity(TrophismDTO dto)
        {
            return new Trophism
            {
                TrophismId = dto.TrophismId,
                Name = dto.Name
            };
        }
    }
}
