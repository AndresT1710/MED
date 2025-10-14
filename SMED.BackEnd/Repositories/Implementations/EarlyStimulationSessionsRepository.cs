using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class EarlyStimulationSessionsRepository : IRepository<EarlyStimulationSessionsDTO, int>
    {
        private readonly SGISContext _context;

        public EarlyStimulationSessionsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<EarlyStimulationSessionsDTO>> GetAllAsync()
        {
            var entities = await _context.EarlyStimulationSessions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<EarlyStimulationSessionsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.EarlyStimulationSessions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<EarlyStimulationSessionsDTO> AddAsync(EarlyStimulationSessionsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.EarlyStimulationSessions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<EarlyStimulationSessionsDTO?> UpdateAsync(EarlyStimulationSessionsDTO dto)
        {
            var entity = await _context.EarlyStimulationSessions.FindAsync(dto.SessionsId);
            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.Date = dto.Date;
            entity.Treatment = dto.Treatment;
            entity.MedicalDischarge = dto.MedicalDischarge;
            entity.Observations = dto.Observations;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EarlyStimulationSessions.FindAsync(id);
            if (entity == null) return false;

            _context.EarlyStimulationSessions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Mapping
        private EarlyStimulationSessionsDTO MapToDto(EarlyStimulationSessions entity) => new EarlyStimulationSessionsDTO
        {
            SessionsId = entity.SessionsId,
            Description = entity.Description,
            Date = entity.Date,
            Treatment = entity.Treatment,
            MedicalDischarge = entity.MedicalDischarge,
            Observations = entity.Observations,
            MedicalCareId = entity.MedicalCareId
        };

        private EarlyStimulationSessions MapToEntity(EarlyStimulationSessionsDTO dto) => new EarlyStimulationSessions
        {
            SessionsId = dto.SessionsId,
            Description = dto.Description,
            Date = dto.Date,
            Treatment = dto.Treatment,
            MedicalDischarge = dto.MedicalDischarge,
            Observations = dto.Observations,
            MedicalCareId = dto.MedicalCareId
        };
    }
}
