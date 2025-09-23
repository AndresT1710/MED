using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicationHistory
    {
        [Key]
        public int MedicationHistoryId { get; set; }

        public string HistoryNumber { get; set; }

        public int MedicineId { get; set; }
        public int ClinicalHistoryId { get; set; }

        public DateTime? ConsumptionDate { get; set; }

        [ForeignKey("MedicineId")]
        [InverseProperty("MedicationHistories")]
        public virtual Medicine Medicine { get; set; } = null!;

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("MedicationHistories")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}