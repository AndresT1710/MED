using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TherapeuticPlanRepository : IRepository<TherapeuticPlanDTO, int>
    {
        private readonly SGISContext _context;

        public TherapeuticPlanRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TherapeuticPlanDTO>> GetAllAsync()
        {
            var entities = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TherapeuticPlanDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(tp => tp.TherapeuticPlanId == id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TherapeuticPlanDTO> AddAsync(TherapeuticPlanDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TherapeuticPlans.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with related data
            await _context.Entry(entity)
                .Reference(tp => tp.PsychologicalDiagnosis)
                .Query()
                .Include(pd => pd.MedicalCare)
                .ThenInclude(mc => mc.Patient)
                .Include(pd => pd.DiagnosticTypeNavigation)
                .LoadAsync();

            return MapToDto(entity);
        }

        public async Task<TherapeuticPlanDTO?> UpdateAsync(TherapeuticPlanDTO dto)
        {
            var entity = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .FirstOrDefaultAsync(tp => tp.TherapeuticPlanId == dto.TherapeuticPlanId);

            if (entity == null) return null;

            entity.CaseSummary = dto.CaseSummary;
            entity.TherapeuticObjective = dto.TherapeuticObjective;
            entity.StrategyApproach = dto.StrategyApproach;
            entity.AssignedTasks = dto.AssignedTasks;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TherapeuticPlans.FindAsync(id);
            if (entity == null) return false;

            _context.TherapeuticPlans.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TherapeuticPlanDTO>> GetByPsychologicalDiagnosisIdAsync(int psychologicalDiagnosisId)
        {
            var entities = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .Where(tp => tp.PsychologicalDiagnosisId == psychologicalDiagnosisId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<TherapeuticPlanDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var entities = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .Where(tp => tp.PsychologicalDiagnosis.MedicalCareId == medicalCareId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<TherapeuticPlanDTO>> GetByPatientIdAsync(int patientId)
        {
            var entities = await _context.TherapeuticPlans
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.MedicalCare)
                    .ThenInclude(mc => mc.Patient)
                .Include(tp => tp.PsychologicalDiagnosis)
                    .ThenInclude(pd => pd.DiagnosticTypeNavigation)
                .Where(tp => tp.PsychologicalDiagnosis.MedicalCare.PatientId == patientId)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private TherapeuticPlanDTO MapToDto(TherapeuticPlan entity)
        {
            return new TherapeuticPlanDTO
            {
                TherapeuticPlanId = entity.TherapeuticPlanId,
                PsychologicalDiagnosisId = entity.PsychologicalDiagnosisId,
                CaseSummary = entity.CaseSummary,
                TherapeuticObjective = entity.TherapeuticObjective,
                StrategyApproach = entity.StrategyApproach,
                AssignedTasks = entity.AssignedTasks,
                CIE10 = entity.PsychologicalDiagnosis?.CIE10,
                Denomination = entity.PsychologicalDiagnosis?.Denomination,
                DiagnosticTypeName = entity.PsychologicalDiagnosis?.DiagnosticTypeNavigation?.Name,
                PatientName = entity.PsychologicalDiagnosis?.MedicalCare?.Patient.PersonNavigation?.FirstName + " " +
                             entity.PsychologicalDiagnosis?.MedicalCare?.Patient.PersonNavigation?.LastName,
                MedicalCareId = entity.PsychologicalDiagnosis?.MedicalCareId,
                CareDate = entity.PsychologicalDiagnosis?.MedicalCare?.CareDate
            };
        }

        private TherapeuticPlan MapToEntity(TherapeuticPlanDTO dto)
        {
            return new TherapeuticPlan
            {
                TherapeuticPlanId = dto.TherapeuticPlanId,
                PsychologicalDiagnosisId = dto.PsychologicalDiagnosisId,
                CaseSummary = dto.CaseSummary,
                TherapeuticObjective = dto.TherapeuticObjective,
                StrategyApproach = dto.StrategyApproach,
                AssignedTasks = dto.AssignedTasks
            };
        }
    }
}