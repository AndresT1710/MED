using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DevelopmentRecord
    {
        [Key]
        public int DevelopmentRecordId { get; set; }

        [StringLength(100)]
        public string HistoryNumber { get; set; }

        public int ClinicalHistoryId { get; set; }

        public string DevelopmentMilestone { get; set; }

        public string AgeRange { get; set; }

        public string Observations { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("DevelopmentRecords")]
        public virtual ClinicalHistory ClinicalHistory { get; set; } = null!;
    }
}