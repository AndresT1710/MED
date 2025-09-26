using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class BodyZoneRepository : IRepository<BodyZoneDTO, int>
    {
        private readonly SGISContext _context;

        public BodyZoneRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<BodyZoneDTO>> GetAllAsync()
        {
            var entities = await _context.BodyZones.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<BodyZoneDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.BodyZones.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<BodyZoneDTO> AddAsync(BodyZoneDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.BodyZones.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<BodyZoneDTO?> UpdateAsync(BodyZoneDTO dto)
        {
            var entity = await _context.BodyZones.FindAsync(dto.BodyZoneId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BodyZones.FindAsync(id);
            if (entity == null) return false;

            _context.BodyZones.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private BodyZoneDTO MapToDto(BodyZone entity)
        {
            return new BodyZoneDTO
            {
                BodyZoneId = entity.BodyZoneId,
                Name = entity.Name
            };
        }

        private BodyZone MapToEntity(BodyZoneDTO dto)
        {
            return new BodyZone
            {
                BodyZoneId = dto.BodyZoneId,
                Name = dto.Name
            };
        }
    }
}
