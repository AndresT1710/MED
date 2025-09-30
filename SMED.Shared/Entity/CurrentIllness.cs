using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class CurrentIllness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurrentIllnessId { get; set; }

        public string? EvolutionTime { get; set; }
        public string? Localization { get; set; }
        public string? Intensity { get; set; }
        public string? AggravatingFactors { get; set; }
        public string? MitigatingFactors { get; set; }
        public string? NocturnalPain { get; set; }
        public string? Weakness { get; set; }
        public string? Paresthesias { get; set; }
        public string? ComplementaryExams { get; set; }

        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("CurrentIllnesses")]
        public virtual MedicalCare? MedicalCare { get; set; }
    }

}