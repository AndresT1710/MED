using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace SMED.Shared.Entity
{
    public class Perimeters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PerimetersId { get; set; }

        public string? Cephalic { get; set; }
        public string? Neck { get; set; }
        public string? RelaxedArmHalf { get; set; }
        public string? Forearm { get; set; }
        public string? Wrist { get; set; }

        public int? MeasurementsId { get; set; }

        [ForeignKey("MeasurementsId")]
        [InverseProperty("Perimeters")]
        public virtual Measurements? Measurements { get; set; }
    }
}