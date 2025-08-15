using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NonPharmacologicalTreatmentRepository : IRepository<NonPharmacologicalTreatmentDTO, int>
    {
        private readonly SGISContext _context;

        public NonPharmacologicalTreatmentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NonPharmacologicalTreatmentDTO>> GetAllAsync()
        {
            var treatments = await _context.NonPharmacologicalTreatments.ToListAsync();
            return treatments.Select(MapToDto).ToList();
        }

        public async Task<NonPharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.NonPharmacologicalTreatments.FindAsync(id);
            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<NonPharmacologicalTreatmentDTO> AddAsync(NonPharmacologicalTreatmentDTO dto)
        {
            var entity = new Non_PharmacologicalTreatment
            {
                Description = dto.Description
            };

            _context.NonPharmacologicalTreatments.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<NonPharmacologicalTreatmentDTO?> UpdateAsync(NonPharmacologicalTreatmentDTO dto)
        {
            var entity = await _context.NonPharmacologicalTreatments.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NonPharmacologicalTreatments.FindAsync(id);
            if (entity == null) return false;

            _context.NonPharmacologicalTreatments.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static NonPharmacologicalTreatmentDTO MapToDto(Non_PharmacologicalTreatment treatment) => new NonPharmacologicalTreatmentDTO
        {
            Id = treatment.Id,
            Description = treatment.Description
        };
    }

}
