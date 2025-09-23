using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class CurrentProblemHistory
    {
        [Key]
        public int CurrentProblemHistoryId { get; set; }

        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }

        public string? AppearanceEvolution { get; set; }
        public string? TriggeringFactors { get; set; }
        public string? FrequencyIntensitySymptoms { get; set; }
        public string? Impact { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("CurrentProblemHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}