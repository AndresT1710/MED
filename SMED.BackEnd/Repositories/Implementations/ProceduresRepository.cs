using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ProceduresRepository : IRepository<ProceduresDTO, int>
    {
        private readonly SGISContext _context;

        public ProceduresRepository(SGISContext context) => _context = context;

        public async Task<List<ProceduresDTO>> GetAllAsync() =>
            await _context.Procedures
                .Select(p => new ProceduresDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    TypeOfProcedureId = p.TypeOfProcedureId,
                    MedicalCareId = p.MedicalCareId
                })
                .ToListAsync();

        public async Task<ProceduresDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Procedures.FindAsync(id);
            return entity == null ? null : new ProceduresDTO
            {
                Id = entity.Id,
                Description = entity.Description,
                TypeOfProcedureId = entity.TypeOfProcedureId,
                MedicalCareId = entity.MedicalCareId
            };
        }

        public async Task<ProceduresDTO> AddAsync(ProceduresDTO dto)
        {
            var entity = new Procedures
            {
                Description = dto.Description,
                TypeOfProcedureId = dto.TypeOfProcedureId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.Procedures.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ProceduresDTO?> UpdateAsync(ProceduresDTO dto)
        {
            var entity = await _context.Procedures.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.TypeOfProcedureId = dto.TypeOfProcedureId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Procedures.FindAsync(id);
            if (entity == null) return false;

            _context.Procedures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
