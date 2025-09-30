using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalEvaluationPositionRepository : IRepository<MedicalEvaluationPositionDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalEvaluationPositionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalEvaluationPositionDTO>> GetAllAsync()
        {
            var entities = await _context.MedicalEvaluationPositions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MedicalEvaluationPositionDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalEvaluationPositions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MedicalEvaluationPositionDTO> AddAsync(MedicalEvaluationPositionDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicalEvaluationPositions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MedicalEvaluationPositionDTO?> UpdateAsync(MedicalEvaluationPositionDTO dto)
        {
            var entity = await _context.MedicalEvaluationPositions.FindAsync(dto.MedicalEvaluationPositionId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalEvaluationPositions.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalEvaluationPositions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private MedicalEvaluationPositionDTO MapToDto(MedicalEvaluationPosition entity)
        {
            return new MedicalEvaluationPositionDTO
            {
                MedicalEvaluationPositionId = entity.MedicalEvaluationPositionId,
                Name = entity.Name
            };
        }

        private MedicalEvaluationPosition MapToEntity(MedicalEvaluationPositionDTO dto)
        {
            return new MedicalEvaluationPosition
            {
                MedicalEvaluationPositionId = dto.MedicalEvaluationPositionId,
                Name = dto.Name
            };
        }
    }
}
