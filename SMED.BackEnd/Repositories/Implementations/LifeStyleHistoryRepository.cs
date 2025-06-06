using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class LifeStyleHistoryRepository : IRepository<LifeStyleHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public LifeStyleHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<LifeStyleHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.LifeStyleHistories
                .Include(h => h.LifeStyleNavigation)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<LifeStyleHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.LifeStyleHistories
                .Include(h => h.LifeStyleNavigation)
                .FirstOrDefaultAsync(h => h.LifeStyleHistoryId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<LifeStyleHistoryDTO> AddAsync(LifeStyleHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.LifeStyleHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<LifeStyleHistoryDTO?> UpdateAsync(LifeStyleHistoryDTO dto)
        {
            var entity = await _context.LifeStyleHistories.FindAsync(dto.LifeStyleHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.LifeStyleId = dto.LifeStyleId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.LifeStyleHistories.FindAsync(id);
            if (entity == null) return false;

            _context.LifeStyleHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private LifeStyleHistoryDTO MapToDto(LifeStyleHistory entity)
        {
            return new LifeStyleHistoryDTO
            {
                LifeStyleHistoryId = entity.LifeStyleHistoryId,
                HistoryNumber = entity.HistoryNumber,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                LifeStyleId = entity.LifeStyleId,
                LifeStyleName = entity.LifeStyleNavigation?.Name
            };
        }

        private LifeStyleHistory MapToEntity(LifeStyleHistoryDTO dto)
        {
            return new LifeStyleHistory
            {
                LifeStyleHistoryId = dto.LifeStyleHistoryId,
                HistoryNumber = dto.HistoryNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                LifeStyleId = dto.LifeStyleId
            };
        }
    }
}
