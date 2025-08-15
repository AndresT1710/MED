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
            var treatments = await _context.PharmacologicalTreatments
                .Include(pt => pt.Medicine)
                .Include(pt => pt.MedicalDiagnosis)
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        public async Task<PharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            var treatment = await _context.PharmacologicalTreatments
                .Include(pt => pt.Medicine)
                .Include(pt => pt.MedicalDiagnosis)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            return treatment == null ? null : MapToDto(treatment);
        }

        public async Task<PharmacologicalTreatmentDTO> AddAsync(PharmacologicalTreatmentDTO dto)
        {
            // ✅ Validar que el diagnóstico existe
            var diagnosisExists = await _context.Diagnosis
                .AnyAsync(d => d.Id == dto.MedicalDiagnosisId);

            if (!diagnosisExists)
            {
                throw new InvalidOperationException($"El diagnóstico médico con ID {dto.MedicalDiagnosisId} no existe.");
            }

            // ✅ Validar que el medicamento existe
            var medicineExists = await _context.Medicines
                .AnyAsync(m => m.Id == dto.MedicineId);

            if (!medicineExists)
            {
                throw new InvalidOperationException($"El medicamento con ID {dto.MedicineId} no existe.");
            }

            var entity = new PharmacologicalTreatment
            {
                MedicalDiagnosisId = dto.MedicalDiagnosisId,
                Description = dto.Description ?? $"Tratamiento farmacológico - {DateTime.Now:yyyy-MM-dd}",
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
            var entity = await _context.PharmacologicalTreatments
                .FirstOrDefaultAsync(pt => pt.Id == dto.Id);

            if (entity == null) return null;

            // ✅ Validar que el diagnóstico existe
            var diagnosisExists = await _context.Diagnosis
                .AnyAsync(d => d.Id == dto.MedicalDiagnosisId);

            if (!diagnosisExists)
            {
                throw new InvalidOperationException($"El diagnóstico médico con ID {dto.MedicalDiagnosisId} no existe.");
            }

            // ✅ Validar que el medicamento existe
            var medicineExists = await _context.Medicines
                .AnyAsync(m => m.Id == dto.MedicineId);

            if (!medicineExists)
            {
                throw new InvalidOperationException($"El medicamento con ID {dto.MedicineId} no existe.");
            }

            entity.MedicalDiagnosisId = dto.MedicalDiagnosisId;
            entity.Description = dto.Description;
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
            var entity = await _context.PharmacologicalTreatments
                .Include(pt => pt.Indications)
                .FirstOrDefaultAsync(pt => pt.Id == id);

            if (entity == null) return false;

            // Eliminar indicaciones primero
            _context.Indications.RemoveRange(entity.Indications);

            // Eliminar el tratamiento farmacológico
            _context.PharmacologicalTreatments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Método para obtener tratamientos farmacológicos por diagnóstico
        public async Task<List<PharmacologicalTreatmentDTO>> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            var treatments = await _context.PharmacologicalTreatments
                .Include(pt => pt.Medicine)
                .Include(pt => pt.MedicalDiagnosis)
                .Where(pt => pt.MedicalDiagnosisId == medicalDiagnosisId)
                .ToListAsync();

            return treatments.Select(MapToDto).ToList();
        }

        private static PharmacologicalTreatmentDTO MapToDto(PharmacologicalTreatment treatment) => new PharmacologicalTreatmentDTO
        {
            Id = treatment.Id,
            MedicalDiagnosisId = treatment.MedicalDiagnosisId,
            Description = treatment.Description,
            Dose = treatment.Dose,
            Frequency = treatment.Frequency,
            Duration = treatment.Duration,
            ViaAdmission = treatment.ViaAdmission,
            MedicineId = treatment.MedicineId,
            MedicineName = treatment.Medicine?.Name
        };
    }
}
