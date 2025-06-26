using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PlaceOfAttentionRepository : IRepository<PlaceOfAttentionDTO, int>
    {
        private readonly SGISContext _context;

        public PlaceOfAttentionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PlaceOfAttentionDTO>> GetAllAsync()
        {
            var places = await _context.PlaceOfAttentions.ToListAsync();
            return places.Select(MapToDto).ToList();
        }

        public async Task<PlaceOfAttentionDTO?> GetByIdAsync(int id)
        {
            var place = await _context.PlaceOfAttentions.FindAsync(id);
            return place == null ? null : MapToDto(place);
        }

        public async Task<PlaceOfAttentionDTO> AddAsync(PlaceOfAttentionDTO dto)
        {
            var entity = new PlaceOfAttention
            {
                Name = dto.Name
            };

            _context.PlaceOfAttentions.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<PlaceOfAttentionDTO?> UpdateAsync(PlaceOfAttentionDTO dto)
        {
            var entity = await _context.PlaceOfAttentions.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PlaceOfAttentions.FindAsync(id);
            if (entity == null) return false;

            _context.PlaceOfAttentions.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static PlaceOfAttentionDTO MapToDto(PlaceOfAttention place) => new PlaceOfAttentionDTO
        {
            Id = place.Id,
            Name = place.Name
        };
    }

}
