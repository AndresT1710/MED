using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class RegionRepository : IRepository<RegionDTO, int>
    {
        private readonly SGISContext _context;

        public RegionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<RegionDTO>> GetAllAsync()
        {
            var regions = await _context.Regions
                .OrderBy(r => r.Name)
                .ToListAsync();

            return regions.Select(MapToDto).ToList();
        }

        public async Task<RegionDTO?> GetByIdAsync(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            return region == null ? null : MapToDto(region);
        }

        public async Task<RegionDTO> AddAsync(RegionDTO dto)
        {
            var entity = new Region
            {
                Name = dto.Name ?? string.Empty
            };

            _context.Regions.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<RegionDTO?> UpdateAsync(RegionDTO dto)
        {
            var entity = await _context.Regions.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name ?? string.Empty;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Regions.FindAsync(id);
            if (entity == null) return false;

            _context.Regions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static RegionDTO MapToDto(Region region) => new RegionDTO
        {
            Id = region.Id,
            Name = region.Name
        };
    }
}
