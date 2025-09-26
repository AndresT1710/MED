using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PerinatalHistoryRepository : IRepository<PerinatalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PerinatalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PerinatalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PerinatalHistories
                .Include(ph => ph.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PerinatalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PerinatalHistories
                .Include(ph => ph.ClinicalHistory)
                .FirstOrDefaultAsync(ph => ph.PerinatalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PerinatalHistoryDTO> AddAsync(PerinatalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PerinatalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PerinatalHistoryDTO?> UpdateAsync(PerinatalHistoryDTO dto)
        {
            var entity = await _context.PerinatalHistories.FindAsync(dto.PerinatalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PerinatalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.PerinatalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PerinatalHistoryDTO MapToDto(PerinatalHistory entity)
        {
            return new PerinatalHistoryDTO
            {
                PerinatalHistoryId = entity.PerinatalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private PerinatalHistory MapToEntity(PerinatalHistoryDTO dto)
        {
            return new PerinatalHistory
            {
                PerinatalHistoryId = dto.PerinatalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}