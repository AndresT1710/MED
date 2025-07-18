using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
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
            var exams = await _context.PhysicalExams.ToListAsync();
            return exams.Select(MapToDto).ToList();
        }

        public async Task<PhysicalExamDTO?> GetByIdAsync(int id)
        {
            var exam = await _context.PhysicalExams.FindAsync(id);
            return exam == null ? null : MapToDto(exam);
        }

        public async Task<PhysicalExamDTO> AddAsync(PhysicalExamDTO dto)
        {
            var entity = new PhysicalExam
            {
                Extremities = dto.Extremities,
                PhysicalExamDetailId = dto.PhysicalExamDetailId,
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

            entity.Extremities = dto.Extremities;
            entity.PhysicalExamDetailId = dto.PhysicalExamDetailId;
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

        private static PhysicalExamDTO MapToDto(PhysicalExam exam) => new PhysicalExamDTO
        {
            PhysicalExamId = exam.PhysicalExamId,
            Extremities = exam.Extremities,
            PhysicalExamDetailId = exam.PhysicalExamDetailId,
            MedicalCareId = exam.MedicalCareId
        };
    }

}
