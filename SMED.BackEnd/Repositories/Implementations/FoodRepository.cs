using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class FoodRepository : IRepository<FoodDTO, int>
    {
        private readonly SGISContext _context;

        public FoodRepository(SGISContext context)
        {
            _context = context;
        }

        

        public async Task<List<FoodDTO>> GetAllAsync()
        {
            var foods = await _context.Foods.ToListAsync();
            return foods.Select(MapToDto).ToList();
        }

        public async Task<FoodDTO?> GetByIdAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            return food == null ? null : MapToDto(food);
        }

        public async Task<FoodDTO> AddAsync(FoodDTO dto)
        {
            var entity = new Food
            {
                Name = dto.Name
            };

            _context.Foods.Add(entity);
            await _context.SaveChangesAsync();
            dto.FoodId = entity.FoodId;
            return dto;
        }

        public async Task<FoodDTO?> UpdateAsync(FoodDTO dto)
        {
            var entity = await _context.Foods.FindAsync(dto.FoodId);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Foods.FindAsync(id);
            if (entity == null) return false;

            _context.Foods.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static FoodDTO MapToDto(Food food) => new FoodDTO
        {
            FoodId = food.FoodId,
            Name = food.Name
        };
    }

}
