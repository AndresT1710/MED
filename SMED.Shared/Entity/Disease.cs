using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int DiseaseTypeId { get; set; }

        [InverseProperty("DiseaseNavigation")]
        public virtual ICollection<FamilyHistoryDetail> FamilyHistoryDetails { get; set; } = new List<FamilyHistoryDetail>();

        [InverseProperty("DiseaseNavigation")]
        public virtual ICollection<PersonalHistory> PersonalHistories { get; set; } = new List<PersonalHistory>();

        [ForeignKey("DiseaseTypeId")]
        [InverseProperty("Diseases")]
        public virtual DiseaseType DiseaseTypeNavigation { get; set; } = null!;

        [InverseProperty("DiseaseNavigation")]
        public virtual ICollection<IdentifiedDisease> IdentifiedDiseases { get; set; } = new List<IdentifiedDisease>();

        [InverseProperty("DiseaseNavigation")]
        public virtual ICollection<MedicalDiagnosis> Diagnosis { get; set; } = new List<MedicalDiagnosis>();

    }
}
