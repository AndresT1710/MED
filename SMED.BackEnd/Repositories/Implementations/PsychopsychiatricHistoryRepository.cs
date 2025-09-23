using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PsychopsychiatricHistoryRepository : IRepository<PsychopsychiatricHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PsychopsychiatricHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PsychopsychiatricHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PsychopsychiatricHistories
                .Include(pp => pp.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PsychopsychiatricHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PsychopsychiatricHistories
                .Include(pp => pp.ClinicalHistory)
                .FirstOrDefaultAsync(pp => pp.PsychopsychiatricHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PsychopsychiatricHistoryDTO> AddAsync(PsychopsychiatricHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PsychopsychiatricHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PsychopsychiatricHistoryDTO?> UpdateAsync(PsychopsychiatricHistoryDTO dto)
        {
            var entity = await _context.PsychopsychiatricHistories.FindAsync(dto.PsychopsychiatricHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Type = dto.Type;
            entity.Actor = dto.Actor;
            entity.HistoryDate = dto.HistoryDate;
            entity.HistoryState = dto.HistoryState;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PsychopsychiatricHistories.FindAsync(id);
            if (entity == null) return false;

            _context.PsychopsychiatricHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PsychopsychiatricHistoryDTO MapToDto(PsychopsychiatricHistory entity)
        {
            return new PsychopsychiatricHistoryDTO
            {
                PsychopsychiatricHistoryId = entity.PsychopsychiatricHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Type = entity.Type,
                Actor = entity.Actor,
                HistoryDate = entity.HistoryDate,
                HistoryState = entity.HistoryState
            };
        }

        private PsychopsychiatricHistory MapToEntity(PsychopsychiatricHistoryDTO dto)
        {
            return new PsychopsychiatricHistory
            {
                PsychopsychiatricHistoryId = dto.PsychopsychiatricHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Type = dto.Type,
                Actor = dto.Actor,
                HistoryDate = dto.HistoryDate,
                HistoryState = dto.HistoryState
            };
        }
    }
}