using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class LocationRepository : IRepository<LocationDTO, int>
    {
        private readonly SGISContext _context;

        public LocationRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<LocationDTO>> GetAllAsync()
        {
            var places = await _context.Locations.ToListAsync();
            return places.Select(MapToDto).ToList();
        }

        public async Task<LocationDTO?> GetByIdAsync(int id)
        {
            var place = await _context.Locations.FindAsync(id);
            return place == null ? null : MapToDto(place);
        }

        public async Task<LocationDTO> AddAsync(LocationDTO dto)
        {
            var entity = new Location
            {
                Name = dto.Name
            };

            _context.Locations.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<LocationDTO?> UpdateAsync(LocationDTO dto)
        {
            var entity = await _context.Locations.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity == null) return false;

            _context.Locations.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static LocationDTO MapToDto(Location place) => new LocationDTO
        {
            Id = place.Id,
            Name = place.Name
        };
    }

}


