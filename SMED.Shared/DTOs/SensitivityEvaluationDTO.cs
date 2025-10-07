using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SensitivityEvaluationDTO
    {
        public int SensitivityEvaluationId { get; set; }
        public string Demandmas { get; set; }

        public int? SensitivityLevelId { get; set; }
        public int? BodyZoneId { get; set; }

        public int? MedicalCareId { get; set; }

    }
}
