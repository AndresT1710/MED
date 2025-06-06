using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class WaterConsumptionHistoryDTO
    {
        public int WaterConsumptionHistoryId { get; set; }
        public string MedicalRecordNumber { get; set; } = null!;
        public decimal? Amount { get; set; }
        public string? Frequency { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Description { get; set; } = null!;
        public int ClinicalHistoryId { get; set; }
    }
}
