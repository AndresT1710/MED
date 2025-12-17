using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PreviousMeasurementDTO
    {
        public string Value { get; set; } = string.Empty;
        public DateTime? MeasurementDate { get; set; }
        public int MedicalCareId { get; set; }
        public string Area { get; set; } = string.Empty;

        // Para mostrar en formato amigable
        public string DisplayInfo => MeasurementDate.HasValue
            ? $"{Value} ({MeasurementDate.Value:dd/MM/yyyy})"
            : Value;
    }
}
