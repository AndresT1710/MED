using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class NeuromuscularEvaluationDTO
    {
        public int NeuromuscularEvaluationId { get; set; }
        public int? ShadeId { get; set; }
        public string? ShadeName { get; set; }
        public int? StrengthId { get; set; }
        public string? StrengthName { get; set; }
        public int? TrophismId { get; set; }
        public string? TrophismName { get; set; }
        public int? MedicalCareId { get; set; }
    }

}
