using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class GynecologicalHistory
    {
        [Key]
        public int GynecologicalHistoryId { get; set; }

        [StringLength(50)]
        public string MedicalRecordNumber { get; set; } = null!;

        [StringLength(255)]
        public string? GynecologicalDevelopment { get; set; }

        public DateOnly? Menarche { get; set; }

        public DateOnly? Pubarche { get; set; }

        [StringLength(100)]
        public string? MenstrualCycles { get; set; }

        public DateOnly? LastMenstruation { get; set; }

        [StringLength(255)]
        public string? ContraceptiveMethods { get; set; }

        public int? DiseaseId { get; set; }

        [ForeignKey("DiseaseId")]
        [InverseProperty("GynecologicalHistories")]
        public virtual Disease? DiseaseNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("GynecologicalHistories")]
        public virtual ClinicalHistory MedicalRecordNavigation { get; set; } = null!;
    }
}
