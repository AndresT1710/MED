using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class BasicClinicalHistroyDTO
    {
        public int ClinicalHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
        public string? GeneralObservations { get; set; }
        public string? PatientFullName { get; set; }
        public string? DocumentNumber { get; set; }

        public PatientDTO Patient { get; set; } = new PatientDTO();

    }
}
