using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PsychosexualHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PsychosexualHistoryId { get; set; }

        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }

        public string? Description { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PsychosexualHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}