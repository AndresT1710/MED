using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class JointRangeOfMotionRepository : IRepository<JointRangeOfMotionDTO, int>
    {
        private readonly SGISContext _context;

        public JointRangeOfMotionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<JointRangeOfMotionDTO>> GetAllAsync()
        {
            var entities = await _context.JointRangeOfMotions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<JointRangeOfMotionDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.JointRangeOfMotions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<JointRangeOfMotionDTO> AddAsync(JointRangeOfMotionDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.JointRangeOfMotions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<JointRangeOfMotionDTO?> UpdateAsync(JointRangeOfMotionDTO dto)
        {
            var entity = await _context.JointRangeOfMotions.FindAsync(dto.JointRangeOfMotionId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.JointRangeOfMotions.FindAsync(id);
            if (entity == null) return false;

            _context.JointRangeOfMotions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private JointRangeOfMotionDTO MapToDto(JointRangeOfMotion entity)
        {
            return new JointRangeOfMotionDTO
            {
                JointRangeOfMotionId = entity.JointRangeOfMotionId,
                Name = entity.Name
            };
        }

        private JointRangeOfMotion MapToEntity(JointRangeOfMotionDTO dto)
        {
            return new JointRangeOfMotion
            {
                JointRangeOfMotionId = dto.JointRangeOfMotionId,
                Name = dto.Name
            };
        }
    }
}
