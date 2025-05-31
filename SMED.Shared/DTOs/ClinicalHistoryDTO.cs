using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMED.Shared.Entity;

namespace SMED.Shared.DTOs
{
    public class ClinicalHistoryDTO
    {
        public int ClinicalHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public string? GeneralObservations { get; set; }

        public string PatientFullName { get; set; }

        public PatientDTO Patient { get; set; } = new PatientDTO();
    }
}
