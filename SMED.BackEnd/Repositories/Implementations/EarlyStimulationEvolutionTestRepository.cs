using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class EarlyStimulationEvolutionTestRepository : IRepository<EarlyStimulationEvolutionTestDTO, int>
    {
        private readonly SGISContext _context;

        public EarlyStimulationEvolutionTestRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<EarlyStimulationEvolutionTestDTO>> GetAllAsync()
        {
            var entities = await _context.EarlyStimulationEvolutionTests.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<EarlyStimulationEvolutionTestDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.EarlyStimulationEvolutionTests.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<EarlyStimulationEvolutionTestDTO> AddAsync(EarlyStimulationEvolutionTestDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.EarlyStimulationEvolutionTests.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<EarlyStimulationEvolutionTestDTO?> UpdateAsync(EarlyStimulationEvolutionTestDTO dto)
        {
            var entity = await _context.EarlyStimulationEvolutionTests.FindAsync(dto.TestId);
            if (entity == null) return null;

            entity.Age = dto.Age;
            entity.Age1 = dto.Age1;
            entity.GrossMotorSkills = dto.GrossMotorSkills;
            entity.FineMotorSkills = dto.FineMotorSkills;
            entity.HearingAndLanguage = dto.HearingAndLanguage;
            entity.SocialPerson = dto.SocialPerson;
            entity.Total = dto.Total;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EarlyStimulationEvolutionTests.FindAsync(id);
            if (entity == null) return false;

            _context.EarlyStimulationEvolutionTests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Mapeos
        private EarlyStimulationEvolutionTestDTO MapToDto(EarlyStimulationEvolutionTest entity) => new EarlyStimulationEvolutionTestDTO
        {
            TestId = entity.TestId,
            Age = entity.Age,
            Age1 = entity.Age1,
            GrossMotorSkills = entity.GrossMotorSkills,
            FineMotorSkills = entity.FineMotorSkills,
            HearingAndLanguage = entity.HearingAndLanguage,
            SocialPerson = entity.SocialPerson,
            Total = entity.Total,
            MedicalCareId = entity.MedicalCareId
        };

        private EarlyStimulationEvolutionTest MapToEntity(EarlyStimulationEvolutionTestDTO dto) => new EarlyStimulationEvolutionTest
        {
            TestId = dto.TestId,
            Age = dto.Age,
            Age1 = dto.Age1,
            GrossMotorSkills = dto.GrossMotorSkills,
            FineMotorSkills = dto.FineMotorSkills,
            HearingAndLanguage = dto.HearingAndLanguage,
            SocialPerson = dto.SocialPerson,
            Total = dto.Total,
            MedicalCareId = dto.MedicalCareId
        };
    }
}
