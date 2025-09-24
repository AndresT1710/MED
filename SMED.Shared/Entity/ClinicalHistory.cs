using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class ClinicalHistory
    {
        [Key]
        public int ClinicalHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }

        public bool? IsActive { get; set; }

        public string? GeneralObservations { get; set; }

        public int? PatientId { get; set; }


        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<AllergyHistory> AllergyHistories { get; set; } = new List<AllergyHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<SurgeryHistory> SurgeryHistories { get; set; } = new List<SurgeryHistory>();

        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<FamilyHistoryDetail> FamilyHistoryDetails { get; set; } = new List<FamilyHistoryDetail>();

        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<GynecologicalHistory> GynecologicalHistories { get; set; } = new List<GynecologicalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<ToxicHabitHistory> ToxicHabitsHistory { get; set; } = new List<ToxicHabitHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<FoodIntoleranceHistory> FoodIntoleranceHistories { get; set; } = new List<FoodIntoleranceHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<ObstetricHistory> ObstetricHistories { get; set; } = new List<ObstetricHistory>();

        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<PersonalHistory> PersonalHistories { get; set; } = new List<PersonalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<HabitHistory> HabitHistories { get; set; } = new List<HabitHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<SleepHabitHistory> SleepHabitHistories { get; set; } = new List<SleepHabitHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<SportsActivitiesHistory> SportsActivitiesHistories { get; set; } = new List<SportsActivitiesHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<LifeStyleHistory> LifeStyleHistories { get; set; } = new List<LifeStyleHistory>();

        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<DietaryHabitsHistory> DietaryHabitHistories { get; set; } = new List<DietaryHabitsHistory>();

        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<FoodConsumptionHistory> FoodConsumptionHistories { get; set; } = new List<FoodConsumptionHistory>();

        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<WaterConsumptionHistory> WaterConsumptionHistories { get; set; } = new List<WaterConsumptionHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<MedicationHistory> MedicationHistories { get; set; } = new List<MedicationHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<PsychopsychiatricHistory> PsychopsychiatricHistories { get; set; } = new List<PsychopsychiatricHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<CurrentProblemHistory> CurrentProblemHistories { get; set; } = new List<CurrentProblemHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<WorkHistory> WorkHistories { get; set; } = new List<WorkHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<PsychosexualHistory> PsychosexualHistories { get; set; } = new List<PsychosexualHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<PrenatalHistory> PrenatalHistories { get; set; } = new List<PrenatalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<PostnatalHistory> PostnatalHistories { get; set; } = new List<PostnatalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<PerinatalHistory> PerinatalHistories { get; set; } = new List<PerinatalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<NeuropsychologicalHistory> NeuropsychologicalHistories { get; set; } = new List<NeuropsychologicalHistory>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<NeurologicalExam> NeurologicalExams { get; set; } = new List<NeurologicalExam>();

        [InverseProperty("ClinicalHistory")]
        public virtual ICollection<DevelopmentRecord> DevelopmentRecords { get; set; } = new List<DevelopmentRecord>();

        [ForeignKey("PatientId")]
        [InverseProperty("ClinicalHistory")]
        public virtual Patient? Patient { get; set; }

        [NotMapped]
        public virtual Person? Person => Patient?.PersonNavigation;
    }
}