using Infrastructure.Models;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SurgeryRepository : IRepository<SurgeryDTO, int>
    {
        private readonly SGISContext _context;

        public SurgeryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SurgeryDTO>> GetAllAsync()
        {
            var list = await _context.Surgeries.ToListAsync();
            return list.Select(MapToDto).ToList();
        }

        public async Task<SurgeryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Surgeries.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SurgeryDTO> AddAsync(SurgeryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Surgeries.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SurgeryDTO?> UpdateAsync(SurgeryDTO dto)
        {
            var entity = await _context.Surgeries.FindAsync(dto.SurgeryId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Surgeries.FindAsync(id);
            if (entity == null) return false;

            _context.Surgeries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SurgeryDTO MapToDto(Surgery entity) => new()
        {
            SurgeryId = entity.SurgeryId,
            Name = entity.Name
        };

        private Surgery MapToEntity(SurgeryDTO dto) => new()
        {
            SurgeryId = dto.SurgeryId,
            Name = dto.Name
        };
    }

}
