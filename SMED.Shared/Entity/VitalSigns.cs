using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class VitalSigns
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Icm { get; set; }
        public decimal? AbdominalCircumference { get; set; }
        public string? BloodPressure { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? MeanArterialPressure { get; set; }
        public int? HeartRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? BloodGlucose { get; set; }
        public decimal? Hemoglobin { get; set; }


        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("VitalSigns")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}
