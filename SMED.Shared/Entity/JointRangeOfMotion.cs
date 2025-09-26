using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class JointRangeOfMotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JointRangeOfMotionId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("JointRangeOfMotion")]
        public virtual ICollection<OsteoarticularEvaluation> OsteoarticularEvaluations { get; set; } = new List<OsteoarticularEvaluation>();
    }

}
