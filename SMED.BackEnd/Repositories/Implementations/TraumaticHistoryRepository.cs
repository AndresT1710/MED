using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TraumaticHistoryRepository : IRepository<TraumaticHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public TraumaticHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TraumaticHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.TraumaticHistories
                .Include(t => t.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TraumaticHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TraumaticHistories
                .Include(t => t.ClinicalHistory)
                .FirstOrDefaultAsync(t => t.TraumaticHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TraumaticHistoryDTO> AddAsync(TraumaticHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TraumaticHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TraumaticHistoryDTO?> UpdateAsync(TraumaticHistoryDTO dto)
        {
            var entity = await _context.TraumaticHistories.FindAsync(dto.TraumaticHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TraumaticHistories.FindAsync(id);
            if (entity == null) return false;
            _context.TraumaticHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private TraumaticHistoryDTO MapToDto(TraumaticHistory entity)
        {
            return new TraumaticHistoryDTO
            {
                TraumaticHistoryId = entity.TraumaticHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private TraumaticHistory MapToEntity(TraumaticHistoryDTO dto)
        {
            return new TraumaticHistory
            {
                TraumaticHistoryId = dto.TraumaticHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}