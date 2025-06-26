using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiagnosisTreatmentRepository : IRepository<DiagnosisTreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public DiagnosisTreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiagnosisTreatmentDTO>> GetAllAsync()
        {
            var diagnosisTreatments = await _context.DiagnosisTreatments.ToListAsync();
            return diagnosisTreatments.Select(MapToDto).ToList();
        }

        public async Task<DiagnosisTreatmentDTO?> GetByIdAsync(int id)
        {
            var diagnosisTreatment = await _context.DiagnosisTreatments.FindAsync(id);
            return diagnosisTreatment == null ? null : MapToDto(diagnosisTreatment);
        }

        public async Task<DiagnosisTreatmentDTO> AddAsync(DiagnosisTreatmentDTO dto)
        {
            var entity = new DiagnosisTreatment
            {
                DiagnosisId = dto.DiagnosisId,
                TreatmentId = dto.TreatmentId
            };

            _context.DiagnosisTreatments.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<DiagnosisTreatmentDTO?> UpdateAsync(DiagnosisTreatmentDTO dto)
        {
            var entity = await _context.DiagnosisTreatments.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.DiagnosisId = dto.DiagnosisId;
            entity.TreatmentId = dto.TreatmentId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DiagnosisTreatments.FindAsync(id);
            if (entity == null) return false;

            _context.DiagnosisTreatments.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static DiagnosisTreatmentDTO MapToDto(DiagnosisTreatment diagnosisTreatment) => new DiagnosisTreatmentDTO
        {
            Id = diagnosisTreatment.Id,
            DiagnosisId = diagnosisTreatment.DiagnosisId,
            TreatmentId = diagnosisTreatment.TreatmentId
        };
    }

}
