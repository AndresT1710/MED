using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMED.Shared.Entity;

namespace SMED.Shared.DTOs
{
    public class ClinicalHistoryDTO
    {
        public int ClinicalHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public string? GeneralObservations { get; set; }

        public string PatientFullName { get; set; }
        public string? DocumentNumber { get; set; }

        public PatientDTO Patient { get; set; } = new PatientDTO();

        public List<PersonalHistoryDTO>? PersonalHistories { get; set; } = new();
        public List<SurgeryHistoryDTO>? SurgeryHistories { get; set; } = new();
        public List<AllergyHistoryDTO>? AllergyHistories { get; set; } = new();
        public List<HabitHistoryDTO>? HabitHistories { get; set; } = new();
        public List<FamilyHistoryDetailDTO>? FamilyHistories { get; set; } = new();
        public List<ToxicHabitHistoryDTO>? ToxicHabitsHistory { get; set; } = new();
        public List<FoodIntoleranceHistoryDTO>? FoodIntoleranceHistories { get; set; } = new();
        public List<ObstetricHistoryDTO>? ObstetricHistories { get; set; } = new();
        public List<MedicationHistoryDTO>? MedicationHistories { get; set; } = new();
        public List<PsychopsychiatricHistoryDTO>? PsychopsychiatricHistories { get; set; } = new();
        public List<CurrentProblemHistoryDTO>? CurrentProblemHistories { get; set; } = new();
        public List<WorkHistoryDTO>? WorkHistories { get; set; } = new();
        public List<PsychosexualHistoryDTO>? PsychosexualHistories { get; set; } = new();
        public List<PrenatalHistoryDTO>? PrenatalHistories { get; set; } = new();
        public List<PostnatalHistoryDTO>? PostnatalHistories { get; set; } = new();
        public List<PerinatalHistoryDTO>? PerinatalHistories { get; set; } = new();
        public List<NeuropsychologicalHistoryDTO>? NeuropsychologicalHistories { get; set; } = new();
        public List<NeurologicalExamDTO>? NeurologicalExams { get; set; } = new();
        public List<DevelopmentRecordDTO>? DevelopmentRecords { get; set; } = new();
        public List<TraumaticHistoryDTO>? TraumaticHistories { get; set; } = new();
        public List<HospitalizationsHistoryDTO>? HospitalizationsHistories { get; set; } = new();
        public List<TransfusionsHistoryDTO>? TransfusionsHistories { get; set; } = new();
        public GynecologicalHistoryDTO? GynecologicalHistory { get; set; } = new();
        public SportsActivitiesHistoryDTO? SportsActivitiesHistory { get; set; } = new();
        public LifeStyleHistoryDTO? LifeStyleHistory { get; set; } = new();
        public DietaryHabitsHistoryDTO? DietaryHabitsHistory { get; set; } = new();
        public SleepHabitHistoryDTO? SleepHabitHistory { get; set; } = new();
        public FoodConsumptionHistoryDTO? FoodConsumptionHistory { get; set; } = new();
        public WaterConsumptionHistoryDTO? WaterConsumptionHistory { get; set; } = new();
        public ObstetricHistoryDTO? ObstetricHistory { get; set; }

        public List<ToxicHabitHistoryDTO>? ToxicHabitHistories { get; set; } = new();
    }
}