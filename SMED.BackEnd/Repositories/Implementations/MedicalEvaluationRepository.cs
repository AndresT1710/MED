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
    public class MedicalEvaluationRepository : IRepository<MedicalEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        // ===========================================
        // 🔹 Obtener todas las evaluaciones médicas
        // ===========================================
        public async Task<List<MedicalEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.MedicalEvaluations
                .Include(m => m.TypeOfMedicalEvaluation)
                .Include(m => m.MedicalEvaluationPosition)
                .Include(m => m.MedicalEvaluationMembers)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Obtener evaluación médica por ID
        // ===========================================
        public async Task<MedicalEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalEvaluations
                .Include(m => m.TypeOfMedicalEvaluation)
                .Include(m => m.MedicalEvaluationPosition)
                .Include(m => m.MedicalEvaluationMembers)
                .FirstOrDefaultAsync(m => m.MedicalEvaluationId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ===========================================
        // 🔹 Agregar nueva evaluación médica
        // ===========================================
        public async Task<MedicalEvaluationDTO> AddAsync(MedicalEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicalEvaluations.Add(entity);
            await _context.SaveChangesAsync();

            // Cargar relaciones
            await _context.Entry(entity).Reference(m => m.TypeOfMedicalEvaluation).LoadAsync();
            await _context.Entry(entity).Reference(m => m.MedicalEvaluationPosition).LoadAsync();
            await _context.Entry(entity).Reference(m => m.MedicalEvaluationMembers).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Actualizar evaluación médica existente
        // ===========================================
        public async Task<MedicalEvaluationDTO?> UpdateAsync(MedicalEvaluationDTO dto)
        {
            var entity = await _context.MedicalEvaluations.FindAsync(dto.MedicalEvaluationId);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.MedicalCareId = dto.MedicalCareId;
            entity.TypeOfMedicalEvaluationId = dto.TypeOfMedicalEvaluationId;
            entity.MedicalEvaluationPositionId = dto.MedicalEvaluationPositionId;
            entity.MedicalEvaluationMembersId = dto.MedicalEvaluationMembersId;

            await _context.SaveChangesAsync();

            // Recargar relaciones actualizadas
            await _context.Entry(entity).Reference(m => m.TypeOfMedicalEvaluation).LoadAsync();
            await _context.Entry(entity).Reference(m => m.MedicalEvaluationPosition).LoadAsync();
            await _context.Entry(entity).Reference(m => m.MedicalEvaluationMembers).LoadAsync();

            return MapToDto(entity);
        }

        // ===========================================
        // 🔹 Eliminar evaluación médica
        // ===========================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<MedicalEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.MedicalEvaluations
                .Include(m => m.TypeOfMedicalEvaluation)
                .Include(m => m.MedicalEvaluationPosition)
                .Include(m => m.MedicalEvaluationMembers)
                .Where(m => m.MedicalCareId == medicalCareId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ===========================================
        // 🔹 Mapeos DTO ↔ Entity
        // ===========================================
        private MedicalEvaluationDTO MapToDto(MedicalEvaluation entity)
        {
            return new MedicalEvaluationDTO
            {
                MedicalEvaluationId = entity.MedicalEvaluationId,
                Description = entity.Description,
                MedicalCareId = entity.MedicalCareId,

                TypeOfMedicalEvaluationId = entity.TypeOfMedicalEvaluationId,
                TypeOfMedicalEvaluationName = entity.TypeOfMedicalEvaluation?.Name ?? "No registrado",

                MedicalEvaluationPositionId = entity.MedicalEvaluationPositionId,
                MedicalEvaluationPositionName = entity.MedicalEvaluationPosition?.Name ?? "No registrado",

                MedicalEvaluationMembersId = entity.MedicalEvaluationMembersId,
                MedicalEvaluationMembersName = entity.MedicalEvaluationMembers?.Name ?? "No registrado"
            };
        }


        private MedicalEvaluation MapToEntity(MedicalEvaluationDTO dto)
        {
            return new MedicalEvaluation
            {
                MedicalEvaluationId = dto.MedicalEvaluationId,
                Description = dto.Description,
                MedicalCareId = dto.MedicalCareId,
                TypeOfMedicalEvaluationId = dto.TypeOfMedicalEvaluationId,
                MedicalEvaluationPositionId = dto.MedicalEvaluationPositionId,
                MedicalEvaluationMembersId = dto.MedicalEvaluationMembersId
            };
        }
    }
}
