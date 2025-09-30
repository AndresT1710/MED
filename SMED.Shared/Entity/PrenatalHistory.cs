using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PrenatalHistory
    {
        [Key]
        public int PrenatalHistoryId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int ClinicalHistoryId { get; set; }

        public string? Description { get; set; }

        public bool? PlannedPregnancy { get; set; }

        public string? MedicationsOrVitamins { get; set; }

        public bool? RadiationExposure { get; set; }

        public int? NumberOfControls { get; set; }

        public int? NumberOfUltrasounds { get; set; }

        public bool? FetalSuffering { get; set; }

        public string? ComplicationsDuringPregnancy { get; set; }



        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PrenatalHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}