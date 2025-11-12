using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class ActivityRepository : IRepository<ActivityDTO, int>
    {
        private readonly SGISContext _context;

        public ActivityRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityDTO>> GetAllAsync()
        {
            var entities = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ActivityDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .FirstOrDefaultAsync(a => a.ActivityId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<ActivityDTO> AddAsync(ActivityDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Activities.Add(entity);
            await _context.SaveChangesAsync();

            // Recargar con datos relacionados
            await _context.Entry(entity)
                .Reference(a => a.Session)
                .LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.PsychologySession)
                .LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.TypeOfActivity)
                .LoadAsync();

            return MapToDto(entity);
        }

        public async Task<ActivityDTO?> UpdateAsync(ActivityDTO dto)
        {
            var entity = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .FirstOrDefaultAsync(a => a.ActivityId == dto.ActivityId);

            if (entity == null) return null;

            entity.NameActivity = dto.NameActivity;
            entity.DateActivity = dto.DateActivity;
            entity.SessionId = dto.SessionId;
            entity.PsychologySessionId = dto.PsychologySessionId;
            entity.TypeOfActivityId = dto.TypeOfActivityId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Activities.FindAsync(id);
            if (entity == null) return false;

            _context.Activities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos adicionales específicos de Activity
        public async Task<List<ActivityDTO>> GetBySessionIdAsync(int sessionId)
        {
            var entities = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .Where(a => a.SessionId == sessionId)
                .OrderBy(a => a.DateActivity)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<ActivityDTO>> GetByPsychologySessionIdAsync(int psychologySessionId)
        {
            var entities = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .Where(a => a.PsychologySessionId == psychologySessionId)
                .OrderBy(a => a.DateActivity)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<ActivityDTO>> GetByTypeOfActivityIdAsync(int typeOfActivityId)
        {
            var entities = await _context.Activities
                .Include(a => a.Session)
                .Include(a => a.PsychologySession)
                .Include(a => a.TypeOfActivity)
                .Where(a => a.TypeOfActivityId == typeOfActivityId)
                .OrderBy(a => a.DateActivity)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private ActivityDTO MapToDto(Activity entity)
        {
            return new ActivityDTO
            {
                ActivityId = entity.ActivityId,
                NameActivity = entity.NameActivity,
                DateActivity = entity.DateActivity,
                SessionId = entity.SessionId,
                PsychologySessionId = entity.PsychologySessionId,
                TypeOfActivityId = entity.TypeOfActivityId,
                SessionDescription = entity.Session?.Description,
                PsychologySessionDescription = entity.PsychologySession?.Description,
                TypeOfActivityName = entity.TypeOfActivity?.Name
            };
        }

        private Activity MapToEntity(ActivityDTO dto)
        {
            return new Activity
            {
                ActivityId = dto.ActivityId,
                NameActivity = dto.NameActivity,
                DateActivity = dto.DateActivity,
                SessionId = dto.SessionId,
                PsychologySessionId = dto.PsychologySessionId,
                TypeOfActivityId = dto.TypeOfActivityId
            };
        }
    }
}