using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using System.Xml.Linq;

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
        public virtual ICollection<ToxicHabitBackground> ToxicHabitBackgrounds { get; set; } = new List<ToxicHabitBackground>();


        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<FoodIntoleranceHistory> FoodIntoleranceHistories { get; set; } = new List<FoodIntoleranceHistory>();


        [InverseProperty("HistoryNavigation")]
        public virtual ICollection<ObstetricHistory> ObstetricHistories { get; set; } = new List<ObstetricHistory>();


        [InverseProperty("MedicalRecordNavigation")]
        public virtual ICollection<PersonalHistory> PersonalHistories { get; set; } = new List<PersonalHistory>();

        [ForeignKey("PatientId")]
        [InverseProperty("ClinicalHistories")]
        public virtual Patient? Patient { get; set; }
    }

}
