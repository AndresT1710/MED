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
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Description))
                    throw new ArgumentException("La descripción no puede estar vacía");

                if (dto.MedicalCareId <= 0)
                    throw new ArgumentException("MedicalCareId no válido");

                var entity = new ReasonForConsultation
                {
                    Description = dto.Description.Trim(),
                    MedicalCareId = dto.MedicalCareId
                };

                _context.ReasonForConsultations.Add(entity);
                await _context.SaveChangesAsync();

                dto.Id = entity.Id;
                return dto;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Error de base de datos al crear ReasonForConsultation: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear ReasonForConsultation: {ex.Message}", ex);
            }
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

        public async Task<List<ReasonForConsultationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var reasons = await _context.ReasonForConsultations
                .Where(r => r.MedicalCareId == medicalCareId)
                .ToListAsync();

            return reasons.Select(MapToDto).ToList();
        }

        public async Task<ReasonForConsultationDTO?> GetFirstByCareIdAsync(int medicalCareId)
        {
            var reason = await _context.ReasonForConsultations
                .FirstOrDefaultAsync(r => r.MedicalCareId == medicalCareId);

            return reason == null ? null : MapToDto(reason);
        }

        private static ReasonForConsultationDTO MapToDto(ReasonForConsultation reason) => new ReasonForConsultationDTO
        {
            Id = reason.Id,
            Description = reason.Description,
            MedicalCareId = reason.MedicalCareId
        };
    }

}
