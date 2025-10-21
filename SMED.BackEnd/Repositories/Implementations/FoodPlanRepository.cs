using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class FoodPlanRepository : IRepository<FoodPlanDTO, int>
    {
        private readonly SGISContext _context;

        public FoodPlanRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<FoodPlanDTO>> GetAllAsync()
        {
            var entities = await _context.FoodPlans.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<FoodPlanDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.FoodPlans.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<FoodPlanDTO> AddAsync(FoodPlanDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.FoodPlans.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<FoodPlanDTO?> UpdateAsync(FoodPlanDTO dto)
        {
            var entity = await _context.FoodPlans.FindAsync(dto.FoodPlanId);
            if (entity == null) return null;

            entity.RegistrationDate = dto.RegistrationDate;
            entity.RestrictionId = dto.RestrictionId;
            entity.RecommendedFoodId = dto.RecommendedFoodId;
            entity.CareId = dto.CareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FoodPlans.FindAsync(id);
            if (entity == null) return false;

            _context.FoodPlans.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FoodPlanDTO>> GetByCareIdAsync(int careId)
        {
            var entities = await _context.FoodPlans
                .Where(fp => fp.CareId == careId)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<FoodPlanDTO?> GetByRestrictionIdAsync(int restrictionId)
        {
            var entity = await _context.FoodPlans
                .FirstOrDefaultAsync(fp => fp.RestrictionId == restrictionId);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<List<FoodPlanDTO>> GetByRecommendedFoodIdAsync(int recommendedFoodId)
        {
            var entities = await _context.FoodPlans
                .Where(fp => fp.RecommendedFoodId == recommendedFoodId)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        private FoodPlanDTO MapToDto(FoodPlan entity)
        {
            return new FoodPlanDTO
            {
                FoodPlanId = entity.FoodPlanId,
                RegistrationDate = entity.RegistrationDate,
                RestrictionId = entity.RestrictionId,
                RecommendedFoodId = entity.RecommendedFoodId,
                CareId = entity.CareId
            };
        }

        private FoodPlan MapToEntity(FoodPlanDTO dto)
        {
            return new FoodPlan
            {
                FoodPlanId = dto.FoodPlanId,
                RegistrationDate = dto.RegistrationDate,
                RestrictionId = dto.RestrictionId,
                RecommendedFoodId = dto.RecommendedFoodId,
                CareId = dto.CareId
            };
        }
    }
}