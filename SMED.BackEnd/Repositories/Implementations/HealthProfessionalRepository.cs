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
                .Where(h => h.HealthProfessionalTypeId != null && h.RegistrationNumber != null)
                .Include(h => h.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
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
                NameTypeProfessional = h.HealthProfessionalTypeNavigation?.Name ?? "Sin tipo",
                DocumentNumber = h.PersonNavigation?.PersonDocument?.DocumentNumber,
                Email = h.PersonNavigation?.Email
            }).ToList();
        }

        public async Task<HealthProfessionalDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.HealthProfessionals
                .Include(h => h.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                .Include(h => h.HealthProfessionalTypeNavigation)
                .FirstOrDefaultAsync(h => h.HealthProfessionalId == id);

            if (entity == null) return null;

            return new HealthProfessionalDTO
            {
                HealthProfessionalId = entity.HealthProfessionalId,
                HealthProfessionalTypeId = entity.HealthProfessionalTypeId,
                RegistrationNumber = entity.RegistrationNumber,
                FullName = entity.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        entity.PersonNavigation.FirstName,
                        entity.PersonNavigation.MiddleName,
                        entity.PersonNavigation.LastName,
                        entity.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : "Sin nombre",
                NameTypeProfessional = entity.HealthProfessionalTypeNavigation?.Name ?? "Sin tipo",
                DocumentNumber = entity.PersonNavigation?.PersonDocument?.DocumentNumber,
                Email = entity.PersonNavigation?.Email
            };
        }

        // ✅ NUEVO MÉTODO PARA BÚSQUEDA
        public async Task<List<HealthProfessionalDTO>> SearchAsync(string searchTerm)
        {
            var query = _context.HealthProfessionals
                .Where(h => h.HealthProfessionalTypeId != null && h.RegistrationNumber != null)
                .Include(h => h.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                .Include(h => h.HealthProfessionalTypeNavigation)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(h =>
                    (h.PersonNavigation.FirstName != null && h.PersonNavigation.FirstName.ToLower().Contains(lowerSearchTerm)) ||
                    (h.PersonNavigation.MiddleName != null && h.PersonNavigation.MiddleName.ToLower().Contains(lowerSearchTerm)) ||
                    (h.PersonNavigation.LastName != null && h.PersonNavigation.LastName.ToLower().Contains(lowerSearchTerm)) ||
                    (h.PersonNavigation.SecondLastName != null && h.PersonNavigation.SecondLastName.ToLower().Contains(lowerSearchTerm)) ||
                    (h.RegistrationNumber != null && h.RegistrationNumber.ToLower().Contains(lowerSearchTerm)) ||
                    (h.HealthProfessionalTypeNavigation.Name != null && h.HealthProfessionalTypeNavigation.Name.ToLower().Contains(lowerSearchTerm)) ||
                    (h.PersonNavigation.PersonDocument.DocumentNumber != null && h.PersonNavigation.PersonDocument.DocumentNumber.Contains(searchTerm))
                );
            }

            var professionals = await query.ToListAsync();

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
                NameTypeProfessional = h.HealthProfessionalTypeNavigation?.Name ?? "Sin tipo",
                DocumentNumber = h.PersonNavigation?.PersonDocument?.DocumentNumber,
                Email = h.PersonNavigation?.Email
            }).OrderBy(h => h.FullName).ToList();
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
