using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class AdvanceRepository : IRepository<AdvanceDTO, int>
    {
        private readonly SGISContext _context;

        public AdvanceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<AdvanceDTO>> GetAllAsync()
        {
            var entities = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<AdvanceDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .FirstOrDefaultAsync(a => a.AdvanceId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<AdvanceDTO> AddAsync(AdvanceDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Advances.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data
            await _context.Entry(entity)
                .Reference(a => a.Session)
                .Query()
                .Include(s => s.MedicalCare)
                .ThenInclude(mc => mc.Patient)
                .LoadAsync();

            await _context.Entry(entity)
                .Reference(a => a.PsychologySession)
                .Query()
                .Include(ps => ps.MedicalCare)
                .ThenInclude(mc => mc.Patient)
                .LoadAsync();

            return MapToDto(entity);
        }

        public async Task<AdvanceDTO?> UpdateAsync(AdvanceDTO dto)
        {
            var entity = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .FirstOrDefaultAsync(a => a.AdvanceId == dto.AdvanceId);

            if (entity == null) return null;

            entity.SessionId = dto.SessionId;
            entity.PsychologySessionId = dto.PsychologySessionId;
            entity.Indications = dto.Indications;
            entity.Description = dto.Description;
            entity.Date = dto.Date;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Advances.FindAsync(id);
            if (entity == null) return false;

            _context.Advances.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AdvanceDTO>> GetBySessionIdAsync(int sessionId)
        {
            var entities = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Where(a => a.SessionId == sessionId)
                .OrderBy(a => a.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<AdvanceDTO>> GetByPsychologySessionIdAsync(int psychologySessionId)
        {
            var entities = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Where(a => a.PsychologySessionId == psychologySessionId)
                .OrderBy(a => a.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<AdvanceDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var entities = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Where(a => a.Date >= startDate && a.Date <= endDate)
                .OrderBy(a => a.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<AdvanceDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entities = await _context.Advances
                .Include(a => a.Session)
                    .ThenInclude(s => s.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(a => a.PsychologySession)
                    .ThenInclude(ps => ps.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Where(a => a.Session.MedicalCareId == medicalCareId ||
                           a.PsychologySession.MedicalCareId == medicalCareId)
                .OrderBy(a => a.Date)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private AdvanceDTO MapToDto(Advance entity)
        {
            string? sessionDescription = null;
            string? psychologySessionDescription = null;
            int? medicalCareId = null;
            string? patientName = null;

            if (entity.Session != null)
            {
                sessionDescription = entity.Session.Description;
                medicalCareId = entity.Session.MedicalCareId;
                patientName = entity.Session.MedicalCare?.Patient?.PersonNavigation.FirstName;
            }
            else if (entity.PsychologySession != null)
            {
                psychologySessionDescription = entity.PsychologySession.Description;
                medicalCareId = entity.PsychologySession.MedicalCareId;
                patientName = entity.PsychologySession.MedicalCare?.Patient?.PersonNavigation.FirstName;
            }

            return new AdvanceDTO
            {
                AdvanceId = entity.AdvanceId,
                SessionId = entity.SessionId,
                PsychologySessionId = entity.PsychologySessionId,
                Indications = entity.Indications,
                Description = entity.Description,
                Date = entity.Date,
                SessionDescription = sessionDescription,
                PsychologySessionDescription = psychologySessionDescription,
                MedicalCareId = medicalCareId,
                PatientName = patientName
            };
        }

        private Advance MapToEntity(AdvanceDTO dto)
        {
            return new Advance
            {
                AdvanceId = dto.AdvanceId,
                SessionId = dto.SessionId,
                PsychologySessionId = dto.PsychologySessionId,
                Indications = dto.Indications,
                Description = dto.Description,
                Date = dto.Date
            };
        }
    }
}