using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiametersRepository : IRepository<DiametersDTO, int>
    {
        private readonly SGISContext _context;

        public DiametersRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiametersDTO>> GetAllAsync()
        {
            var entities = await _context.Diameters.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<DiametersDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Diameters.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<DiametersDTO> AddAsync(DiametersDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Diameters.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<DiametersDTO?> UpdateAsync(DiametersDTO dto)
        {
            var entity = await _context.Diameters.FindAsync(dto.DiametersId);
            if (entity == null) return null;

            entity.Humerus = dto.Humerus;
            entity.Femur = dto.Femur;
            entity.Wrist = dto.Wrist;
            entity.MeasurementsId = dto.MeasurementsId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Diameters.FindAsync(id);
            if (entity == null) return false;

            _context.Diameters.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DiametersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            var entity = await _context.Diameters
                .FirstOrDefaultAsync(d => d.MeasurementsId == measurementsId);
            return entity != null ? MapToDto(entity) : null;
        }

        private DiametersDTO MapToDto(Diameters entity)
        {
            return new DiametersDTO
            {
                DiametersId = entity.DiametersId,
                Humerus = entity.Humerus,
                Femur = entity.Femur,
                Wrist = entity.Wrist,
                MeasurementsId = entity.MeasurementsId
            };
        }

        private Diameters MapToEntity(DiametersDTO dto)
        {
            return new Diameters
            {
                DiametersId = dto.DiametersId,
                Humerus = dto.Humerus,
                Femur = dto.Femur,
                Wrist = dto.Wrist,
                MeasurementsId = dto.MeasurementsId
            };
        }
    }
}