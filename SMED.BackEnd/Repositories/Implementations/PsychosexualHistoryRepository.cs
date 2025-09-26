using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PsychosexualHistoryRepository : IRepository<PsychosexualHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public PsychosexualHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PsychosexualHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.PsychosexualHistories
                .Include(ps => ps.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PsychosexualHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PsychosexualHistories
                .Include(ps => ps.ClinicalHistory)
                .FirstOrDefaultAsync(ps => ps.PsychosexualHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PsychosexualHistoryDTO> AddAsync(PsychosexualHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PsychosexualHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PsychosexualHistoryDTO?> UpdateAsync(PsychosexualHistoryDTO dto)
        {
            var entity = await _context.PsychosexualHistories.FindAsync(dto.PsychosexualHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PsychosexualHistories.FindAsync(id);
            if (entity == null) return false;

            _context.PsychosexualHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private PsychosexualHistoryDTO MapToDto(PsychosexualHistory entity)
        {
            return new PsychosexualHistoryDTO
            {
                PsychosexualHistoryId = entity.PsychosexualHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Description = entity.Description
            };
        }

        private PsychosexualHistory MapToEntity(PsychosexualHistoryDTO dto)
        {
            return new PsychosexualHistory
            {
                PsychosexualHistoryId = dto.PsychosexualHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Description = dto.Description
            };
        }
    }
}