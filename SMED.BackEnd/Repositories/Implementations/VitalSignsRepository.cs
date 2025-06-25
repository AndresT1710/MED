using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class VitalSignsRepository : IRepository<VitalSignsDTO, int>
    {
        private readonly SGISContext _context;

        public VitalSignsRepository(SGISContext context) => _context = context;

        public async Task<List<VitalSignsDTO>> GetAllAsync() =>
            await _context.VitalSigns
                .Select(v => new VitalSignsDTO
                {
                    Id = v.Id,
                    Weight = v.Weight,
                    Height = v.Height,
                    Icm = v.Icm,
                    AbdominalCircumference = v.AbdominalCircumference,
                    BloodPressure = v.BloodPressure,
                    Temperature = v.Temperature,
                    MeanArterialPressure = v.MeanArterialPressure,
                    HeartRate = v.HeartRate,
                    OxygenSaturation = v.OxygenSaturation,
                    RespiratoryRate = v.RespiratoryRate,
                    BloodGlucose = v.BloodGlucose,
                    Hemoglobin = v.Hemoglobin,
                    MedicalCareId = v.MedicalCareId
                })
                .ToListAsync();

        public async Task<VitalSignsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.VitalSigns.FindAsync(id);
            return entity == null ? null : new VitalSignsDTO
            {
                Id = entity.Id,
                Weight = entity.Weight,
                Height = entity.Height,
                Icm = entity.Icm,
                AbdominalCircumference = entity.AbdominalCircumference,
                BloodPressure = entity.BloodPressure,
                Temperature = entity.Temperature,
                MeanArterialPressure = entity.MeanArterialPressure,
                HeartRate = entity.HeartRate,
                OxygenSaturation = entity.OxygenSaturation,
                RespiratoryRate = entity.RespiratoryRate,
                BloodGlucose = entity.BloodGlucose,
                Hemoglobin = entity.Hemoglobin,
                MedicalCareId = entity.MedicalCareId
            };
        }

        public async Task<VitalSignsDTO> AddAsync(VitalSignsDTO dto)
        {
            var entity = new VitalSigns
            {
                Weight = dto.Weight,
                Height = dto.Height,
                Icm = dto.Icm,
                AbdominalCircumference = dto.AbdominalCircumference,
                BloodPressure = dto.BloodPressure,
                Temperature = dto.Temperature,
                MeanArterialPressure = dto.MeanArterialPressure,
                HeartRate = dto.HeartRate,
                OxygenSaturation = dto.OxygenSaturation,
                RespiratoryRate = dto.RespiratoryRate,
                BloodGlucose = dto.BloodGlucose,
                Hemoglobin = dto.Hemoglobin,
                MedicalCareId = dto.MedicalCareId
            };

            _context.VitalSigns.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<VitalSignsDTO?> UpdateAsync(VitalSignsDTO dto)
        {
            var entity = await _context.VitalSigns.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Weight = dto.Weight;
            entity.Height = dto.Height;
            entity.Icm = dto.Icm;
            entity.AbdominalCircumference = dto.AbdominalCircumference;
            entity.BloodPressure = dto.BloodPressure;
            entity.Temperature = dto.Temperature;
            entity.MeanArterialPressure = dto.MeanArterialPressure;
            entity.HeartRate = dto.HeartRate;
            entity.OxygenSaturation = dto.OxygenSaturation;
            entity.RespiratoryRate = dto.RespiratoryRate;
            entity.BloodGlucose = dto.BloodGlucose;
            entity.Hemoglobin = dto.Hemoglobin;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.VitalSigns.FindAsync(id);
            if (entity == null) return false;

            _context.VitalSigns.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
