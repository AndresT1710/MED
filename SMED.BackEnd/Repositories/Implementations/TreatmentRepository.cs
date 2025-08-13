using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TreatmentRepository : IRepository<TreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public TreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TreatmentDTO>> GetAllAsync()
        {
            var treatments = await _context.Treatments
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        public async Task<TreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.Treatments
                .FirstOrDefaultAsync(t => t.Id == id);

            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<TreatmentDTO> AddAsync(TreatmentDTO dto)
        {
            var entity = new Treatment
            {
                Recommendations = dto.Recommendations,
                MedicalDiagnosisId = dto.MedicalDiagnosisId,
                Description = dto.Description
            };

            _context.Treatments.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<TreatmentDTO?> UpdateAsync(TreatmentDTO dto)
        {
            var entity = await _context.Treatments
                .FirstOrDefaultAsync(t => t.Id == dto.Id);

            if (entity == null) return null;

            entity.Recommendations = dto.Recommendations;
            entity.MedicalDiagnosisId = dto.MedicalDiagnosisId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Treatments
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null) return false;

            _context.Treatments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        //  Obtener tratamientos por MedicalDiagnosisId
        public async Task<List<TreatmentDTO>> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            var treatments = await _context.Treatments
                .Where(t => t.MedicalDiagnosisId == medicalDiagnosisId)
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        private static TreatmentDTO MapToDto(Treatment treatment) => new TreatmentDTO
        {
            Id = treatment.Id,
            Recommendations = treatment.Recommendations,
            MedicalDiagnosisId = treatment.MedicalDiagnosisId,
            Description = treatment.Description
        };
    }
}
