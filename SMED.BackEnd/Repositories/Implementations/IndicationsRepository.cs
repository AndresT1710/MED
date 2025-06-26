using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class IndicationsRepository : IRepository<IndicationsDTO, int>
    {
        private readonly SGISContext _context;

        public IndicationsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<IndicationsDTO>> GetAllAsync()
        {
            var indications = await _context.Indications.ToListAsync();
            return indications.Select(MapToDto).ToList();
        }

        public async Task<IndicationsDTO?> GetByIdAsync(int id)
        {
            var indication = await _context.Indications.FindAsync(id);
            return indication == null ? null : MapToDto(indication);
        }

        public async Task<IndicationsDTO> AddAsync(IndicationsDTO dto)
        {
            var entity = new Indications
            {
                Description = dto.Description,
                TreatmentId = dto.TreatmentId
            };

            _context.Indications.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<IndicationsDTO?> UpdateAsync(IndicationsDTO dto)
        {
            var entity = await _context.Indications.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.TreatmentId = dto.TreatmentId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Indications.FindAsync(id);
            if (entity == null) return false;

            _context.Indications.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static IndicationsDTO MapToDto(Indications indication) => new IndicationsDTO
        {
            Id = indication.Id,
            Description = indication.Description,
            TreatmentId = indication.TreatmentId
        };
    }

}
