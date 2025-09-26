using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalEvaluationRepository : IRepository<MedicalEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.MedicalEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MedicalEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MedicalEvaluationDTO> AddAsync(MedicalEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicalEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MedicalEvaluationDTO?> UpdateAsync(MedicalEvaluationDTO dto)
        {
            var entity = await _context.MedicalEvaluations.FindAsync(dto.MedicalEvaluationId);
            if (entity == null) return null;

            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private MedicalEvaluationDTO MapToDto(MedicalEvaluation entity)
        {
            return new MedicalEvaluationDTO
            {
                MedicalEvaluationId = entity.MedicalEvaluationId,
                Description = entity.Description
            };
        }

        private MedicalEvaluation MapToEntity(MedicalEvaluationDTO dto)
        {
            return new MedicalEvaluation
            {
                MedicalEvaluationId = dto.MedicalEvaluationId,
                Description = dto.Description
            };
        }
    }
}
