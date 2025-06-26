using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ReasonForConsultationRepository : IRepository<ReasonForConsultationDTO, int>
    {
        private readonly SGISContext _context;

        public ReasonForConsultationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ReasonForConsultationDTO>> GetAllAsync()
        {
            var reasons = await _context.ReasonForConsultations.ToListAsync();
            return reasons.Select(MapToDto).ToList();
        }

        public async Task<ReasonForConsultationDTO?> GetByIdAsync(int id)
        {
            var reason = await _context.ReasonForConsultations.FindAsync(id);
            return reason == null ? null : MapToDto(reason);
        }

        public async Task<ReasonForConsultationDTO> AddAsync(ReasonForConsultationDTO dto)
        {
            var entity = new ReasonForConsultation
            {
                Description = dto.Description,
                MedicalCareId = dto.MedicalCareId
            };

            _context.ReasonForConsultations.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ReasonForConsultationDTO?> UpdateAsync(ReasonForConsultationDTO dto)
        {
            var entity = await _context.ReasonForConsultations.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ReasonForConsultations.FindAsync(id);
            if (entity == null) return false;

            _context.ReasonForConsultations.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ReasonForConsultationDTO MapToDto(ReasonForConsultation reason) => new ReasonForConsultationDTO
        {
            Id = reason.Id,
            Description = reason.Description,
            MedicalCareId = reason.MedicalCareId
        };
    }

}
