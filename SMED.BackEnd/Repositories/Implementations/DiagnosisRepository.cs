using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiagnosisRepository : IRepository<DiagnosisDTO, int>
    {
        private readonly SGISContext _context;

        public DiagnosisRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiagnosisDTO>> GetAllAsync()
        {
            var diagnoses = await _context.Diagnosis.ToListAsync();
            return diagnoses.Select(MapToDto).ToList();
        }

        public async Task<DiagnosisDTO?> GetByIdAsync(int id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);
            return diagnosis == null ? null : MapToDto(diagnosis);
        }

        public async Task<DiagnosisDTO> AddAsync(DiagnosisDTO dto)
        {
            var entity = new Diagnosis
            {
                Cie10 = dto.Cie10,
                Denomination = dto.Denomination,
                DiagnosticType = dto.DiagnosticType,
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

        public async Task<DiagnosisDTO?> UpdateAsync(DiagnosisDTO dto)
        {
            var entity = await _context.Diagnosis.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Cie10 = dto.Cie10;
            entity.Denomination = dto.Denomination;
            entity.DiagnosticType = dto.DiagnosticType;
            entity.Recurrence = dto.Recurrence;
            entity.DiagnosisMotivation = dto.DiagnosisMotivation;
            entity.MedicalCareId = dto.MedicalCareId;
            entity.DiseaseId = dto.DiseaseId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Diagnosis.FindAsync(id);
            if (entity == null) return false;

            _context.Diagnosis.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static DiagnosisDTO MapToDto(Diagnosis diagnosis) => new DiagnosisDTO
        {
            Id = diagnosis.Id,
            Cie10 = diagnosis.Cie10,
            Denomination = diagnosis.Denomination,
            DiagnosticType = diagnosis.DiagnosticType,
            Recurrence = diagnosis.Recurrence,
            DiagnosisMotivation = diagnosis.DiagnosisMotivation,
            MedicalCareId = diagnosis.MedicalCareId,
            DiseaseId = diagnosis.DiseaseId
        };
    }

}
