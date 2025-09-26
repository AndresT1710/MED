using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class MedicationHistoryDTO
    {
        public int MedicationHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int MedicineId { get; set; }
        public int ClinicalHistoryId { get; set; }
        public DateTime? ConsumptionDate { get; set; }

        public string? MedicineName { get; set; }
        public float? MedicineWeight { get; set; }
    }
}