using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class SkinFoldsRepository : IRepository<SkinFoldsDTO, int>
    {
        private readonly SGISContext _context;

        public SkinFoldsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<SkinFoldsDTO>> GetAllAsync()
        {
            var entities = await _context.SkinFolds.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<SkinFoldsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.SkinFolds.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<SkinFoldsDTO> AddAsync(SkinFoldsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.SkinFolds.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<SkinFoldsDTO?> UpdateAsync(SkinFoldsDTO dto)
        {
            var entity = await _context.SkinFolds.FindAsync(dto.SkinFoldsId);
            if (entity == null) return null;

            entity.Subscapular = dto.Subscapular;
            entity.Triceps = dto.Triceps;
            entity.Biceps = dto.Biceps;
            entity.IliacCrest = dto.IliacCrest;
            entity.Supraespinal = dto.Supraespinal;
            entity.Abdominal = dto.Abdominal;
            entity.FrontalThigh = dto.FrontalThigh;
            entity.MedialCalf = dto.MedialCalf;
            entity.MedialAxillary = dto.MedialAxillary;
            entity.Pectoral = dto.Pectoral;
            entity.MeasurementsId = dto.MeasurementsId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.SkinFolds.FindAsync(id);
            if (entity == null) return false;

            _context.SkinFolds.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SkinFoldsDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            var entity = await _context.SkinFolds
                .FirstOrDefaultAsync(sf => sf.MeasurementsId == measurementsId);
            return entity != null ? MapToDto(entity) : null;
        }

        private SkinFoldsDTO MapToDto(SkinFolds entity)
        {
            return new SkinFoldsDTO
            {
                SkinFoldsId = entity.SkinFoldsId,
                Subscapular = entity.Subscapular,
                Triceps = entity.Triceps,
                Biceps = entity.Biceps,
                IliacCrest = entity.IliacCrest,
                Supraespinal = entity.Supraespinal,
                Abdominal = entity.Abdominal,
                FrontalThigh = entity.FrontalThigh,
                MedialCalf = entity.MedialCalf,
                MedialAxillary = entity.MedialAxillary,
                Pectoral = entity.Pectoral,
                MeasurementsId = entity.MeasurementsId
            };
        }

        private SkinFolds MapToEntity(SkinFoldsDTO dto)
        {
            return new SkinFolds
            {
                SkinFoldsId = dto.SkinFoldsId,
                Subscapular = dto.Subscapular,
                Triceps = dto.Triceps,
                Biceps = dto.Biceps,
                IliacCrest = dto.IliacCrest,
                Supraespinal = dto.Supraespinal,
                Abdominal = dto.Abdominal,
                FrontalThigh = dto.FrontalThigh,
                MedialCalf = dto.MedialCalf,
                MedialAxillary = dto.MedialAxillary,
                Pectoral = dto.Pectoral,
                MeasurementsId = dto.MeasurementsId
            };
        }
    }
}