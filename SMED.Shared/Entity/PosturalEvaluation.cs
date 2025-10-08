using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PosturalEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PosturalEvaluationId { get; set; }

        public string? Observation { get; set; }
        public float Grade { get; set; }
        public string? BodyAlignment { get; set; }

        public int? MedicalCareId { get; set; }
        public int? ViewId { get; set; }

        [ForeignKey("MedicalCareId")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [ForeignKey("ViewId")]
        public virtual View? View { get; set; }
    }
}
