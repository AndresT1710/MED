using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PerimetersRepository : IRepository<PerimetersDTO, int>
    {
        private readonly SGISContext _context;

        public PerimetersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PerimetersDTO>> GetAllAsync()
        {
            var entities = await _context.Perimeters.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<PerimetersDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Perimeters.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<PerimetersDTO> AddAsync(PerimetersDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Perimeters.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<PerimetersDTO?> UpdateAsync(PerimetersDTO dto)
        {
            var entity = await _context.Perimeters.FindAsync(dto.PerimetersId);
            if (entity == null) return null;

            entity.Cephalic = dto.Cephalic;
            entity.Neck = dto.Neck;
            entity.RelaxedArmHalf = dto.RelaxedArmHalf;
            entity.Forearm = dto.Forearm;
            entity.Wrist = dto.Wrist;
            entity.MeasurementsId = dto.MeasurementsId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Perimeters.FindAsync(id);
            if (entity == null) return false;

            _context.Perimeters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PerimetersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            var entity = await _context.Perimeters
                .FirstOrDefaultAsync(p => p.MeasurementsId == measurementsId);
            return entity != null ? MapToDto(entity) : null;
        }

        private PerimetersDTO MapToDto(Perimeters entity)
        {
            return new PerimetersDTO
            {
                PerimetersId = entity.PerimetersId,
                Cephalic = entity.Cephalic,
                Neck = entity.Neck,
                RelaxedArmHalf = entity.RelaxedArmHalf,
                Forearm = entity.Forearm,
                Wrist = entity.Wrist,
                MeasurementsId = entity.MeasurementsId
            };
        }

        private Perimeters MapToEntity(PerimetersDTO dto)
        {
            return new Perimeters
            {
                PerimetersId = dto.PerimetersId,
                Cephalic = dto.Cephalic,
                Neck = dto.Neck,
                RelaxedArmHalf = dto.RelaxedArmHalf,
                Forearm = dto.Forearm,
                Wrist = dto.Wrist,
                MeasurementsId = dto.MeasurementsId
            };
        }
    }
}