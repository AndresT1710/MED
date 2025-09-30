using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PhysiotherapyTreatmentRepository : IRepository<PhysiotherapyTreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public PhysiotherapyTreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PhysiotherapyTreatmentDTO>> GetAllAsync()
        {
            var entities = await _context.PhysiotherapyTreatments.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PhysiotherapyTreatmentDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PhysiotherapyTreatments.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PhysiotherapyTreatmentDTO> AddAsync(PhysiotherapyTreatmentDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PhysiotherapyTreatments.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PhysiotherapyTreatmentDTO?> UpdateAsync(PhysiotherapyTreatmentDTO dto)
        {
            var entity = await _context.PhysiotherapyTreatments.FindAsync(dto.PhysiotherapyTreatmentId);
            if (entity == null) return null;

            entity.Recommendations = dto.Recommendations ?? entity.Recommendations;
            entity.PhysiotherapyDiagnosticId = dto.PhysiotherapyDiagnosticId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PhysiotherapyTreatments.FindAsync(id);
            if (entity == null) return false;

            _context.PhysiotherapyTreatments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping
        private PhysiotherapyTreatmentDTO MapToDto(PhysiotherapyTreatment entity) => new PhysiotherapyTreatmentDTO
        {
            PhysiotherapyTreatmentId = entity.PhysiotherapyTreatmentId,
            Recommendations = entity.Recommendations,
            PhysiotherapyDiagnosticId = entity.PhysiotherapyDiagnosticId
        };

        private PhysiotherapyTreatment MapToEntity(PhysiotherapyTreatmentDTO dto) => new PhysiotherapyTreatment
        {
            PhysiotherapyTreatmentId = dto.PhysiotherapyTreatmentId,
            Recommendations = dto.Recommendations ?? string.Empty,
            PhysiotherapyDiagnosticId = dto.PhysiotherapyDiagnosticId
        };
    }
}
