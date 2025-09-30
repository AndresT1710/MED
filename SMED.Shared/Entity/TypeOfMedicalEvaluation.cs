using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class TypeOfMedicalEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeOfMedicalEvaluationId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("TypeOfMedicalEvaluation")]
        public virtual ICollection<MedicalEvaluation> MedicalEvaluations { get; set; } = new List<MedicalEvaluation>();
    }
}
