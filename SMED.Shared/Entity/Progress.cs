using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Progress
    {
        [Key]
        public int ProgressId { get; set; }

        public string Observation { get; set; } = null!;

        public double ProgressPercentage { get; set; }

        public int MedicalVisitId { get; set; }

        [ForeignKey("MedicalVisitId")]
        [InverseProperty("Progresses")]
        public virtual MedicalVisit MedicalVisitNavigation { get; set; } = null!;
    }

}
