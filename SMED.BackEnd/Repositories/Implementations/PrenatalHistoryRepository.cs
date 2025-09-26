using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PrenatalHistoryRepository : IRepository<PrenatalHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PrenatalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PrenatalHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PrenatalHistories
                .Include(ph => ph.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PrenatalHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PrenatalHistories
                .Include(ph => ph.ClinicalHistory)
                .FirstOrDefaultAsync(ph => ph.PrenatalHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PrenatalHistoryDTO> AddAsync(PrenatalHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PrenatalHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PrenatalHistoryDTO?> UpdateAsync(PrenatalHistoryDTO dto)
        {
            var entity = await _context.PrenatalHistories.FindAsync(dto.PrenatalHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PrenatalHistories.FindAsync(id);
            if (entity == null) return false;
            _context.PrenatalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PrenatalHistoryDTO MapToDto(PrenatalHistory entity)
        {
            return new PrenatalHistoryDTO
            {
                PrenatalHistoryId = entity.PrenatalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private PrenatalHistory MapToEntity(PrenatalHistoryDTO dto)
        {
            return new PrenatalHistory
            {
                PrenatalHistoryId = dto.PrenatalHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}