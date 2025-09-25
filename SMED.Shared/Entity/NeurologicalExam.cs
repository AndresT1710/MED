using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class NeurologicalExam
    {
        [Key]
        public int NeurologicalExamId { get; set; }

        [StringLength(100)]
        public string? HistoryNumber { get; set; }

        public int? ClinicalHistoryId { get; set; } 

        public string? Name { get; set; }

        public string? LinkPdf { get; set; }

        public DateTime? ExamDate { get; set; }

        public string? Description { get; set; }

        public int NeurologicalExamTypeId { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("NeurologicalExams")]
        public virtual ClinicalHistory? ClinicalHistory { get; set; } 

        [ForeignKey("NeurologicalExamTypeId")]
        [InverseProperty("NeurologicalExams")]
        public virtual NeurologicalExamType NeurologicalExamType { get; set; } = null!;
    }
}