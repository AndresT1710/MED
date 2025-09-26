using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalEvaluationMembers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicalEvaluationMembersId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("MedicalEvaluationMembers")]
        public virtual ICollection<MedicalEvaluation>? MedicalEvaluations { get; set; }
    }
}
