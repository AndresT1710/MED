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
            var treatments = await _context.NonPharmacologicalTreatments
                .Include(npt => npt.MedicalDiagnosis)
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        public async Task<NonPharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.NonPharmacologicalTreatments
                .Include(npt => npt.MedicalDiagnosis)
                .FirstOrDefaultAsync(npt => npt.Id == id);

            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<NonPharmacologicalTreatmentDTO> AddAsync(NonPharmacologicalTreatmentDTO dto)
        {
            // ✅ Validar que el diagnóstico existe
            var diagnosisExists = await _context.Diagnosis
                .AnyAsync(d => d.Id == dto.MedicalDiagnosisId);

            if (!diagnosisExists)
            {
                throw new InvalidOperationException($"El diagnóstico médico con ID {dto.MedicalDiagnosisId} no existe.");
            }

            var entity = new Non_PharmacologicalTreatment
            {
                MedicalDiagnosisId = dto.MedicalDiagnosisId,
                Description = dto.Description ?? "Tratamiento no farmacológico"
            };

            _context.NonPharmacologicalTreatments.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<NonPharmacologicalTreatmentDTO?> UpdateAsync(NonPharmacologicalTreatmentDTO dto)
        {
            var entity = await _context.NonPharmacologicalTreatments
                .FirstOrDefaultAsync(npt => npt.Id == dto.Id);

            if (entity == null) return null;

            // ✅ Validar que el diagnóstico existe
            var diagnosisExists = await _context.Diagnosis
                .AnyAsync(d => d.Id == dto.MedicalDiagnosisId);

            if (!diagnosisExists)
            {
                throw new InvalidOperationException($"El diagnóstico médico con ID {dto.MedicalDiagnosisId} no existe.");
            }

            entity.MedicalDiagnosisId = dto.MedicalDiagnosisId;
            entity.Description = dto.Description ?? entity.Description;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NonPharmacologicalTreatments
                .Include(npt => npt.Indications)
                .FirstOrDefaultAsync(npt => npt.Id == id);

            if (entity == null) return false;

            // Eliminar indicaciones primero
            _context.Indications.RemoveRange(entity.Indications);
            
            // Eliminar el tratamiento no farmacológico
            _context.NonPharmacologicalTreatments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Método para obtener tratamientos no farmacológicos por diagnóstico
        public async Task<List<NonPharmacologicalTreatmentDTO>> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            var treatments = await _context.NonPharmacologicalTreatments
                .Include(npt => npt.MedicalDiagnosis)
                .Where(npt => npt.MedicalDiagnosisId == medicalDiagnosisId)
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        private static NonPharmacologicalTreatmentDTO MapToDto(Non_PharmacologicalTreatment treatment) => new NonPharmacologicalTreatmentDTO
        {
            Id = treatment.Id,
            MedicalDiagnosisId = treatment.MedicalDiagnosisId,
            Description = treatment.Description
        };
    }
}
