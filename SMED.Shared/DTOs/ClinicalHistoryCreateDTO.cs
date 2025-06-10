using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ClinicalHistoryCreateDTO
    {
        public string HistoryNumber { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public string? GeneralObservations { get; set; }

        public PatientDTO Patient { get; set; } = new PatientDTO();
    }
}
