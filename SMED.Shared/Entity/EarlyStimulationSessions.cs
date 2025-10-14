using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class EarlyStimulationSessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionsId { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Treatment { get; set; }
        public bool? MedicalDischarge { get; set; }
        public string? Observations { get; set; }
        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("EarlyStimulationSessions")]
        public virtual MedicalCare? MedicalCare { get; set; }
    }
}
