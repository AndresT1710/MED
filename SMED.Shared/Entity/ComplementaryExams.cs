using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class ComplementaryExams
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplementaryExamsId { get; set; }

        public string? Exam { get; set; }
        public DateTime ExamDate { get; set; }
        public string? Descriptions { get; set; }
        public string? PdfLink { get; set; }
        public int MedicalCareId { get; set; }


        [ForeignKey("MedicalCareId")]
        public virtual MedicalCare MedicalCare { get; set; }
    }
}
