using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PhysicalExamDetailRepository : IRepository<PhysicalExamDetailDTO, int>
    {
        private readonly SGISContext _context;

        public PhysicalExamDetailRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PhysicalExamDetailDTO>> GetAllAsync()
        {
            var details = await _context.PhysicalExamDetails.ToListAsync();
            return details.Select(MapToDto).ToList();
        }

        public async Task<PhysicalExamDetailDTO?> GetByIdAsync(int id)
        {
            var detail = await _context.PhysicalExamDetails.FindAsync(id);
            return detail == null ? null : MapToDto(detail);
        }

        public async Task<PhysicalExamDetailDTO> AddAsync(PhysicalExamDetailDTO dto)
        {
            var entity = new PhysicalExamDetail
            {
                Description = dto.Description,
                PhysicalExamTypeId = dto.PhysicalExamTypeId
            };

            _context.PhysicalExamDetails.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PhysicalExamDetailDTO?> UpdateAsync(PhysicalExamDetailDTO dto)
        {
            var entity = await _context.PhysicalExamDetails.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.PhysicalExamTypeId = dto.PhysicalExamTypeId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PhysicalExamDetails.FindAsync(id);
            if (entity == null) return false;

            _context.PhysicalExamDetails.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static PhysicalExamDetailDTO MapToDto(PhysicalExamDetail detail) => new PhysicalExamDetailDTO
        {
            Id = detail.Id,
            Description = detail.Description,
            PhysicalExamTypeId = detail.PhysicalExamTypeId
        };
    }

}
