using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class TraumaticHistory
    {
        [Key]
        public int TraumaticHistoryId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int? ClinicalHistoryId { get; set; }

        public string? Description { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("TraumaticHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}