using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class InterconsultationDTO
    {
        public int Id { get; set; }
        public DateTime? InterconsultationDate { get; set; }
        public string? Reason { get; set; }
        public int DiagnosisId { get; set; }
        public int ServiceId { get; set; }
    }
}
