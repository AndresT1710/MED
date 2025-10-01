using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class HospitalizationsHistory
    {
        [Key]
        public int HospitalizationsHistoryId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int? ClinicalHistoryId { get; set; }

        public string? HospitalizationReason { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HospitalizationDate { get; set; }

        public string? HospitalizationPlace { get; set; }

        public string? Observations { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("HospitalizationsHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;



    }
}