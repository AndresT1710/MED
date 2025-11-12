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
        public int? SessionId { get; set; }
        public int? PsychologySessionId { get; set; }
        public string Indications { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }

        // Propiedades de navegación
        public string? SessionDescription { get; set; }
        public string? PsychologySessionDescription { get; set; }
        public int? MedicalCareId { get; set; }
        public string? PatientName { get; set; }
    }
}
