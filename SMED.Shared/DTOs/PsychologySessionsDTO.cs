using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PsychologySessionsDTO
    {
        public int PsychologySessionsId { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public bool? MedicalDischarge { get; set; }
        public string? Observations { get; set; }
        public string? VoluntaryRegistrationLink { get; set; }
        public string? SummarySession { get; set; }
        public int? MedicalCareId { get; set; }

        // Propiedades de navegación
        public string? MedicalCareDescription { get; set; }
        public string? PatientName { get; set; }

        // Colecciones
        public List<ActivityDTO> Activities { get; set; } = new List<ActivityDTO>();
        public List<AdvanceDTO> Advances { get; set; } = new List<AdvanceDTO>();
    }
}
