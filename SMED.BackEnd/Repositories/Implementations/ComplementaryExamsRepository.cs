using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ComplementaryExamsRepository : IRepository<ComplementaryExamsDTO, int>
    {
        private readonly SGISContext _context;

        public ComplementaryExamsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ComplementaryExamsDTO>> GetAllAsync()
        {
            var entities = await _context.ComplementaryExams
                .Include(c => c.MedicalCare)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<ComplementaryExamsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ComplementaryExams
                .Include(c => c.MedicalCare)
                .FirstOrDefaultAsync(c => c.ComplementaryExamsId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ComplementaryExamsDTO> AddAsync(ComplementaryExamsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ComplementaryExams.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ComplementaryExamsDTO?> UpdateAsync(ComplementaryExamsDTO dto)
        {
            var entity = await _context.ComplementaryExams.FindAsync(dto.ComplementaryExamsId);
            if (entity == null) return null;

            entity.Exam = dto.Exam;
            entity.ExamDate = dto.ExamDate;
            entity.Descriptions = dto.Descriptions;
            entity.PdfLink = dto.PdfLink;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ComplementaryExams.FindAsync(id);
            if (entity == null) return false;

            _context.ComplementaryExams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private ComplementaryExamsDTO MapToDto(ComplementaryExams entity)
        {
            return new ComplementaryExamsDTO
            {
                ComplementaryExamsId = entity.ComplementaryExamsId,
                Exam = entity.Exam,
                ExamDate = entity.ExamDate,
                Descriptions = entity.Descriptions,
                PdfLink = entity.PdfLink,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private ComplementaryExams MapToEntity(ComplementaryExamsDTO dto)
        {
            return new ComplementaryExams
            {
                ComplementaryExamsId = dto.ComplementaryExamsId,
                Exam = dto.Exam,
                ExamDate = dto.ExamDate,
                Descriptions = dto.Descriptions,
                PdfLink = dto.PdfLink,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}
