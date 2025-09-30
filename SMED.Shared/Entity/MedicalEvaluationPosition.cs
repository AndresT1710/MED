using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalEvaluationPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicalEvaluationPositionId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("MedicalEvaluationPosition")]
        public virtual ICollection<MedicalEvaluation> MedicalEvaluations { get; set; } = new List<MedicalEvaluation>();
    }
}
