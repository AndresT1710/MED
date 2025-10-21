using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MeasurementsRepository : IRepository<MeasurementsDTO, int>
    {
        private readonly SGISContext _context;

        public MeasurementsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MeasurementsDTO>> GetAllAsync()
        {
            var entities = await _context.Measurements
                .Include(m => m.SkinFolds)
                .Include(m => m.BioImpedance)
                .Include(m => m.Perimeters)
                .Include(m => m.Diameters)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<MeasurementsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Measurements
                .Include(m => m.SkinFolds)
                .Include(m => m.BioImpedance)
                .Include(m => m.Perimeters)
                .Include(m => m.Diameters)
                .FirstOrDefaultAsync(m => m.MeasurementsId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MeasurementsDTO> AddAsync(MeasurementsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Measurements.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MeasurementsDTO?> UpdateAsync(MeasurementsDTO dto)
        {
            var entity = await _context.Measurements
                .Include(m => m.SkinFolds)
                .Include(m => m.BioImpedance)
                .Include(m => m.Perimeters)
                .Include(m => m.Diameters)
                .FirstOrDefaultAsync(m => m.MeasurementsId == dto.MeasurementsId);

            if (entity == null) return null;

            entity.MedicalCareId = dto.MedicalCareId;

            // Actualizar relaciones si existen
            if (dto.SkinFolds != null)
            {
                if (entity.SkinFolds != null)
                {
                    entity.SkinFolds.Subscapular = dto.SkinFolds.Subscapular;
                    entity.SkinFolds.Triceps = dto.SkinFolds.Triceps;
                    entity.SkinFolds.Biceps = dto.SkinFolds.Biceps;
                    entity.SkinFolds.IliacCrest = dto.SkinFolds.IliacCrest;
                    entity.SkinFolds.Supraespinal = dto.SkinFolds.Supraespinal;
                    entity.SkinFolds.Abdominal = dto.SkinFolds.Abdominal;
                    entity.SkinFolds.FrontalThigh = dto.SkinFolds.FrontalThigh;
                    entity.SkinFolds.MedialCalf = dto.SkinFolds.MedialCalf;
                    entity.SkinFolds.MedialAxillary = dto.SkinFolds.MedialAxillary;
                    entity.SkinFolds.Pectoral = dto.SkinFolds.Pectoral;
                }
                else
                {
                    entity.SkinFolds = MapToEntity(dto.SkinFolds);
                }
            }

            if (dto.BioImpedance != null)
            {
                if (entity.BioImpedance != null)
                {
                    entity.BioImpedance.BodyFatPercentage = dto.BioImpedance.BodyFatPercentage;
                    entity.BioImpedance.UpperSectionFatPercentage = dto.BioImpedance.UpperSectionFatPercentage;
                    entity.BioImpedance.LowerSectionFatPercentage = dto.BioImpedance.LowerSectionFatPercentage;
                    entity.BioImpedance.VisceralFat = dto.BioImpedance.VisceralFat;
                    entity.BioImpedance.MuscleMass = dto.BioImpedance.MuscleMass;
                    entity.BioImpedance.BoneWeight = dto.BioImpedance.BoneWeight;
                    entity.BioImpedance.BodyWater = dto.BioImpedance.BodyWater;
                }
                else
                {
                    entity.BioImpedance = MapToEntity(dto.BioImpedance);
                }
            }

            if (dto.Perimeters != null)
            {
                if (entity.Perimeters != null)
                {
                    entity.Perimeters.Cephalic = dto.Perimeters.Cephalic;
                    entity.Perimeters.Neck = dto.Perimeters.Neck;
                    entity.Perimeters.RelaxedArmHalf = dto.Perimeters.RelaxedArmHalf;
                    entity.Perimeters.Forearm = dto.Perimeters.Forearm;
                    entity.Perimeters.Wrist = dto.Perimeters.Wrist;
                }
                else
                {
                    entity.Perimeters = MapToEntity(dto.Perimeters);
                }
            }

            if (dto.Diameters != null)
            {
                if (entity.Diameters != null)
                {
                    entity.Diameters.Humerus = dto.Diameters.Humerus;
                    entity.Diameters.Femur = dto.Diameters.Femur;
                    entity.Diameters.Wrist = dto.Diameters.Wrist;
                }
                else
                {
                    entity.Diameters = MapToEntity(dto.Diameters);
                }
            }

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Measurements.FindAsync(id);
            if (entity == null) return false;

            _context.Measurements.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MeasurementsDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entity = await _context.Measurements
                .Include(m => m.SkinFolds)
                .Include(m => m.BioImpedance)
                .Include(m => m.Perimeters)
                .Include(m => m.Diameters)
                .FirstOrDefaultAsync(m => m.MedicalCareId == medicalCareId);

            return entity != null ? MapToDto(entity) : null;
        }

        private MeasurementsDTO MapToDto(Measurements entity)
        {
            return new MeasurementsDTO
            {
                MeasurementsId = entity.MeasurementsId,
                MedicalCareId = entity.MedicalCareId,
                SkinFolds = entity.SkinFolds != null ? new SkinFoldsDTO
                {
                    SkinFoldsId = entity.SkinFolds.SkinFoldsId,
                    Subscapular = entity.SkinFolds.Subscapular,
                    Triceps = entity.SkinFolds.Triceps,
                    Biceps = entity.SkinFolds.Biceps,
                    IliacCrest = entity.SkinFolds.IliacCrest,
                    Supraespinal = entity.SkinFolds.Supraespinal,
                    Abdominal = entity.SkinFolds.Abdominal,
                    FrontalThigh = entity.SkinFolds.FrontalThigh,
                    MedialCalf = entity.SkinFolds.MedialCalf,
                    MedialAxillary = entity.SkinFolds.MedialAxillary,
                    Pectoral = entity.SkinFolds.Pectoral,
                    MeasurementsId = entity.SkinFolds.MeasurementsId
                } : null,
                BioImpedance = entity.BioImpedance != null ? new BioImpedanceDTO
                {
                    BioImpedanceId = entity.BioImpedance.BioImpedanceId,
                    BodyFatPercentage = entity.BioImpedance.BodyFatPercentage,
                    UpperSectionFatPercentage = entity.BioImpedance.UpperSectionFatPercentage,
                    LowerSectionFatPercentage = entity.BioImpedance.LowerSectionFatPercentage,
                    VisceralFat = entity.BioImpedance.VisceralFat,
                    MuscleMass = entity.BioImpedance.MuscleMass,
                    BoneWeight = entity.BioImpedance.BoneWeight,
                    BodyWater = entity.BioImpedance.BodyWater,
                    MeasurementsId = entity.BioImpedance.MeasurementsId
                } : null,
                Perimeters = entity.Perimeters != null ? new PerimetersDTO
                {
                    PerimetersId = entity.Perimeters.PerimetersId,
                    Cephalic = entity.Perimeters.Cephalic,
                    Neck = entity.Perimeters.Neck,
                    RelaxedArmHalf = entity.Perimeters.RelaxedArmHalf,
                    Forearm = entity.Perimeters.Forearm,
                    Wrist = entity.Perimeters.Wrist,
                    MeasurementsId = entity.Perimeters.MeasurementsId
                } : null,
                Diameters = entity.Diameters != null ? new DiametersDTO
                {
                    DiametersId = entity.Diameters.DiametersId,
                    Humerus = entity.Diameters.Humerus,
                    Femur = entity.Diameters.Femur,
                    Wrist = entity.Diameters.Wrist,
                    MeasurementsId = entity.Diameters.MeasurementsId
                } : null
            };
        }

        private Measurements MapToEntity(MeasurementsDTO dto)
        {
            return new Measurements
            {
                MeasurementsId = dto.MeasurementsId,
                MedicalCareId = dto.MedicalCareId,
                SkinFolds = dto.SkinFolds != null ? MapToEntity(dto.SkinFolds) : null,
                BioImpedance = dto.BioImpedance != null ? MapToEntity(dto.BioImpedance) : null,
                Perimeters = dto.Perimeters != null ? MapToEntity(dto.Perimeters) : null,
                Diameters = dto.Diameters != null ? MapToEntity(dto.Diameters) : null
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