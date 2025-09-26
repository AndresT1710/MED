using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SensitivityEvaluationRepository : IRepository<SensitivityEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public SensitivityEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SensitivityEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.SensitivityEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SensitivityEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SensitivityEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SensitivityEvaluationDTO> AddAsync(SensitivityEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SensitivityEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SensitivityEvaluationDTO?> UpdateAsync(SensitivityEvaluationDTO dto)
        {
            var entity = await _context.SensitivityEvaluations.FindAsync(dto.SensitivityEvaluationId);
            if (entity == null) return null;

            entity.Demandmas = dto.Demandmas;
            entity.SensitivityLevelId = dto.SensitivityLevelId;
            entity.BodyZoneId = dto.BodyZoneId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SensitivityEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.SensitivityEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private SensitivityEvaluationDTO MapToDto(SensitivityEvaluation entity)
        {
            return new SensitivityEvaluationDTO
            {
                SensitivityEvaluationId = entity.SensitivityEvaluationId,
                Demandmas = entity.Demandmas,
                SensitivityLevelId = entity.SensitivityLevelId,
                BodyZoneId = entity.BodyZoneId
            };
        }

        private SensitivityEvaluation MapToEntity(SensitivityEvaluationDTO dto)
        {
            return new SensitivityEvaluation
            {
                SensitivityEvaluationId = dto.SensitivityEvaluationId,
                Demandmas = dto.Demandmas,
                SensitivityLevelId = dto.SensitivityLevelId,
                BodyZoneId = dto.BodyZoneId
            };
        }
    }
}
