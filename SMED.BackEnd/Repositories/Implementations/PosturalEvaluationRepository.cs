using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PosturalEvaluationRepository : IRepository<PosturalEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public PosturalEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        // ====================================
        // 🔹 GET ALL
        // ====================================
        public async Task<List<PosturalEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.PosturalEvaluations
                .Include(x => x.View)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 GET BY ID
        // ====================================
        public async Task<PosturalEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PosturalEvaluations
                .Include(x => x.View)
                .FirstOrDefaultAsync(x => x.PosturalEvaluationId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ====================================
        // 🔹 ADD
        // ====================================
        public async Task<PosturalEvaluationDTO> AddAsync(PosturalEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PosturalEvaluations.Add(entity);
            await _context.SaveChangesAsync();

            // Retorna con nombre de View incluido
            return await GetByIdAsync(entity.PosturalEvaluationId) ?? MapToDto(entity);
        }

        // ====================================
        // 🔹 UPDATE
        // ====================================
        public async Task<PosturalEvaluationDTO?> UpdateAsync(PosturalEvaluationDTO dto)
        {
            var entity = await _context.PosturalEvaluations.FindAsync(dto.PosturalEvaluationId);
            if (entity == null) return null;

            entity.Observation = dto.Observation;
            entity.Grade = dto.Grade;
            entity.BodyAlignment = dto.BodyAlignment;
            entity.MedicalCareId = dto.MedicalCareId;
            entity.ViewId = dto.ViewId;

            await _context.SaveChangesAsync();

            // Retorna el DTO actualizado con relaciones
            return await GetByIdAsync(entity.PosturalEvaluationId);
        }

        // ====================================
        // 🔹 DELETE
        // ====================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PosturalEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.PosturalEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PosturalEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.PosturalEvaluations
                .Include(x => x.View)
                .Where(x => x.MedicalCareId == medicalCareId)
                .OrderByDescending(x => x.PosturalEvaluationId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 MAPEO ENTITY → DTO
        // ====================================
        private PosturalEvaluationDTO MapToDto(PosturalEvaluation entity)
        {
            return new PosturalEvaluationDTO
            {
                PosturalEvaluationId = entity.PosturalEvaluationId,
                Observation = entity.Observation,
                Grade = entity.Grade,
                BodyAlignment = entity.BodyAlignment,
                MedicalCareId = entity.MedicalCareId,
                ViewId = entity.ViewId,
                ViewName = entity.View?.Name
            };
        }

        // ====================================
        // 🔹 MAPEO DTO → ENTITY
        // ====================================
        private PosturalEvaluation MapToEntity(PosturalEvaluationDTO dto)
        {
            return new PosturalEvaluation
            {
                PosturalEvaluationId = dto.PosturalEvaluationId,
                Observation = dto.Observation,
                Grade = dto.Grade,
                BodyAlignment = dto.BodyAlignment,
                MedicalCareId = dto.MedicalCareId,
                ViewId = dto.ViewId
            };
        }
    }
}
