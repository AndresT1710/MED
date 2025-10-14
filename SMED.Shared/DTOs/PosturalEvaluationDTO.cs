using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PosturalEvaluationDTO
    {
        public int PosturalEvaluationId { get; set; }
        public string? Observation { get; set; }
        public float Grade { get; set; }
        public string? BodyAlignment { get; set; }
        public int? MedicalCareId { get; set; }
        public int? ViewId { get; set; }
        public string? ViewName { get; set; }
    }
}
