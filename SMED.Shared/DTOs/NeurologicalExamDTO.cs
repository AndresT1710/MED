using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class NeurologicalExamDTO
    {
        public int NeurologicalExamId { get; set; }
        public string? HistoryNumber { get; set; }
        public int? ClinicalHistoryId { get; set; } 
        public string? Name { get; set; }
        public string? LinkPdf { get; set; }
        public DateTime? ExamDate { get; set; }
        public string? Description { get; set; }
        public int NeurologicalExamTypeId { get; set; }
        public string? NeurologicalExamTypeName { get; set; }
    }
}