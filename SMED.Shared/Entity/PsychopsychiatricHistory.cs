using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PsychopsychiatricHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PsychopsychiatricHistoryId { get; set; }

        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }

        public string? Type { get; set; }
        public string? Actor { get; set; }
        public DateTime? HistoryDate { get; set; }
        public string? HistoryState { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PsychopsychiatricHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}