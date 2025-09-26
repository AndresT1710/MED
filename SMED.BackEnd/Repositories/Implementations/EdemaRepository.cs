using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class EdemaRepository : IRepository<EdemaDTO, int>
    {
        private readonly SGISContext _context;

        public EdemaRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<EdemaDTO>> GetAllAsync()
        {
            var entities = await _context.Edemas.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<EdemaDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Edemas.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<EdemaDTO> AddAsync(EdemaDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Edemas.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<EdemaDTO?> UpdateAsync(EdemaDTO dto)
        {
            var entity = await _context.Edemas.FindAsync(dto.EdemaId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Edemas.FindAsync(id);
            if (entity == null) return false;

            _context.Edemas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private EdemaDTO MapToDto(Edema entity) => new EdemaDTO
        {
            EdemaId = entity.EdemaId,
            Name = entity.Name
        };

        private Edema MapToEntity(EdemaDTO dto) => new Edema
        {
            EdemaId = dto.EdemaId,
            Name = dto.Name
        };
    }
}
