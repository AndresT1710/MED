using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class WorkHistoryRepository : IRepository<WorkHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public WorkHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<WorkHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.WorkHistories
                .Include(w => w.ClinicalHistory)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<WorkHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.WorkHistories
                .Include(w => w.ClinicalHistory)
                .FirstOrDefaultAsync(w => w.WorkHistoryId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<WorkHistoryDTO> AddAsync(WorkHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.WorkHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<WorkHistoryDTO?> UpdateAsync(WorkHistoryDTO dto)
        {
            var entity = await _context.WorkHistories.FindAsync(dto.WorkHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.Experience = dto.Experience;
            entity.Stability = dto.Stability;
            entity.SatisfactionLevel = dto.SatisfactionLevel;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.WorkHistories.FindAsync(id);
            if (entity == null) return false;

            _context.WorkHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private WorkHistoryDTO MapToDto(WorkHistory entity)
        {
            return new WorkHistoryDTO
            {
                WorkHistoryId = entity.WorkHistoryId,
                HistoryNumber = entity.HistoryNumber,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                Experience = entity.Experience,
                Stability = entity.Stability,
                SatisfactionLevel = entity.SatisfactionLevel
            };
        }

        private WorkHistory MapToEntity(WorkHistoryDTO dto)
        {
            return new WorkHistory
            {
                WorkHistoryId = dto.WorkHistoryId,
                HistoryNumber = dto.HistoryNumber,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                Experience = dto.Experience,
                Stability = dto.Stability,
                SatisfactionLevel = dto.SatisfactionLevel
            };
        }
    }
}