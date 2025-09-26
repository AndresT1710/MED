using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class StatusRepository : IRepository<StatusDTO, int>
    {
        private readonly SGISContext _context;

        public StatusRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<StatusDTO>> GetAllAsync()
        {
            var entities = await _context.Statuses.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<StatusDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Statuses.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<StatusDTO> AddAsync(StatusDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Statuses.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<StatusDTO?> UpdateAsync(StatusDTO dto)
        {
            var entity = await _context.Statuses.FindAsync(dto.StatusId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Statuses.FindAsync(id);
            if (entity == null) return false;

            _context.Statuses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private StatusDTO MapToDto(Status entity) => new StatusDTO
        {
            StatusId = entity.StatusId,
            Name = entity.Name
        };

        private Status MapToEntity(StatusDTO dto) => new Status
        {
            StatusId = dto.StatusId,
            Name = dto.Name
        };
    }
}
