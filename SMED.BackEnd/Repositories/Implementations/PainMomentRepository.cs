using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PainMomentRepository : IRepository<PainMomentDTO, int>
    {
        private readonly SGISContext _context;

        public PainMomentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PainMomentDTO>> GetAllAsync()
        {
            var entities = await _context.PainMoments.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PainMomentDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PainMoments.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PainMomentDTO> AddAsync(PainMomentDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PainMoments.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PainMomentDTO?> UpdateAsync(PainMomentDTO dto)
        {
            var entity = await _context.PainMoments.FindAsync(dto.PainMomentId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PainMoments.FindAsync(id);
            if (entity == null) return false;

            _context.PainMoments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private PainMomentDTO MapToDto(PainMoment entity) => new PainMomentDTO
        {
            PainMomentId = entity.PainMomentId,
            Name = entity.Name
        };

        private PainMoment MapToEntity(PainMomentDTO dto) => new PainMoment
        {
            PainMomentId = dto.PainMomentId,
            Name = dto.Name
        };
    }
}
