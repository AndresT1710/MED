using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ComplementaryExamsDTO
    {
        public int ComplementaryExamsId { get; set; }
        public string? Exam { get; set; }
        public DateTime ExamDate { get; set; }
        public string? Descriptions { get; set; }
        public string? PdfLink { get; set; }

        public int MedicalCareId { get; set; }
    }
}
