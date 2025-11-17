using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PsychologySessionsRepository : IRepository<PsychologySessionsDTO, int>
    {
        private readonly SGISContext _context;

        public PsychologySessionsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PsychologySessionsDTO>> GetAllAsync()
        {
            var entities = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PsychologySessionsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .FirstOrDefaultAsync(ps => ps.PsychologySessionsId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PsychologySessionsDTO> AddAsync(PsychologySessionsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.PsychologySessions.Add(entity);
            await _context.SaveChangesAsync();

            // Recargar con datos relacionados
            await _context.Entry(entity)
                .Reference(ps => ps.MedicalCare)
                .Query()
                .Include(mc => mc.Patient)
                .ThenInclude(p => p.PersonNavigation)
                .LoadAsync();

            await _context.Entry(entity)
                .Collection(ps => ps.Activities)
                .LoadAsync();

            await _context.Entry(entity)
                .Collection(ps => ps.Advances)
                .LoadAsync();

            return MapToDto(entity);
        }

        public async Task<PsychologySessionsDTO?> UpdateAsync(PsychologySessionsDTO dto)
        {
            var entity = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .FirstOrDefaultAsync(ps => ps.PsychologySessionsId == dto.PsychologySessionsId);

            if (entity == null) return null;

            entity.Description = dto.Description;
            entity.Date = dto.Date;
            entity.MedicalDischarge = dto.MedicalDischarge;
            entity.Observations = dto.Observations;
            entity.VoluntaryRegistrationLink = dto.VoluntaryRegistrationLink;
            entity.SummarySession = dto.SummarySession;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PsychologySessions.FindAsync(id);
            if (entity == null) return false;

            _context.PsychologySessions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos adicionales específicos de PsychologySessions
        public async Task<List<PsychologySessionsDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entities = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .Where(ps => ps.MedicalCareId == medicalCareId)
                .OrderBy(ps => ps.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<PsychologySessionsDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var entities = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .Where(ps => ps.Date >= startDate && ps.Date <= endDate)
                .OrderBy(ps => ps.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<PsychologySessionsDTO>> GetByMedicalDischargeAsync(bool medicalDischarge)
        {
            var entities = await _context.PsychologySessions
                .Include(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(ps => ps.Activities)
                .Include(ps => ps.Advances)
                .Where(ps => ps.MedicalDischarge == medicalDischarge)
                .OrderBy(ps => ps.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos - CORREGIDOS siguiendo el patrón de TherapeuticPlan
        // =========================
        private PsychologySessionsDTO MapToDto(PsychologySessions entity)
        {
            return new PsychologySessionsDTO
            {
                PsychologySessionsId = entity.PsychologySessionsId,
                Description = entity.Description,
                Date = entity.Date,
                MedicalDischarge = entity.MedicalDischarge,
                Observations = entity.Observations,
                VoluntaryRegistrationLink = entity.VoluntaryRegistrationLink,
                SummarySession = entity.SummarySession,
                MedicalCareId = entity.MedicalCareId,
                // SIGUIENDO EL PATRÓN DE THERAPEUTIC PLAN:
                PatientName = entity.MedicalCare?.Patient?.PersonNavigation?.FirstName + " " +
                             entity.MedicalCare?.Patient?.PersonNavigation?.LastName,
                Activities = entity.Activities?.Select(a => new ActivityDTO
                {
                    ActivityId = a.ActivityId,
                    NameActivity = a.NameActivity,
                    DateActivity = a.DateActivity
                }).ToList() ?? new List<ActivityDTO>(),
                Advances = entity.Advances?.Select(a => new AdvanceDTO
                {
                    AdvanceId = a.AdvanceId,
                    Indications = a.Indications,
                    Description = a.Description,
                    Date = a.Date
                }).ToList() ?? new List<AdvanceDTO>()
            };
        }

        private PsychologySessions MapToEntity(PsychologySessionsDTO dto)
        {
            return new PsychologySessions
            {
                PsychologySessionsId = dto.PsychologySessionsId,
                Description = dto.Description,
                Date = dto.Date,
                MedicalDischarge = dto.MedicalDischarge,
                Observations = dto.Observations,
                VoluntaryRegistrationLink = dto.VoluntaryRegistrationLink,
                SummarySession = dto.SummarySession,
                MedicalCareId = dto.MedicalCareId
            };
        }
    }
}