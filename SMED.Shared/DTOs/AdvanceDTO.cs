using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class AdvanceDTO
    {
        public int AdvanceId { get; set; }
        public int SessionId { get; set; }
        public string Task { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? SessionDescription { get; set; }
        public int? MedicalCareId { get; set; }
    }
}
