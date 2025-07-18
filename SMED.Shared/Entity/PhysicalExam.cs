using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PhysicalExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhysicalExamId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int RegionId { get; set; }

        public int PathologicalEvidenceId { get; set; }

        public int PhysicalExamTypeId { get; set; }

        public int MedicalCareId { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("PhysicalExams")]
        public virtual Region RegionNavigation { get; set; } = null!;

        [ForeignKey("PathologicalEvidenceId")]
        [InverseProperty("PhysicalExams")]
        public virtual PathologicalEvidence PathologicalEvidenceNavigation { get; set; } = null!;

        [ForeignKey("PhysicalExamTypeId")]
        [InverseProperty("PhysicalExams")]
        public virtual PhysicalExamType PhysicalExamTypeNavigation { get; set; } = null!;

        [ForeignKey("MedicalCareId")]
        [InverseProperty("PhysicalExam")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }

}
