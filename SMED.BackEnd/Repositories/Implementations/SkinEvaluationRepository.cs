using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SkinEvaluationRepository : IRepository<SkinEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public SkinEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SkinEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.SkinEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SkinEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SkinEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SkinEvaluationDTO> AddAsync(SkinEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SkinEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SkinEvaluationDTO?> UpdateAsync(SkinEvaluationDTO dto)
        {
            var entity = await _context.SkinEvaluations.FindAsync(dto.SkinEvaluationId);
            if (entity == null) return null;

            entity.MedicalCareId = dto.MedicalCareId;
            entity.ColorId = dto.ColorId;
            entity.EdemaId = dto.EdemaId;
            entity.StatusId = dto.StatusId;
            entity.SwellingId = dto.SwellingId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SkinEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.SkinEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private SkinEvaluationDTO MapToDto(SkinEvaluation entity)
        {
            return new SkinEvaluationDTO
            {
                SkinEvaluationId = entity.SkinEvaluationId,
                MedicalCareId = entity.MedicalCareId,
                ColorId = entity.ColorId,
                EdemaId = entity.EdemaId,
                StatusId = entity.StatusId,
                SwellingId = entity.SwellingId
            };
        }

        private SkinEvaluation MapToEntity(SkinEvaluationDTO dto)
        {
            return new SkinEvaluation
            {
                SkinEvaluationId = dto.SkinEvaluationId,
                MedicalCareId = dto.MedicalCareId,
                ColorId = dto.ColorId,
                EdemaId = dto.EdemaId,
                StatusId = dto.StatusId,
                SwellingId = dto.SwellingId
            };
        }
    }
}
