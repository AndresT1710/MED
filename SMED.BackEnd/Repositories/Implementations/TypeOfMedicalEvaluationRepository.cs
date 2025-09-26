using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TypeOfMedicalEvaluationRepository : IRepository<TypeOfMedicalEvaluationDTO, int>
    {
        private readonly SGISContext _context;

        public TypeOfMedicalEvaluationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TypeOfMedicalEvaluationDTO>> GetAllAsync()
        {
            var entities = await _context.TypeOfMedicalEvaluations.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TypeOfMedicalEvaluationDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TypeOfMedicalEvaluations.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TypeOfMedicalEvaluationDTO> AddAsync(TypeOfMedicalEvaluationDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TypeOfMedicalEvaluations.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TypeOfMedicalEvaluationDTO?> UpdateAsync(TypeOfMedicalEvaluationDTO dto)
        {
            var entity = await _context.TypeOfMedicalEvaluations.FindAsync(dto.TypeOfMedicalEvaluationId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TypeOfMedicalEvaluations.FindAsync(id);
            if (entity == null) return false;

            _context.TypeOfMedicalEvaluations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private TypeOfMedicalEvaluationDTO MapToDto(TypeOfMedicalEvaluation entity)
        {
            return new TypeOfMedicalEvaluationDTO
            {
                TypeOfMedicalEvaluationId = entity.TypeOfMedicalEvaluationId,
                Name = entity.Name
            };
        }

        private TypeOfMedicalEvaluation MapToEntity(TypeOfMedicalEvaluationDTO dto)
        {
            return new TypeOfMedicalEvaluation
            {
                TypeOfMedicalEvaluationId = dto.TypeOfMedicalEvaluationId,
                Name = dto.Name
            };
        }
    }
}
