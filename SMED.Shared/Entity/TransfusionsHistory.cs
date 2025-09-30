using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class TransfusionsHistory
    {
        [Key]
        public int TransfusionsHistoryId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int? ClinicalHistoryId { get; set; }

        public string? TransfusionReason { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransfusionDate { get; set; }

        public string? Observations { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("TransfusionsHistories")]
        public virtual ClinicalHistory? ClinicalHistory { get; set; } = null!;
    }
}