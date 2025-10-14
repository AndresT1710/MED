using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicalEvaluationDTO
    {
        public int MedicalEvaluationId { get; set; }
        public string? Description { get; set; }
        public int? MedicalCareId { get; set; }
        public int? TypeOfMedicalEvaluationId { get; set; }
        public string? TypeOfMedicalEvaluationName { get; set; }
        public int? MedicalEvaluationPositionId { get; set; }
        public string? MedicalEvaluationPositionName { get; set; }
        public int? MedicalEvaluationMembersId { get; set; }
        public string? MedicalEvaluationMembersName { get; set; }
    }
}