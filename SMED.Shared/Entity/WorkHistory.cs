using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class WorkHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkHistoryId { get; set; }

        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }

        public string? Experience { get; set; }
        public string? Stability { get; set; }
        public string? SatisfactionLevel { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("WorkHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}