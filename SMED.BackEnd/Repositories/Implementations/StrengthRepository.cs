using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class StrengthRepository : IRepository<StrengthDTO, int>
    {
        private readonly SGISContext _context;

        public StrengthRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<StrengthDTO>> GetAllAsync()
        {
            var entities = await _context.Strengths.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<StrengthDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Strengths.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<StrengthDTO> AddAsync(StrengthDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Strengths.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<StrengthDTO?> UpdateAsync(StrengthDTO dto)
        {
            var entity = await _context.Strengths.FindAsync(dto.StrengthId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Strengths.FindAsync(id);
            if (entity == null) return false;

            _context.Strengths.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private StrengthDTO MapToDto(Strength entity)
        {
            return new StrengthDTO
            {
                StrengthId = entity.StrengthId,
                Name = entity.Name
            };
        }

        private Strength MapToEntity(StrengthDTO dto)
        {
            return new Strength
            {
                StrengthId = dto.StrengthId,
                Name = dto.Name
            };
        }
    }
}
