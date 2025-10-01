using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PerinatalHistory
    {
        [Key]
        public int PerinatalHistoryId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int ClinicalHistoryId { get; set; }

        public string? Description { get; set; }

        public string? TypeOfBirth { get; set; }

        public string? Apgar { get; set; }

        public string? AuditoryScreen { get; set; }

        public string? ResuscitationManeuvers { get; set; }

        public string? PlaceOfCare { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal? NumberOfWeeks { get; set; }

        public bool? BirthCry { get; set; }

        public string? MetabolicScreen { get; set; }

        public string? ComplicationsDuringChildbirth { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("PerinatalHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}