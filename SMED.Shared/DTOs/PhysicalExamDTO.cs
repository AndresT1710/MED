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
        public string? Observation { get; set; }
        public int RegionId { get; set; }
        public string? RegionName { get; set; }
        public int PathologicalEvidenceId { get; set; }
        public string? PathologicalEvidenceName { get; set; }
        public int? PhysicalExamTypeId { get; set; }
        public string? PhysicalExamTypeName { get; set; }
        public int MedicalCareId { get; set; }
    }
}
