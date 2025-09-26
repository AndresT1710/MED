using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class OsteoarticularEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OsteoarticularEvaluationId { get; set; }

        public int? MedicalCareId { get; set; }
        [ForeignKey("MedicalCareId")]
        [InverseProperty("OsteoarticularEvaluations")]
        public virtual MedicalCare? MedicalCare { get; set; }

        public int? JointConditionId { get; set; }
        [ForeignKey("JointConditionId")]
        [InverseProperty("OsteoarticularEvaluations")]
        public virtual JointCondition? JointCondition { get; set; }

        public int? JointRangeOfMotionId { get; set; }
        [ForeignKey("JointRangeOfMotionId")]
        [InverseProperty("OsteoarticularEvaluations")]
        public virtual JointRangeOfMotion? JointRangeOfMotion { get; set; }
    }

}
