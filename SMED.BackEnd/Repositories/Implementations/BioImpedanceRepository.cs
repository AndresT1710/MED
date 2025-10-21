using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class BioImpedanceRepository : IRepository<BioImpedanceDTO, int>
    {
        private readonly SGISContext _context;

        public BioImpedanceRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<BioImpedanceDTO>> GetAllAsync()
        {
            var entities = await _context.BioImpedances.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<BioImpedanceDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.BioImpedances.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<BioImpedanceDTO> AddAsync(BioImpedanceDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.BioImpedances.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<BioImpedanceDTO?> UpdateAsync(BioImpedanceDTO dto)
        {
            var entity = await _context.BioImpedances.FindAsync(dto.BioImpedanceId);
            if (entity == null) return null;

            entity.BodyFatPercentage = dto.BodyFatPercentage;
            entity.UpperSectionFatPercentage = dto.UpperSectionFatPercentage;
            entity.LowerSectionFatPercentage = dto.LowerSectionFatPercentage;
            entity.VisceralFat = dto.VisceralFat;
            entity.MuscleMass = dto.MuscleMass;
            entity.BoneWeight = dto.BoneWeight;
            entity.BodyWater = dto.BodyWater;
            entity.MeasurementsId = dto.MeasurementsId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BioImpedances.FindAsync(id);
            if (entity == null) return false;

            _context.BioImpedances.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BioImpedanceDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            var entity = await _context.BioImpedances
                .FirstOrDefaultAsync(bi => bi.MeasurementsId == measurementsId);
            return entity != null ? MapToDto(entity) : null;
        }

        private BioImpedanceDTO MapToDto(BioImpedance entity)
        {
            return new BioImpedanceDTO
            {
                BioImpedanceId = entity.BioImpedanceId,
                BodyFatPercentage = entity.BodyFatPercentage,
                UpperSectionFatPercentage = entity.UpperSectionFatPercentage,
                LowerSectionFatPercentage = entity.LowerSectionFatPercentage,
                VisceralFat = entity.VisceralFat,
                MuscleMass = entity.MuscleMass,
                BoneWeight = entity.BoneWeight,
                BodyWater = entity.BodyWater,
                MeasurementsId = entity.MeasurementsId
            };
        }

        private BioImpedance MapToEntity(BioImpedanceDTO dto)
        {
            return new BioImpedance
            {
                BioImpedanceId = dto.BioImpedanceId,
                BodyFatPercentage = dto.BodyFatPercentage,
                UpperSectionFatPercentage = dto.UpperSectionFatPercentage,
                LowerSectionFatPercentage = dto.LowerSectionFatPercentage,
                VisceralFat = dto.VisceralFat,
                MuscleMass = dto.MuscleMass,
                BoneWeight = dto.BoneWeight,
                BodyWater = dto.BodyWater,
                MeasurementsId = dto.MeasurementsId
            };
        }
    }
}