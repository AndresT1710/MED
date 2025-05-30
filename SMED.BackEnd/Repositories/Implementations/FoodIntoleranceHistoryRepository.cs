using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class FoodIntoleranceHistoryRepository : IRepository<FoodIntoleranceHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public FoodIntoleranceHistoryRepository(SGISContext context)
        {
            _context = context;
        }


        public async Task<List<FoodIntoleranceHistoryDTO>> GetAllAsync()
        {
            var list = await _context.FoodIntoleranceHistories.ToListAsync();
            return list.Select(MapToDto).ToList();
        }

        public async Task<FoodIntoleranceHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.FoodIntoleranceHistories.FindAsync(id);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<FoodIntoleranceHistoryDTO> AddAsync(FoodIntoleranceHistoryDTO dto)
        {
            var entity = new FoodIntoleranceHistory
            {
                HistoryNumber = dto.HistoryNumber,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                FoodId = dto.FoodId,
                ClinicalHistoryId = dto.ClinicalHistoryId
            };

            _context.FoodIntoleranceHistories.Add(entity);
            await _context.SaveChangesAsync();

            dto.FoodIntoleranceHistoryId = entity.FoodIntoleranceHistoryId;
            return dto;
        }

        public async Task<FoodIntoleranceHistoryDTO?> UpdateAsync(FoodIntoleranceHistoryDTO dto)
        {
            var entity = await _context.FoodIntoleranceHistories.FindAsync(dto.FoodIntoleranceHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.FoodId = dto.FoodId;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FoodIntoleranceHistories.FindAsync(id);
            if (entity == null) return false;

            _context.FoodIntoleranceHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static FoodIntoleranceHistoryDTO MapToDto(FoodIntoleranceHistory history) => new FoodIntoleranceHistoryDTO
        {
            FoodIntoleranceHistoryId = history.FoodIntoleranceHistoryId,
            HistoryNumber = history.HistoryNumber,
            Description = history.Description,
            RegistrationDate = history.RegistrationDate,
            FoodId = history.FoodId,
            ClinicalHistoryId = history.ClinicalHistoryId
        };
    }

}
