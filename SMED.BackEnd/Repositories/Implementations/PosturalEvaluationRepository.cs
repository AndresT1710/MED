using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PosturalEvaluationRepository : IRepository<PosturalEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public PosturalEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PosturalEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.PosturalEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PosturalEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PosturalEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PosturalEvaluationDTO> AddAsync(PosturalEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PosturalEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

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
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PosturalEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.PosturalEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private PosturalEvaluationDTO MapToDto(PosturalEvaluation entity) => new PosturalEvaluationDTO
        {
            PosturalEvaluationId = entity.PosturalEvaluationId,
            Observation = entity.Observation,
            Grade = entity.Grade,
            BodyAlignment = entity.BodyAlignment,
            MedicalCareId = entity.MedicalCareId,
            ViewId = entity.ViewId
        };

        private PosturalEvaluation MapToEntity(PosturalEvaluationDTO dto) => new PosturalEvaluation
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
