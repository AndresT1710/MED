using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace SMED.Shared.Entity
{
    public class Diameters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiametersId { get; set; }

        public string? Humerus { get; set; }
        public string? Femur { get; set; }
        public string? Wrist { get; set; }

        public int? MeasurementsId { get; set; }

        [ForeignKey("MeasurementsId")]
        [InverseProperty("Diameters")]
        public virtual Measurements? Measurements { get; set; }
    }
}