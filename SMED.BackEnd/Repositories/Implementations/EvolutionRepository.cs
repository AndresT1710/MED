using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class EvolutionRepository : IRepository<EvolutionDTO, int>
    {
        private readonly SGISContext _context;

        public EvolutionRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<EvolutionDTO>> GetAllAsync()
        {
            var evolutions = await _context.Evolutions.ToListAsync();
            return evolutions.Select(MapToDto).ToList();
        }

        public async Task<EvolutionDTO?> GetByIdAsync(int id)
        {
            var evolution = await _context.Evolutions.FindAsync(id);
            return evolution == null ? null : MapToDto(evolution);
        }

        public async Task<EvolutionDTO> AddAsync(EvolutionDTO dto)
        {
            var entity = new Evolution
            {
                Description = dto.Description,
                Percentage = dto.Percentage,
                MedicalCareId = dto.MedicalCareId
            };

            _context.Evolutions.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<EvolutionDTO?> UpdateAsync(EvolutionDTO dto)
        {
            var entity = await _context.Evolutions.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.Percentage = dto.Percentage;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Evolutions.FindAsync(id);
            if (entity == null) return false;

            _context.Evolutions.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static EvolutionDTO MapToDto(Evolution evolution) => new EvolutionDTO
        {
            Id = evolution.Id,
            Description = evolution.Description,
            Percentage = evolution.Percentage,
            MedicalCareId = evolution.MedicalCareId
        };
    }

}
