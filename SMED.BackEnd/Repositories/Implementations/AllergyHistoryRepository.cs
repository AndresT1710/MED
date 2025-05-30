using Infrastructure.Models;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;


namespace SMED.BackEnd.Repositories.Implementations
{
    public class AllergyHistoryRepository : IRepository<AllergyHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public AllergyHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<AllergyHistoryDTO>> GetAllAsync()
        {
            return await _context.AllergyHistories
                .Include(a => a.AllergyNavigation)
                .Include(a => a.HistoryNavigation)
                .Select(a => new AllergyHistoryDTO
                {
                    AllergyHistoryId = a.AllergyHistoryId,
                    HistoryNumber = a.HistoryNumber,
                    RegistrationDate = a.RegistrationDate,
                    AllergyId = a.AllergyId,
                    ClinicalHistoryId = a.ClinicalHistoryId,
                    AllergyName = a.AllergyNavigation != null ? a.AllergyNavigation.Name : null,
                    ClinicalHistoryNumber = a.HistoryNavigation.HistoryNumber
                }).ToListAsync();
        }

        public async Task<AllergyHistoryDTO?> GetByIdAsync(int id)
        {
            return await _context.AllergyHistories
                .Include(a => a.AllergyNavigation)
                .Include(a => a.HistoryNavigation)
                .Where(a => a.AllergyHistoryId == id)
                .Select(a => new AllergyHistoryDTO
                {
                    AllergyHistoryId = a.AllergyHistoryId,
                    HistoryNumber = a.HistoryNumber,
                    RegistrationDate = a.RegistrationDate,
                    AllergyId = a.AllergyId,
                    ClinicalHistoryId = a.ClinicalHistoryId,
                    AllergyName = a.AllergyNavigation != null ? a.AllergyNavigation.Name : null,
                    ClinicalHistoryNumber = a.HistoryNavigation.HistoryNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<AllergyHistoryDTO> AddAsync(AllergyHistoryDTO dto)
        {
            var entity = new AllergyHistory
            {
                HistoryNumber = dto.HistoryNumber,
                RegistrationDate = dto.RegistrationDate,
                AllergyId = dto.AllergyId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };

            _context.AllergyHistories.Add(entity);
            await _context.SaveChangesAsync();

            dto.AllergyHistoryId = entity.AllergyHistoryId;
            return dto;
        }

        public async Task<AllergyHistoryDTO?> UpdateAsync(AllergyHistoryDTO dto)
        {
            var entity = await _context.AllergyHistories.FindAsync(dto.AllergyHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.AllergyId = dto.AllergyId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AllergyHistories.FindAsync(id);
            if (entity == null) return false;

            _context.AllergyHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
