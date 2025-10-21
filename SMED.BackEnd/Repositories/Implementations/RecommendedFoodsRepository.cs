using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class RecommendedFoodsRepository : IRepository<RecommendedFoodsDTO, int>
    {
        private readonly SGISContext _context;

        public RecommendedFoodsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<RecommendedFoodsDTO>> GetAllAsync()
        {
            var entities = await _context.RecommendedFoods.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<RecommendedFoodsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.RecommendedFoods.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<RecommendedFoodsDTO> AddAsync(RecommendedFoodsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.RecommendedFoods.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<RecommendedFoodsDTO?> UpdateAsync(RecommendedFoodsDTO dto)
        {
            var entity = await _context.RecommendedFoods.FindAsync(dto.RecommendedFoodId);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.Frequency = dto.Frequency;
            entity.Quantity = dto.Quantity;
            entity.FoodId = dto.FoodId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.RecommendedFoods.FindAsync(id);
            if (entity == null) return false;

            _context.RecommendedFoods.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RecommendedFoodsDTO>> GetByFoodIdAsync(int foodId)
        {
            var entities = await _context.RecommendedFoods
                .Where(rf => rf.FoodId == foodId)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<RecommendedFoodsDTO>> GetByFrequencyAsync(string frequency)
        {
            var entities = await _context.RecommendedFoods
                .Where(rf => rf.Frequency == frequency)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        private RecommendedFoodsDTO MapToDto(RecommendedFoods entity)
        {
            return new RecommendedFoodsDTO
            {
                RecommendedFoodId = entity.RecommendedFoodId,
                Description = entity.Description,
                Frequency = entity.Frequency,
                Quantity = entity.Quantity,
                FoodId = entity.FoodId
            };
        }

        private RecommendedFoods MapToEntity(RecommendedFoodsDTO dto)
        {
            return new RecommendedFoods
            {
                RecommendedFoodId = dto.RecommendedFoodId,
                Description = dto.Description,
                Frequency = dto.Frequency,
                Quantity = dto.Quantity,
                FoodId = dto.FoodId
            };
        }
    }
}