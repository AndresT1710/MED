using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalDiagnosisRepository : IRepository<MedicalDiagnosisDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalDiagnosisRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalDiagnosisDTO>> GetAllAsync()
        {
            var diagnoses = await _context.Diagnosis
                .Include(d => d.Treatments)
                .Include(d => d.DiagnosticTypeNavigation)
                .ToListAsync();

            return diagnoses.Select(MapToDto).ToList();
        }

        public async Task<MedicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            var diagnosis = await _context.Diagnosis
                .Include(d => d.Treatments)
                .Include(d => d.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(d => d.Id == id);

            return diagnosis == null ? null : MapToDto(diagnosis);
        }

        public async Task<MedicalDiagnosisDTO> AddAsync(MedicalDiagnosisDTO dto)
        {
            var entity = new MedicalDiagnosis
            {
                Cie10 = dto.Cie10,
                Denomination = dto.Denomination,
                DiagnosticTypeId = dto.DiagnosticTypeId,
                Recurrence = dto.Recurrence,
                DiagnosisMotivation = dto.DiagnosisMotivation,
                MedicalCareId = dto.MedicalCareId,
                DiseaseId = dto.DiseaseId
            };

            _context.Diagnosis.Add(entity);
            await _context.SaveChangesAsync();

            var diagnosticType = await _context.DiagnosticTypes.FindAsync(dto.DiagnosticTypeId);

            dto.Id = entity.Id;
            dto.DiagnosticTypeName = diagnosticType?.Name ?? string.Empty;

            return dto;
        }

        public async Task<MedicalDiagnosisDTO?> UpdateAsync(MedicalDiagnosisDTO dto)
        {
            var entity = await _context.Diagnosis
                .Include(d => d.Treatments)
                .FirstOrDefaultAsync(d => d.Id == dto.Id);

            if (entity == null) return null;

            entity.Cie10 = dto.Cie10;
            entity.Denomination = dto.Denomination;
            entity.DiagnosticTypeId = dto.DiagnosticTypeId;
            entity.Recurrence = dto.Recurrence;
            entity.DiagnosisMotivation = dto.DiagnosisMotivation;
            entity.MedicalCareId = dto.MedicalCareId;
            entity.DiseaseId = dto.DiseaseId;

            await _context.SaveChangesAsync();

            var diagnosticType = await _context.DiagnosticTypes.FindAsync(dto.DiagnosticTypeId);
            dto.DiagnosticTypeName = diagnosticType?.Name ?? string.Empty;

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Diagnosis
                .Include(d => d.Treatments)
                    .ThenInclude(t => t.Indications)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (entity == null) return false;

            // Eliminar tratamientos y sus indicaciones
            foreach (var treatment in entity.Treatments)
            {
                _context.Indications.RemoveRange(treatment.Indications);

                // Eliminar tratamientos farmacológicos y no farmacológicos
                var pharmacological = await _context.PharmacologicalTreatments
                    .Where(pt => pt.Id == treatment.Id)
                    .ToListAsync();
                _context.PharmacologicalTreatments.RemoveRange(pharmacological);

                var nonPharmacological = await _context.NonPharmacologicalTreatments
                    .Where(npt => npt.Id == treatment.Id)
                    .ToListAsync();
                _context.NonPharmacologicalTreatments.RemoveRange(nonPharmacological);
            }

            _context.Treatments.RemoveRange(entity.Treatments);
            _context.Diagnosis.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MedicalDiagnosisDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var diagnoses = await _context.Diagnosis
                .Include(d => d.Treatments)
                .Include(d => d.DiagnosticTypeNavigation)
                .Where(d => d.MedicalCareId == medicalCareId)
                .ToListAsync();

            return diagnoses.Select(MapToDto).ToList();
        }

        public async Task<bool> AssignTreatmentsAsync(int diagnosisId, List<int> treatmentIds)
        {
            var diagnosis = await _context.Diagnosis
                .Include(d => d.Treatments)
                .FirstOrDefaultAsync(d => d.Id == diagnosisId);

            if (diagnosis == null) return false;

            var treatments = await _context.Treatments
                .Where(t => treatmentIds.Contains(t.Id))
                .ToListAsync();

            diagnosis.Treatments.Clear();
            foreach (var treatment in treatments)
            {
                diagnosis.Treatments.Add(treatment);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private static MedicalDiagnosisDTO MapToDto(MedicalDiagnosis diagnosis) => new MedicalDiagnosisDTO
        {
            Id = diagnosis.Id,
            Cie10 = diagnosis.Cie10,
            Denomination = diagnosis.Denomination,
            DiagnosticTypeId = diagnosis.DiagnosticTypeId,
            DiagnosticTypeName = diagnosis.DiagnosticTypeNavigation?.Name ?? string.Empty,
            Recurrence = diagnosis.Recurrence,
            DiagnosisMotivation = diagnosis.DiagnosisMotivation,
            MedicalCareId = diagnosis.MedicalCareId,
            DiseaseId = diagnosis.DiseaseId,
            TreatmentIds = diagnosis.Treatments.Select(t => t.Id).ToList()
        };
    }
}
