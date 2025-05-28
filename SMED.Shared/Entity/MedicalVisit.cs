using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalVisit
    {
        [Key]
        public int MedicalVisitId { get; set; }

        public DateOnly VisitDate { get; set; }

        public int? PatientId { get; set; }

        [InverseProperty("MedicalVisitNavigation")]
        public virtual ICollection<Progress> Progresses { get; set; } = new List<Progress>();

        [ForeignKey("PatientId")]
        [InverseProperty("MedicalVisits")]
        public virtual Patient? Patient { get; set; } 

    }

}
