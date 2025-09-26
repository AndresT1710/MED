using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PhysiotherapyTreatment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhysiotherapyTreatmentId { get; set; }

        public string Recommendations { get; set; }

        public int? PhysiotherapyDiagnosticId { get; set; }

        // Navigation properties
        [ForeignKey("PhysiotherapyDiagnosticId")]
        public virtual PhysiotherapyDiagnostic? PhysiotherapyDiagnostic { get; set; }

    }
}
