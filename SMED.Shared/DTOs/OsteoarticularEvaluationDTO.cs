using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class OsteoarticularEvaluationDTO
    {
        public int OsteoarticularEvaluationId { get; set; }
        public int? JointConditionId { get; set; }
        public string? JointConditionName { get; set; }
        public int? JointRangeOfMotionId { get; set; }
        public string? JointRangeOfMotionName { get; set; }
        public int? MedicalCareId { get; set; }
    }
}
