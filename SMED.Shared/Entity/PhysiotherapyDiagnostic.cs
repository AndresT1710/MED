using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PhysiotherapyDiagnostic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhysiotherapyDiagnosticId { get; set; }

        public string Description { get; set; }

        public int? MedicalCareId { get; set; }

        // Navigation properties
        [ForeignKey("MedicalCareId")]
        public virtual MedicalCare? MedicalCare { get; set; }

        public virtual ICollection<PhysiotherapyTreatment> PhysiotherapyTreatments { get; set; } = new List<PhysiotherapyTreatment>();
    }
}
