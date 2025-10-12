using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalServiceRepository : IRepository<MedicalServiceDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalServiceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalServiceDTO>> GetAllAsync()
        {
            var entities = await _context.MedicalServices
                .Include(ms => ms.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<MedicalServiceDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MedicalServices
                .Include(ms => ms.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .FirstOrDefaultAsync(ms => ms.ServiceId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MedicalServiceDTO> AddAsync(MedicalServiceDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MedicalServices.Add(entity);
            await _context.SaveChangesAsync();

            // Recargar la entidad con las relaciones para obtener el nombre
            var savedEntity = await _context.MedicalServices
                .Include(ms => ms.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .FirstOrDefaultAsync(ms => ms.ServiceId == entity.ServiceId);

            return MapToDto(savedEntity!);
        }

        public async Task<MedicalServiceDTO?> UpdateAsync(MedicalServiceDTO dto)
        {
            var entity = await _context.MedicalServices
                .Include(ms => ms.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .FirstOrDefaultAsync(ms => ms.ServiceId == dto.ServiceId);

            if (entity == null) return null;

            entity.CareId = dto.CareId;
            entity.ServiceDate = dto.ServiceDate;
            entity.ServiceType = dto.ServiceType;
            entity.Diagnosis = dto.Diagnosis;
            entity.Observations = dto.Observations;
            entity.Recommendations = dto.Recommendations;
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalServices.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalServices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapeo manual actualizado
        private MedicalServiceDTO MapToDto(MedicalService entity)
        {
            return new MedicalServiceDTO
            {
                ServiceId = entity.ServiceId,
                CareId = entity.CareId,
                ServiceDate = entity.ServiceDate,
                ServiceType = entity.ServiceType,
                Diagnosis = entity.Diagnosis,
                Observations = entity.Observations,
                Recommendations = entity.Recommendations,
                PatientId = entity.PatientId,
                HealthProfessionalId = entity.HealthProfessionalId,
                HealthProfessionalName = GetHealthProfessionalName(entity.HealthProfessional)
            };
        }

        private MedicalService MapToEntity(MedicalServiceDTO dto)
        {
            return new MedicalService
            {
                ServiceId = dto.ServiceId,
                CareId = dto.CareId,
                ServiceDate = dto.ServiceDate,
                ServiceType = dto.ServiceType,
                Diagnosis = dto.Diagnosis,
                Observations = dto.Observations,
                Recommendations = dto.Recommendations,
                PatientId = dto.PatientId,
                HealthProfessionalId = dto.HealthProfessionalId
            };
        }

        private string GetHealthProfessionalName(HealthProfessional? healthProfessional)
        {
            if (healthProfessional?.PersonNavigation == null)
                return string.Empty;

            var person = healthProfessional.PersonNavigation;
            var names = new List<string> { person.FirstName, person.MiddleName, person.LastName, person.SecondLastName };
            return string.Join(" ", names.Where(name => !string.IsNullOrEmpty(name)));
        }
    }
}