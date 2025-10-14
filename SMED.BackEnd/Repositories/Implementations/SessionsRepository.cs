using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SessionsRepository : IRepository<SessionsDTO, int>
    {
        private readonly SGISContext _context;

        public SessionsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SessionsDTO>> GetAllAsync()
        {
            var entities = await _context.Sessions
                .Include(s => s.MedicalCare)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<SessionsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Sessions
                .Include(s => s.MedicalCare)
                .FirstOrDefaultAsync(s => s.SessionsId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SessionsDTO> AddAsync(SessionsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Sessions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SessionsDTO?> UpdateAsync(SessionsDTO dto)
        {
            var entity = await _context.Sessions.FindAsync(dto.SessionsId);
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
            var entity = await _context.Sessions.FindAsync(id);
            if (entity == null) return false;

            _context.Sessions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SessionsDTO>> GetByCareIdAsync(int medicalCareId)
        {
            var entities = await _context.Sessions
                .Where(s => s.MedicalCareId == medicalCareId)
                .OrderByDescending(s => s.Date) 
                .ThenByDescending(s => s.SessionsId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private SessionsDTO MapToDto(Sessions entity)
        {
            return new SessionsDTO
            {
                SessionsId = entity.SessionsId,
                Description = entity.Description,
                Date = entity.Date,
                Treatment = entity.Treatment,
                MedicalDischarge = entity.MedicalDischarge,
                Observations = entity.Observations,
                MedicalCareId = entity.MedicalCareId
            };
        }

        private Sessions MapToEntity(SessionsDTO dto)
        {
            return new Sessions
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
}
