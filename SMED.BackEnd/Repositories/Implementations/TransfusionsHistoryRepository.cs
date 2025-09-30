using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TransfusionsHistoryRepository : IRepository<TransfusionsHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public TransfusionsHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TransfusionsHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.TransfusionsHistories
                .Include(t => t.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TransfusionsHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TransfusionsHistories
                .Include(t => t.ClinicalHistory)
                .FirstOrDefaultAsync(t => t.TransfusionsHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TransfusionsHistoryDTO> AddAsync(TransfusionsHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TransfusionsHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TransfusionsHistoryDTO?> UpdateAsync(TransfusionsHistoryDTO dto)
        {
            var entity = await _context.TransfusionsHistories.FindAsync(dto.TransfusionsHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.TransfusionReason = dto.TransfusionReason;
            entity.TransfusionDate = dto.TransfusionDate;
            entity.Observations = dto.Observations;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TransfusionsHistories.FindAsync(id);
            if (entity == null) return false;
            _context.TransfusionsHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private TransfusionsHistoryDTO MapToDto(TransfusionsHistory entity)
        {
            return new TransfusionsHistoryDTO
            {
                TransfusionsHistoryId = entity.TransfusionsHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                TransfusionReason = entity.TransfusionReason,
                TransfusionDate = entity.TransfusionDate,
                Observations = entity.Observations
            };
        }

        private TransfusionsHistory MapToEntity(TransfusionsHistoryDTO dto)
        {
            return new TransfusionsHistory
            {
                TransfusionsHistoryId = dto.TransfusionsHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                TransfusionReason = dto.TransfusionReason,
                TransfusionDate = dto.TransfusionDate,
                Observations = dto.Observations
            };
        }
    }
}