using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SwellingRepository : IRepository<SwellingDTO, int>
    {
        private readonly SGISContext _context;

        public SwellingRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SwellingDTO>> GetAllAsync()
        {
            var entities = await _context.Swellings.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SwellingDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Swellings.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SwellingDTO> AddAsync(SwellingDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Swellings.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SwellingDTO?> UpdateAsync(SwellingDTO dto)
        {
            var entity = await _context.Swellings.FindAsync(dto.SwellingId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Swellings.FindAsync(id);
            if (entity == null) return false;

            _context.Swellings.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private SwellingDTO MapToDto(Swelling entity) => new SwellingDTO
        {
            SwellingId = entity.SwellingId,
            Name = entity.Name
        };

        private Swelling MapToEntity(SwellingDTO dto) => new Swelling
        {
            SwellingId = dto.SwellingId,
            Name = dto.Name
        };
    }
}
