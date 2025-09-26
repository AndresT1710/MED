using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class OsteoarticularEvaluationRepository : IRepository<OsteoarticularEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public OsteoarticularEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<OsteoarticularEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.OsteoarticularEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<OsteoarticularEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.OsteoarticularEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<OsteoarticularEvaluationDTO> AddAsync(OsteoarticularEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.OsteoarticularEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<OsteoarticularEvaluationDTO?> UpdateAsync(OsteoarticularEvaluationDTO dto)
        {
            var entity = await _context.OsteoarticularEvaluations.FindAsync(dto.OsteoarticularEvaluationId);
            if (entity == null) return null;

            entity.JointConditionId = dto.JointConditionId;
            entity.JointRangeOfMotionId = dto.JointRangeOfMotionId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OsteoarticularEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.OsteoarticularEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private OsteoarticularEvaluationDTO MapToDto(OsteoarticularEvaluation entity)
        {
            return new OsteoarticularEvaluationDTO
            {
                OsteoarticularEvaluationId = entity.OsteoarticularEvaluationId,
                JointConditionId = entity.JointConditionId,
                JointRangeOfMotionId = entity.JointRangeOfMotionId,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private OsteoarticularEvaluation MapToEntity(OsteoarticularEvaluationDTO dto)
        {
            return new OsteoarticularEvaluation
            {
                OsteoarticularEvaluationId = dto.OsteoarticularEvaluationId,
                JointConditionId = dto.JointConditionId,
                JointRangeOfMotionId = dto.JointRangeOfMotionId,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}
