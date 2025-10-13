using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ReviewSystemDevicesRepository : IRepository<ReviewSystemDevicesDTO, int>
    {
        private readonly SGISContext _context;

        public ReviewSystemDevicesRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewSystemDevicesDTO>> GetAllAsync()
        {
            var reviews = await _context.ReviewSystemDevices
                .Include(r => r.SystemsDevices) // Incluimos la relación con SystemsDevices
                .ToListAsync();

            return reviews.Select(MapToDto).ToList();
        }

        public async Task<ReviewSystemDevicesDTO?> GetByIdAsync(int id)
        {
            var review = await _context.ReviewSystemDevices
                .Include(r => r.SystemsDevices) // Incluimos también al obtener por ID
                .FirstOrDefaultAsync(r => r.Id == id);

            return review == null ? null : MapToDto(review);
        }

        public async Task<ReviewSystemDevicesDTO> AddAsync(ReviewSystemDevicesDTO dto)
        {
            var entity = new ReviewSystemDevices
            {
                State = dto.State,
                Observations = dto.Observations,
                SystemsDevicesId = dto.SystemsDevicesId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.ReviewSystemDevices.Add(entity);
            await _context.SaveChangesAsync();

            // Cargamos el sistema para obtener el nombre
            var system = await _context.SystemsDevices.FindAsync(dto.SystemsDevicesId);
            dto.Id = entity.Id;
            dto.SystemName = system?.Name ?? string.Empty;

            return dto;
        }

        public async Task<ReviewSystemDevicesDTO?> UpdateAsync(ReviewSystemDevicesDTO dto)
        {
            var entity = await _context.ReviewSystemDevices.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.State = dto.State;
            entity.Observations = dto.Observations;
            entity.SystemsDevicesId = dto.SystemsDevicesId;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            // Actualizamos también el nombre del sistema
            var system = await _context.SystemsDevices.FindAsync(dto.SystemsDevicesId);
            dto.SystemName = system?.Name ?? string.Empty;

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ReviewSystemDevices.FindAsync(id);
            if (entity == null) return false;

            _context.ReviewSystemDevices.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ReviewSystemDevicesDTO MapToDto(ReviewSystemDevices review) => new ReviewSystemDevicesDTO
        {
            Id = review.Id,
            State = review.State,
            Observations = review.Observations,
            SystemsDevicesId = review.SystemsDevicesId,
            SystemName = review.SystemsDevices?.Name ?? string.Empty, // <- Trae el nombre del sistema
            MedicalCareId = review.MedicalCareId
        };
    }
}
