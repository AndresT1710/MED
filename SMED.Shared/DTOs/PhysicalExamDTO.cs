using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PhysicalExamDTO
    {
        public int PhysicalExamId { get; set; }
        public string? Extremities { get; set; }
        public int? PhysicalExamDetailId { get; set; }
        public int MedicalCareId { get; set; }
    }
}
