using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace SMED.Shared.Entity
{
    public class BioImpedance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BioImpedanceId { get; set; }

        public string? BodyFatPercentage { get; set; }
        public string? UpperSectionFatPercentage { get; set; }
        public string? LowerSectionFatPercentage { get; set; }
        public string? VisceralFat { get; set; }
        public string? MuscleMass { get; set; }
        public string? BoneWeight { get; set; }
        public string? BodyWater { get; set; }

        public int? MeasurementsId { get; set; }

        [ForeignKey("MeasurementsId")]
        [InverseProperty("BioImpedance")]
        public virtual Measurements? Measurements { get; set; }
    }
}