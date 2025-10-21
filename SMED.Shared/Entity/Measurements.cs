using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class Measurements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeasurementsId { get; set; }

        public int? MedicalCareId { get; set; }

        // Relaciones 1:1
        [ForeignKey("MedicalCareId")]
        [InverseProperty("Measurements")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [InverseProperty("Measurements")]
        public virtual SkinFolds? SkinFolds { get; set; }

        [InverseProperty("Measurements")]
        public virtual BioImpedance? BioImpedance { get; set; }

        [InverseProperty("Measurements")]
        public virtual Perimeters? Perimeters { get; set; }

        [InverseProperty("Measurements")]
        public virtual Diameters? Diameters { get; set; }
    }
}