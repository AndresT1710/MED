using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MaritalStatusRepository : IRepository<MaritalStatusDTO, int>
    {
        private readonly SGISContext _context;

        public MaritalStatusRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MaritalStatusDTO>> GetAllAsync()
        {
            var entities = await _context.MaritalStatuses.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MaritalStatusDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MaritalStatuses.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MaritalStatusDTO> AddAsync(MaritalStatusDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MaritalStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MaritalStatusDTO?> UpdateAsync(MaritalStatusDTO dto)
        {
            var entity = await _context.MaritalStatuses.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MaritalStatuses.FindAsync(id);
            if (entity == null) return false;

            _context.MaritalStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private MaritalStatusDTO MapToDto(MaritalStatus entity)
        {
            return new MaritalStatusDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        private MaritalStatus MapToEntity(MaritalStatusDTO dto)
        {
            return new MaritalStatus
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}