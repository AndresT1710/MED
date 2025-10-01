using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class NeurologicalExamRepository : IRepository<NeurologicalExamDTO, int>
    {
        private readonly SGISContext _context;

        public NeurologicalExamRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<NeurologicalExamDTO>> GetAllAsync()
        {
            var entities = await _context.NeurologicalExams
                .Include(n => n.ClinicalHistory)
                .Include(n => n.NeurologicalExamType)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<NeurologicalExamDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.NeurologicalExams
                .Include(n => n.ClinicalHistory)
                .Include(n => n.NeurologicalExamType)
                .FirstOrDefaultAsync(n => n.NeurologicalExamId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<NeurologicalExamDTO> AddAsync(NeurologicalExamDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.NeurologicalExams.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<NeurologicalExamDTO?> UpdateAsync(NeurologicalExamDTO dto)
        {
            var entity = await _context.NeurologicalExams.FindAsync(dto.NeurologicalExamId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Name = dto.Name;
            entity.LinkPdf = dto.LinkPdf;
            entity.ExamDate = dto.ExamDate;
            entity.Description = dto.Description;
            entity.NeurologicalExamTypeId = dto.NeurologicalExamTypeId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.NeurologicalExams.FindAsync(id);
            if (entity == null) return false;
            _context.NeurologicalExams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private NeurologicalExamDTO MapToDto(NeurologicalExam entity)
        {
            return new NeurologicalExamDTO
            {
                NeurologicalExamId = entity.NeurologicalExamId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Name = entity.Name,
                LinkPdf = entity.LinkPdf,
                ExamDate = entity.ExamDate,
                Description = entity.Description,
                NeurologicalExamTypeId = entity.NeurologicalExamTypeId,
                NeurologicalExamTypeName = entity.NeurologicalExamType?.Name
            };
        }

        private NeurologicalExam MapToEntity(NeurologicalExamDTO dto)
        {
            return new NeurologicalExam
            {
                NeurologicalExamId = dto.NeurologicalExamId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Name = dto.Name,
                LinkPdf = dto.LinkPdf,
                ExamDate = dto.ExamDate,
                Description = dto.Description,
                NeurologicalExamTypeId = dto.NeurologicalExamTypeId
            };
        }
    }
}