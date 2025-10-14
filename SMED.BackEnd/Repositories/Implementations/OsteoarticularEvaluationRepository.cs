using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class OsteoarticularEvaluationRepository : IRepository<OsteoarticularEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public OsteoarticularEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        // ===========================================
        // 🔹 Obtener todas las evaluaciones osteoarticulares
        // ===========================================
        public async Task<List<OsteoarticularEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.OsteoarticularEvaluations
                .Include(o => o.JointCondition)
                .Include(o => o.JointRangeOfMotion)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Obtener una evaluación por ID
        // ===========================================
        public async Task<OsteoarticularEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.OsteoarticularEvaluations
                .Include(o => o.JointCondition)
                .Include(o => o.JointRangeOfMotion)
                .FirstOrDefaultAsync(o => o.OsteoarticularEvaluationId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ===========================================
        // 🔹 Agregar una nueva evaluación
        // ===========================================
        public async Task<OsteoarticularEvaluationDTO> AddAsync(OsteoarticularEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.OsteoarticularEvaluations.Add(entity);
            await _context.SaveChangesAsync();

            // Cargar relaciones
            await _context.Entry(entity).Reference(o => o.JointCondition).LoadAsync();
            await _context.Entry(entity).Reference(o => o.JointRangeOfMotion).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Actualizar una evaluación existente
        // ===========================================
        public async Task<OsteoarticularEvaluationDTO?> UpdateAsync(OsteoarticularEvaluationDTO dto)
        {
            var entity = await _context.OsteoarticularEvaluations.FindAsync(dto.OsteoarticularEvaluationId);
            if (entity == null) return null;

            entity.JointConditionId = dto.JointConditionId;
            entity.JointRangeOfMotionId = dto.JointRangeOfMotionId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();

            // Recargar relaciones para reflejar nombres actualizados
            await _context.Entry(entity).Reference(o => o.JointCondition).LoadAsync();
            await _context.Entry(entity).Reference(o => o.JointRangeOfMotion).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Eliminar una evaluación
        // ===========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OsteoarticularEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.OsteoarticularEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OsteoarticularEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.OsteoarticularEvaluations
                .Include(o => o.JointCondition)
                .Include(o => o.JointRangeOfMotion)
                .Where(o => o.MedicalCareId == medicalCareId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Mapeos DTO ↔ Entity
        // ===========================================
        private OsteoarticularEvaluationDTO MapToDto(OsteoarticularEvaluation entity)
        {
            return new OsteoarticularEvaluationDTO
            {
                OsteoarticularEvaluationId = entity.OsteoarticularEvaluationId,
                JointConditionId = entity.JointConditionId,
                JointConditionName = entity.JointCondition?.Name ?? "No registrado",
                JointRangeOfMotionId = entity.JointRangeOfMotionId,
                JointRangeOfMotionName = entity.JointRangeOfMotion?.Name ?? "No registrado",
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
