using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalEvaluationMembersRepository : IRepository<MedicalEvaluationMembersDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalEvaluationMembersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalEvaluationMembersDTO>> GetAllAsync()
        {
            var entities = await _context.MedicalEvaluationMembers.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MedicalEvaluationMembersDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalEvaluationMembers.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MedicalEvaluationMembersDTO> AddAsync(MedicalEvaluationMembersDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicalEvaluationMembers.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MedicalEvaluationMembersDTO?> UpdateAsync(MedicalEvaluationMembersDTO dto)
        {
            var entity = await _context.MedicalEvaluationMembers.FindAsync(dto.MedicalEvaluationMembersId);
            if (entity == null) return null;

            entity.Name = dto.Name;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalEvaluationMembers.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalEvaluationMembers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private MedicalEvaluationMembersDTO MapToDto(MedicalEvaluationMembers entity)
        {
            return new MedicalEvaluationMembersDTO
            {
                MedicalEvaluationMembersId = entity.MedicalEvaluationMembersId,
                Name = entity.Name
            };
        }

        private MedicalEvaluationMembers MapToEntity(MedicalEvaluationMembersDTO dto)
        {
            return new MedicalEvaluationMembers
            {
                MedicalEvaluationMembersId = dto.MedicalEvaluationMembersId,
                Name = dto.Name
            };
        }
    }
}
