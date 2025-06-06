using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class FoodConsumptionHistoryRepository : IRepository<FoodConsumptionHistoryDTO, int>
    {
        private readonly SGISContext _context;

        public FoodConsumptionHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<FoodConsumptionHistoryDTO>> GetAllAsync()
        {
            var entities = await _context.FoodConsumptionHistories.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<FoodConsumptionHistoryDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.FoodConsumptionHistories.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<FoodConsumptionHistoryDTO> AddAsync(FoodConsumptionHistoryDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.FoodConsumptionHistories.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<FoodConsumptionHistoryDTO?> UpdateAsync(FoodConsumptionHistoryDTO dto)
        {
            var entity = await _context.FoodConsumptionHistories.FindAsync(dto.FoodConsumptionHistoryId);
            if (entity == null) return null;

            entity.HistoryNumber = dto.HistoryNumber;
            entity.Hour = dto.Hour;
            entity.Place = dto.Place;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.ClinicalHistoryId = dto.ClinicalHistoryId;
            entity.FoodId = dto.FoodId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FoodConsumptionHistories.FindAsync(id);
            if (entity == null) return false;

            _context.FoodConsumptionHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private FoodConsumptionHistoryDTO MapToDto(FoodConsumptionHistory entity)
        {
            return new FoodConsumptionHistoryDTO
            {
                FoodConsumptionHistoryId = entity.FoodConsumptionHistoryId,
                HistoryNumber = entity.HistoryNumber,
                Hour = entity.Hour,
                Place = entity.Place,
                Amount = entity.Amount,
                Description = entity.Description,
                RegistrationDate = entity.RegistrationDate,
                ClinicalHistoryId = entity.ClinicalHistoryId,
                FoodId = entity.FoodId
            };
        }

        private FoodConsumptionHistory MapToEntity(FoodConsumptionHistoryDTO dto)
        {
            return new FoodConsumptionHistory
            {
                FoodConsumptionHistoryId = dto.FoodConsumptionHistoryId,
                HistoryNumber = dto.HistoryNumber,
                Hour = dto.Hour,
                Place = dto.Place,
                Amount = dto.Amount,
                Description = dto.Description,
                RegistrationDate = dto.RegistrationDate,
                ClinicalHistoryId = dto.ClinicalHistoryId,
                FoodId = dto.FoodId
            };
        }
    }
}
