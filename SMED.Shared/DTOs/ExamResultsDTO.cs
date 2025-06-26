using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ExamResultsDTO
    {
        public int Id { get; set; }
        public string? LinkPdf { get; set; }
        public DateTime? ExamDate { get; set; }
        public string? Description { get; set; }
        public int ExamTypeId { get; set; }
        public int MedicalCareId { get; set; }
    }
}
