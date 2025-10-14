using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SkinEvaluationDTO
    {
        public int SkinEvaluationId { get; set; }
        public int? MedicalCareId { get; set; }
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public int? EdemaId { get; set; }

        public string? EdemaName { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? SwellingId { get; set; }
        public string? SwellingName { get; set; }

        public DateTime? EvaluationDate { get; set; }
    }
}
