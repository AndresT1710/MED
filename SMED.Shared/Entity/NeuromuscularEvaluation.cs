using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class NeuromuscularEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NeuromuscularEvaluationId { get; set; }

        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("NeuromuscularEvaluations")]
        public virtual MedicalCare? MedicalCare { get; set; }

        public int? ShadeId { get; set; }

        [ForeignKey("ShadeId")]
        [InverseProperty("NeuromuscularEvaluations")]
        public virtual Shade? Shade { get; set; }

        public int? StrengthId { get; set; }

        [ForeignKey("StrengthId")]
        [InverseProperty("NeuromuscularEvaluations")]
        public virtual Strength? Strength { get; set; }

        public int? TrophismId { get; set; }

        [ForeignKey("TrophismId")]
        [InverseProperty("NeuromuscularEvaluations")]
        public virtual Trophism? Trophism { get; set; }


    }

}
