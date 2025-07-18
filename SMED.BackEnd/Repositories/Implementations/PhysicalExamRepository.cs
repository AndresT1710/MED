using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories
{
    public class PhysicalExamRepository : IRepository<PhysicalExamDTO, int>
    {
        private readonly SGISContext _context;

        public PhysicalExamRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PhysicalExamDTO>> GetAllAsync()
        {
            var exams = await _context.PhysicalExams
                .Include(p => p.RegionNavigation)
                .Include(p => p.PathologicalEvidenceNavigation)
                .Include(p => p.PhysicalExamTypeNavigation)
                .ToListAsync();

            return exams.Select(MapToDto).ToList();
        }

        public async Task<PhysicalExamDTO?> GetByIdAsync(int id)
        {
            var exam = await _context.PhysicalExams
                .Include(p => p.RegionNavigation)
                .Include(p => p.PathologicalEvidenceNavigation)
                .Include(p => p.PhysicalExamTypeNavigation)
                .FirstOrDefaultAsync(p => p.PhysicalExamId == id);

            return exam == null ? null : MapToDto(exam);
        }

        public async Task<PhysicalExamDTO> AddAsync(PhysicalExamDTO dto)
        {
            var entity = new PhysicalExam
            {
                Observation = dto.Observation,
                RegionId = dto.RegionId,
                PathologicalEvidenceId = dto.PathologicalEvidenceId,
                PhysicalExamTypeId = dto.PhysicalExamTypeId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.PhysicalExams.Add(entity);
            await _context.SaveChangesAsync();

            dto.PhysicalExamId = entity.PhysicalExamId;
            return dto;
        }

        public async Task<PhysicalExamDTO?> UpdateAsync(PhysicalExamDTO dto)
        {
            var entity = await _context.PhysicalExams.FindAsync(dto.PhysicalExamId);
            if (entity == null) return null;

            entity.Observation = dto.Observation;
            entity.RegionId = dto.RegionId;
            entity.PathologicalEvidenceId = dto.PathologicalEvidenceId;
            entity.PhysicalExamTypeId = dto.PhysicalExamTypeId;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PhysicalExams.FindAsync(id);
            if (entity == null) return false;

            _context.PhysicalExams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PhysicalExamDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var exams = await _context.PhysicalExams
                .Include(p => p.RegionNavigation)
                .Include(p => p.PathologicalEvidenceNavigation)
                .Include(p => p.PhysicalExamTypeNavigation)
                .Where(p => p.MedicalCareId == medicalCareId)
                .ToListAsync();

            return exams.Select(MapToDto).ToList();
        }

        private static PhysicalExamDTO MapToDto(PhysicalExam exam) => new PhysicalExamDTO
        {
            PhysicalExamId = exam.PhysicalExamId,
            Observation = exam.Observation,
            RegionId = exam.RegionId,
            RegionName = exam.RegionNavigation?.Name,
            PathologicalEvidenceId = exam.PathologicalEvidenceId,
            PathologicalEvidenceName = exam.PathologicalEvidenceNavigation?.Name,
            PhysicalExamTypeId = exam.PhysicalExamTypeId,
            PhysicalExamTypeName = exam.PhysicalExamTypeNavigation?.Name,
            MedicalCareId = exam.MedicalCareId
        };
    }
}
