using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace SMED.Shared.Entity
{
    public class SkinFolds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkinFoldsId { get; set; }

        public string? Subscapular { get; set; }
        public string? Triceps { get; set; }
        public string? Biceps { get; set; }
        public string? IliacCrest { get; set; }
        public string? Supraespinal { get; set; }
        public string? Abdominal { get; set; }
        public string? FrontalThigh { get; set; }
        public string? MedialCalf { get; set; }
        public string? MedialAxillary { get; set; }
        public string? Pectoral { get; set; }

        public int? MeasurementsId { get; set; }

        [ForeignKey("MeasurementsId")]
        [InverseProperty("SkinFolds")]
        public virtual Measurements? Measurements { get; set; }
    }
}