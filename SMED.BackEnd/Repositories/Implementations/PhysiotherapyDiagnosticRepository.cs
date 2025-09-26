using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PhysiotherapyDiagnosticRepository : IRepository<PhysiotherapyDiagnosticDTO, int>
    {
        private readonly SGISContext _context;

        public PhysiotherapyDiagnosticRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PhysiotherapyDiagnosticDTO>> GetAllAsync()
        {
            var entities = await _context.PhysiotherapyDiagnostics.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PhysiotherapyDiagnosticDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PhysiotherapyDiagnostics.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PhysiotherapyDiagnosticDTO> AddAsync(PhysiotherapyDiagnosticDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PhysiotherapyDiagnostics.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PhysiotherapyDiagnosticDTO?> UpdateAsync(PhysiotherapyDiagnosticDTO dto)
        {
            var entity = await _context.PhysiotherapyDiagnostics.FindAsync(dto.PhysiotherapyDiagnosticId);
            if (entity == null) return null;

            entity.Description = dto.Description ?? entity.Description;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PhysiotherapyDiagnostics.FindAsync(id);
            if (entity == null) return false;

            _context.PhysiotherapyDiagnostics.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private PhysiotherapyDiagnosticDTO MapToDto(PhysiotherapyDiagnostic entity) => new PhysiotherapyDiagnosticDTO
        {
            PhysiotherapyDiagnosticId = entity.PhysiotherapyDiagnosticId,
            Description = entity.Description,
            MedicalCareId = entity.MedicalCareId
        };

        private PhysiotherapyDiagnostic MapToEntity(PhysiotherapyDiagnosticDTO dto) => new PhysiotherapyDiagnostic
        {
            PhysiotherapyDiagnosticId = dto.PhysiotherapyDiagnosticId,
            Description = dto.Description ?? string.Empty,
            MedicalCareId = dto.MedicalCareId
        };
    }
}
