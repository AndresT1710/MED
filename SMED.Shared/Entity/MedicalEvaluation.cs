using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalEvaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicalEvaluationId { get; set; }
        public string? Description { get; set; }

        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("MedicalEvaluations")]
        public virtual MedicalCare? MedicalCare { get; set; }

        public int? TypeOfMedicalEvaluationId { get; set; }

        [ForeignKey("TypeOfMedicalEvaluationId")]
        [InverseProperty("MedicalEvaluations")]
        public virtual TypeOfMedicalEvaluation? TypeOfMedicalEvaluation { get; set; }

        public int? MedicalEvaluationPositionId { get; set; }

        [ForeignKey("MedicalEvaluationPositionId")]
        [InverseProperty("MedicalEvaluations")]
        public virtual MedicalEvaluationPosition? MedicalEvaluationPosition { get; set; }

        public int? MedicalEvaluationMembersId { get; set; }

        [ForeignKey("MedicalEvaluationMembersId")]
        [InverseProperty("MedicalEvaluations")]
        public virtual MedicalEvaluationMembers? MedicalEvaluationMembers { get; set; }
    }
}
