using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MentalFunctionsPsychologyRepository : IRepository<MentalFunctionsPsychologyDTO, int>
    {
        private readonly SGISContext _context;

        public MentalFunctionsPsychologyRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MentalFunctionsPsychologyDTO>> GetAllAsync()
        {
            var entities = await _context.MentalFunctionsPsychologies.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<MentalFunctionsPsychologyDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.MentalFunctionsPsychologies.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<MentalFunctionsPsychologyDTO> AddAsync(MentalFunctionsPsychologyDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.MentalFunctionsPsychologies.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<MentalFunctionsPsychologyDTO?> UpdateAsync(MentalFunctionsPsychologyDTO dto)
        {
            var entity = await _context.MentalFunctionsPsychologies.FindAsync(dto.MentalFunctionsPsychologyId);
            if (entity == null) return null;

            entity.MedicalCareId = dto.MedicalCareId;
            entity.MentalFunctionId = dto.MentalFunctionId;
            entity.Description = dto.Description;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MentalFunctionsPsychologies.FindAsync(id);
            if (entity == null) return false;

            _context.MentalFunctionsPsychologies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MentalFunctionsPsychologyDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entities = await _context.MentalFunctionsPsychologies
                .Where(m => m.MedicalCareId == medicalCareId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        private MentalFunctionsPsychologyDTO MapToDto(MentalFunctionsPsychology entity)
        {
            return new MentalFunctionsPsychologyDTO
            {
                MentalFunctionsPsychologyId = entity.MentalFunctionsPsychologyId,
                MedicalCareId = entity.MedicalCareId,
                MentalFunctionId = entity.MentalFunctionId,
                Description = entity.Description
            };
        }

        private MentalFunctionsPsychology MapToEntity(MentalFunctionsPsychologyDTO dto)
        {
            return new MentalFunctionsPsychology
            {
                MentalFunctionsPsychologyId = dto.MentalFunctionsPsychologyId,
                MedicalCareId = dto.MedicalCareId,
                MentalFunctionId = dto.MentalFunctionId,
                Description = dto.Description
            };
        }
    }
}
