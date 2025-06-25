using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class VitalSignsDTO
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Icm { get; set; }
        public float AbdominalCircumference { get; set; }
        public int BloodPressure { get; set; }
        public float Temperature { get; set; }
        public int MeanArterialPressure { get; set; }
        public float HeartRate { get; set; }
        public float OxygenSaturation { get; set; }
        public float RespiratoryRate { get; set; }
        public float BloodGlucose { get; set; }
        public float Hemoglobin { get; set; }
        public int MedicalCareId { get; set; }
    }
}
