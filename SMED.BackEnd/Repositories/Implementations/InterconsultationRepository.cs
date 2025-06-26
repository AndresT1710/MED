using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class InterconsultationRepository : IRepository<InterconsultationDTO, int>
    {
        private readonly SGISContext _context;

        public InterconsultationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<InterconsultationDTO>> GetAllAsync()
        {
            var interconsultations = await _context.Interconsultations.ToListAsync();
            return interconsultations.Select(MapToDto).ToList();
        }

        public async Task<InterconsultationDTO?> GetByIdAsync(int id)
        {
            var interconsultation = await _context.Interconsultations.FindAsync(id);
            return interconsultation == null ? null : MapToDto(interconsultation);
        }

        public async Task<InterconsultationDTO> AddAsync(InterconsultationDTO dto)
        {
            var entity = new Interconsultation
            {
                InterconsultationDate = dto.InterconsultationDate,
                Reason = dto.Reason,
                DiagnosisId = dto.DiagnosisId
            };

            _context.Interconsultations.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<InterconsultationDTO?> UpdateAsync(InterconsultationDTO dto)
        {
            var entity = await _context.Interconsultations.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.InterconsultationDate = dto.InterconsultationDate;
            entity.Reason = dto.Reason;
            entity.DiagnosisId = dto.DiagnosisId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Interconsultations.FindAsync(id);
            if (entity == null) return false;

            _context.Interconsultations.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static InterconsultationDTO MapToDto(Interconsultation interconsultation) => new InterconsultationDTO
        {
            Id = interconsultation.Id,
            InterconsultationDate = interconsultation.InterconsultationDate,
            Reason = interconsultation.Reason,
            DiagnosisId = interconsultation.DiagnosisId
        };
    }

}
