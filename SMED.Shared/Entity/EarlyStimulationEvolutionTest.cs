using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class EarlyStimulationEvolutionTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }

        public int? Age { get; set; }

        public int? Age1 { get; set; }

        public int? GrossMotorSkills { get; set; }

        public int? FineMotorSkills { get; set; }

        public int? HearingAndLanguage { get; set; }

        public int? SocialPerson { get; set; }

        public int? Total { get; set; }

        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("EarlyStimulationEvolutionTests")]
        public virtual MedicalCare? MedicalCare { get; set; }
    }
}
