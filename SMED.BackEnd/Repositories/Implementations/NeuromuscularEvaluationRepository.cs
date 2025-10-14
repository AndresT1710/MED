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
    public class NeuromuscularEvaluationRepository : IRepository<NeuromuscularEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public NeuromuscularEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        // ====================================
        // 🔹 GET ALL
        // ====================================
        public async Task<List<NeuromuscularEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.NeuromuscularEvaluations
                .Include(x => x.Shade)
                .Include(x => x.Strength)
                .Include(x => x.Trophism)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 GET BY ID
        // ====================================
        public async Task<NeuromuscularEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeuromuscularEvaluations
                .Include(x => x.Shade)
                .Include(x => x.Strength)
                .Include(x => x.Trophism)
                .FirstOrDefaultAsync(x => x.NeuromuscularEvaluationId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        // ====================================
        // 🔹 ADD
        // ====================================
        public async Task<NeuromuscularEvaluationDTO> AddAsync(NeuromuscularEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeuromuscularEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.NeuromuscularEvaluationId) ?? MapToDto(entity);
        }

        // ====================================
        // 🔹 UPDATE
        // ====================================
        public async Task<NeuromuscularEvaluationDTO?> UpdateAsync(NeuromuscularEvaluationDTO dto)
        {
            var entity = await _context.NeuromuscularEvaluations.FindAsync(dto.NeuromuscularEvaluationId);
            if (entity == null) return null;

            entity.ShadeId = dto.ShadeId;
            entity.StrengthId = dto.StrengthId;
            entity.TrophismId = dto.TrophismId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.NeuromuscularEvaluationId);
        }

        // ====================================
        // 🔹 DELETE
        // ====================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeuromuscularEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.NeuromuscularEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<NeuromuscularEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.NeuromuscularEvaluations
                .Include(x => x.Shade)
                .Include(x => x.Strength)
                .Include(x => x.Trophism)
                .Where(x => x.MedicalCareId == medicalCareId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // ====================================
        // 🔹 MAPEO ENTITY → DTO
        // ====================================
        private NeuromuscularEvaluationDTO MapToDto(NeuromuscularEvaluation entity)
        {
            return new NeuromuscularEvaluationDTO
            {
                NeuromuscularEvaluationId = entity.NeuromuscularEvaluationId,
                ShadeId = entity.ShadeId,
                ShadeName = entity.Shade?.Name,
                StrengthId = entity.StrengthId,
                StrengthName = entity.Strength?.Name,
                TrophismId = entity.TrophismId,
                TrophismName = entity.Trophism?.Name,
                MedicalCareId = entity.MedicalCareId
            };
        }

        // ====================================
        // 🔹 MAPEO DTO → ENTITY
        // ====================================
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
