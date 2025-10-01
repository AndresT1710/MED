using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class WaterConsumptionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WaterConsumptionHistoryId { get; set; }

        [StringLength(50)]
        public string MedicalRecordNumber { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string? Frequency { get; set; }


        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public string Description { get; set; } = null!;

        public int ClinicalHistoryId { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("WaterConsumptionHistories")]
        public virtual ClinicalHistory MedicalRecordNavigation { get; set; } = null!;
    }
}
