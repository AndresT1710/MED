using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class HealthProfessionalRepository : IRepository<HealthProfessionalDTO, int>
    {
        private readonly SGISContext _context;
        public HealthProfessionalRepository(SGISContext context) => _context = context;

        public async Task<List<HealthProfessionalDTO>> GetAllAsync()
        {
            var professionals = await _context.HealthProfessionals
                .Where(h => h.HealthProfessionalTypeId != null && h.RegistrationNumber != null) // Solo los profesionales reales
                .Include(h => h.PersonNavigation)
                .Include(h => h.HealthProfessionalTypeNavigation)
                .ToListAsync();

            return professionals.Select(h => new HealthProfessionalDTO
            {
                HealthProfessionalId = h.HealthProfessionalId,
                HealthProfessionalTypeId = h.HealthProfessionalTypeId,
                RegistrationNumber = h.RegistrationNumber,
                FullName = h.PersonNavigation != null
                    ? string.Join(" ", new[] {
                h.PersonNavigation.FirstName,
                h.PersonNavigation.MiddleName,
                h.PersonNavigation.LastName,
                h.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : "Sin nombre",
                NameTypeProfessional = h.HealthProfessionalTypeNavigation?.Name ?? "Sin tipo"
            }).ToList();
        }


        public async Task<HealthProfessionalDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.HealthProfessionals.FindAsync(id);
            return entity == null ? null : new HealthProfessionalDTO
            {
                HealthProfessionalId = entity.HealthProfessionalId,
                HealthProfessionalTypeId = entity.HealthProfessionalTypeId,
                RegistrationNumber = entity.RegistrationNumber
            };
        }

        public async Task<HealthProfessionalDTO> AddAsync(HealthProfessionalDTO dto)
        {
            var entity = new HealthProfessional
            {
                HealthProfessionalId = dto.HealthProfessionalId,
                HealthProfessionalTypeId = dto.HealthProfessionalTypeId,
                RegistrationNumber = dto.RegistrationNumber
            };
            _context.HealthProfessionals.Add(entity);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<HealthProfessionalDTO?> UpdateAsync(HealthProfessionalDTO dto)
        {
            var entity = await _context.HealthProfessionals.FindAsync(dto.HealthProfessionalId);
            if (entity == null) return null;
            entity.HealthProfessionalTypeId = dto.HealthProfessionalTypeId;
            entity.RegistrationNumber = dto.RegistrationNumber;
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.HealthProfessionals.FindAsync(id);
            if (entity == null) return false;
            _context.HealthProfessionals.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
