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
                .ToListAsync();

            return diagnoses.Select(MapToDto).ToList();
        }

        public async Task<MedicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            var diagnosis = await _context.Diagnosis
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

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<MedicalDiagnosisDTO?> UpdateAsync(MedicalDiagnosisDTO dto)
        {
            var entity = await _context.Diagnosis
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
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Diagnosis
                .FirstOrDefaultAsync(d => d.Id == id);

            if (entity == null) return false;

            _context.Diagnosis.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static MedicalDiagnosisDTO MapToDto(MedicalDiagnosis diagnosis) => new MedicalDiagnosisDTO
        {
            Id = diagnosis.Id,
            Cie10 = diagnosis.Cie10,
            Denomination = diagnosis.Denomination,
            DiagnosticTypeId = diagnosis.DiagnosticTypeId,
            Recurrence = diagnosis.Recurrence,
            DiagnosisMotivation = diagnosis.DiagnosisMotivation,
            MedicalCareId = diagnosis.MedicalCareId,
            DiseaseId = diagnosis.DiseaseId
        };
    }
}
