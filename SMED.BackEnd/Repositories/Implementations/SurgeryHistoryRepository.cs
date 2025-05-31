using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SurgeryHistoryRepository : IRepository<SurgeryHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public SurgeryHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SurgeryHistoryDTO>> GetAllAsync()
        {
            return await _context.SurgeryHistories
                .Include(s => s.SurgeryNavigation)
                .Select(s => new SurgeryHistoryDTO
                {
                    SurgeryHistoryId = s.SurgeryHistoryId,
                    HistoryNumber = s.HistoryNumber,
                    Description = s.Description,
                    RegistrationDate = s.RegistrationDate,
                    SurgeryDate = s.SurgeryDate, // Añadido
                    ClinicalHistoryId = s.ClinicalHistoryId,
                    SurgeryId = s.SurgeryId,
                    SurgeryName = s.SurgeryNavigation != null ? s.SurgeryNavigation.Name : null
                })
                .ToListAsync();
        }

        public async Task<SurgeryHistoryDTO?> GetByIdAsync(int id)
        {
            var s = await _context.SurgeryHistories
                .Include(sh => sh.SurgeryNavigation)
                .FirstOrDefaultAsync(sh => sh.SurgeryHistoryId == id);

            if (s == null) return null;

            return new SurgeryHistoryDTO
            {
                SurgeryHistoryId = s.SurgeryHistoryId,
                HistoryNumber = s.HistoryNumber,
                Description = s.Description,
                RegistrationDate = s.RegistrationDate,
                SurgeryDate = s.SurgeryDate, // Añadido
                ClinicalHistoryId = s.ClinicalHistoryId,
                SurgeryId = s.SurgeryId,
                SurgeryName = s.SurgeryNavigation?.Name
            };
        }

        public async Task<SurgeryHistoryDTO> AddAsync(SurgeryHistoryDTO dto)
        {
            var entity = new SurgeryHistory
            {
                HistoryNumber = dto.HistoryNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                SurgeryDate = dto.SurgeryDate, // Añadido
                ClinicalHistoryId = dto.ClinicalHistoryId,
                SurgeryId = dto.SurgeryId
            };

            _context.SurgeryHistories.Add(entity);
            await _context.SaveChangesAsync();
            dto.SurgeryHistoryId = entity.SurgeryHistoryId;
            return dto;
        }

        public async Task<SurgeryHistoryDTO?> UpdateAsync(SurgeryHistoryDTO dto)
        {
            var entity = await _context.SurgeryHistories.FindAsync(dto.SurgeryHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.SurgeryDate = dto.SurgeryDate; // Añadido
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.SurgeryId = dto.SurgeryId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SurgeryHistories.FindAsync(id);
            if (entity == null) return false;

            _context.SurgeryHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}