using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NeuromuscularEvaluationRepository : IRepository<NeuromuscularEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public NeuromuscularEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NeuromuscularEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.NeuromuscularEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<NeuromuscularEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeuromuscularEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<NeuromuscularEvaluationDTO> AddAsync(NeuromuscularEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeuromuscularEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<NeuromuscularEvaluationDTO?> UpdateAsync(NeuromuscularEvaluationDTO dto)
        {
            var entity = await _context.NeuromuscularEvaluations.FindAsync(dto.NeuromuscularEvaluationId);
            if (entity == null) return null;

            entity.ShadeId = dto.ShadeId;
            entity.StrengthId = dto.StrengthId;
            entity.TrophismId = dto.TrophismId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeuromuscularEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.NeuromuscularEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private NeuromuscularEvaluationDTO MapToDto(NeuromuscularEvaluation entity)
        {
            return new NeuromuscularEvaluationDTO
            {
                NeuromuscularEvaluationId = entity.NeuromuscularEvaluationId,
                ShadeId = entity.ShadeId,
                StrengthId = entity.StrengthId,
                TrophismId = entity.TrophismId,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private NeuromuscularEvaluation MapToEntity(NeuromuscularEvaluationDTO dto)
        {
            return new NeuromuscularEvaluation
            {
                NeuromuscularEvaluationId = dto.NeuromuscularEvaluationId,
                ShadeId = dto.ShadeId,
                StrengthId = dto.StrengthId,
                TrophismId = dto.TrophismId,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}
