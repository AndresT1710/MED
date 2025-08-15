using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PharmacologicalTreatmentRepository : IRepository<PharmacologicalTreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public PharmacologicalTreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PharmacologicalTreatmentDTO>> GetAllAsync()
        {
            var treatments = await _context.PharmacologicalTreatments.ToListAsync();
            return treatments.Select(MapToDto).ToList();
        }

        public async Task<PharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.PharmacologicalTreatments.FindAsync(id);
            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<PharmacologicalTreatmentDTO> AddAsync(PharmacologicalTreatmentDTO dto)
        {
            var entity = new PharmacologicalTreatment
            {
                Dose = dto.Dose,
                Frequency = dto.Frequency,
                Duration = dto.Duration,
                ViaAdmission = dto.ViaAdmission,
                MedicineId = dto.MedicineId
            };

            _context.PharmacologicalTreatments.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PharmacologicalTreatmentDTO?> UpdateAsync(PharmacologicalTreatmentDTO dto)
        {
            var entity = await _context.PharmacologicalTreatments.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Dose = dto.Dose;
            entity.Frequency = dto.Frequency;
            entity.Duration = dto.Duration;
            entity.ViaAdmission = dto.ViaAdmission;
            entity.MedicineId = dto.MedicineId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PharmacologicalTreatments.FindAsync(id);
            if (entity == null) return false;

            _context.PharmacologicalTreatments.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static PharmacologicalTreatmentDTO MapToDto(PharmacologicalTreatment treatment) => new PharmacologicalTreatmentDTO
        {
            Id = treatment.Id,
            Dose = treatment.Dose,
            Frequency = treatment.Frequency,
            Duration = treatment.Duration,
            ViaAdmission = treatment.ViaAdmission,
            MedicineId = treatment.MedicineId
        };
    }

}
