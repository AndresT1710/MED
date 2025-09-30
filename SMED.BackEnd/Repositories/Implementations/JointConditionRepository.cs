using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class JointConditionRepository : IRepository<JointConditionDTO, int>
    {
        private readonly SGISContext _context;

        public JointConditionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<JointConditionDTO>> GetAllAsync()
        {
            var entities = await _context.JointConditions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<JointConditionDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.JointConditions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<JointConditionDTO> AddAsync(JointConditionDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.JointConditions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<JointConditionDTO?> UpdateAsync(JointConditionDTO dto)
        {
            var entity = await _context.JointConditions.FindAsync(dto.JointConditionId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.JointConditions.FindAsync(id);
            if (entity == null) return false;

            _context.JointConditions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private JointConditionDTO MapToDto(JointCondition entity)
        {
            return new JointConditionDTO
            {
                JointConditionId = entity.JointConditionId,
                Name = entity.Name
            };
        }

        private JointCondition MapToEntity(JointConditionDTO dto)
        {
            return new JointCondition
            {
                JointConditionId = dto.JointConditionId,
                Name = dto.Name
            };
        }
    }
}
