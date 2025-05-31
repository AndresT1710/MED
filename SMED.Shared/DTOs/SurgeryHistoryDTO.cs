using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class SurgeryHistoryDTO
    {
        public int SurgeryHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public DateTime? SurgeryDate { get; set; } 
        public int ClinicalHistoryId { get; set; }
        public int? SurgeryId { get; set; }

        public string? SurgeryName { get; set; }
    }
}