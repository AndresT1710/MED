using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ExamResultsRepository : IRepository<ExamResultsDTO, int>
    {
        private readonly SGISContext _context;

        public ExamResultsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ExamResultsDTO>> GetAllAsync()
        {
            var results = await _context.ExamResults.ToListAsync();
            return results.Select(MapToDto).ToList();
        }

        public async Task<ExamResultsDTO?> GetByIdAsync(int id)
        {
            var result = await _context.ExamResults.FindAsync(id);
            return result == null ? null : MapToDto(result);
        }

        public async Task<ExamResultsDTO> AddAsync(ExamResultsDTO dto)
        {
            var entity = new ExamResults
            {
                LinkPdf = dto.LinkPdf,
                ExamDate = dto.ExamDate,
                Description = dto.Description,
                ExamTypeId = dto.ExamTypeId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.ExamResults.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<ExamResultsDTO?> UpdateAsync(ExamResultsDTO dto)
        {
            var entity = await _context.ExamResults.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.LinkPdf = dto.LinkPdf;
            entity.ExamDate = dto.ExamDate;
            entity.Description = dto.Description;
            entity.ExamTypeId = dto.ExamTypeId;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ExamResults.FindAsync(id);
            if (entity == null) return false;

            _context.ExamResults.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ExamResultsDTO MapToDto(ExamResults result) => new ExamResultsDTO
        {
            Id = result.Id,
            LinkPdf = result.LinkPdf,
            ExamDate = result.ExamDate,
            Description = result.Description,
            ExamTypeId = result.ExamTypeId,
            MedicalCareId = result.MedicalCareId
        };
    }

}
