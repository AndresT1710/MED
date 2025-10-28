using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ForbiddenFoodRepository : IRepository<ForbiddenFoodDTO, int>
    {
        private readonly SGISContext _context;

        public ForbiddenFoodRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ForbiddenFoodDTO>> GetAllAsync()
        {
            var entities = await _context.ForbiddenFoods.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ForbiddenFoodDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.ForbiddenFoods.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ForbiddenFoodDTO> AddAsync(ForbiddenFoodDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.ForbiddenFoods.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<ForbiddenFoodDTO?> UpdateAsync(ForbiddenFoodDTO dto)
        {
            var entity = await _context.ForbiddenFoods.FindAsync(dto.ForbiddenFoodId);
            if (entity == null) return null;

            entity.RegistrationDate = dto.RegistrationDate;
            entity.Description = dto.Description;
            entity.FoodId = dto.FoodId;
            entity.CareId = dto.CareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ForbiddenFoods.FindAsync(id);
            if (entity == null) return false;

            _context.ForbiddenFoods.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private ForbiddenFoodDTO MapToDto(ForbiddenFood entity) => new ForbiddenFoodDTO
        {
            ForbiddenFoodId = entity.ForbiddenFoodId,
            RegistrationDate = entity.RegistrationDate,
            Description = entity.Description,
            FoodId = entity.FoodId,
            CareId = entity.CareId
        };

        private ForbiddenFood MapToEntity(ForbiddenFoodDTO dto) => new ForbiddenFood
        {
            ForbiddenFoodId = dto.ForbiddenFoodId,
            RegistrationDate = dto.RegistrationDate,
            Description = dto.Description,
            FoodId = dto.FoodId,
            CareId = dto.CareId
        };
    }
}
