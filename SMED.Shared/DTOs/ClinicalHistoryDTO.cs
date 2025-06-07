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

        public ObstetricHistoryDTO? ObstetricHistory { get; set; } = new();

        public GynecologicalHistoryDTO? GynecologicalHistory { get; set; } = new();

        public SportsActivitiesHistoryDTO? SportsActivitiesHistory { get; set; } = new();

        public LifeStyleHistoryDTO? LifeStyleHistory { get; set; } = new();

        public DietaryHabitsHistoryDTO? DietaryHabitsHistory { get; set; } = new();

        public SleepHabitHistoryDTO? SleepHabitHistory { get; set; } = new();

        public FoodConsumptionHistoryDTO? FoodConsumptionHistory { get; set; } = new();

        public WaterConsumptionHistoryDTO? WaterConsumptionHistory { get; set; } = new();
    }

}
