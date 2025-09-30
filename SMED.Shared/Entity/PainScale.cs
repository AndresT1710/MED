using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace SMED.Shared.Entity
{
    public class PainScale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PainScaleId { get; set; }

        public string Observation { get; set; }

        // Foreign keys
        public int? ActionId { get; set; }
        public int? ScaleId { get; set; }
        public int? PainMomentId { get; set; }
        public int? MedicalCareId { get; set; }

        // Navigation properties
        [ForeignKey("ActionId")]
        public virtual ActionF? Action { get; set; }

        [ForeignKey("ScaleId")]
        public virtual Scale? Scale { get; set; }

        [ForeignKey("PainMomentId")]
        public virtual PainMoment? PainMoment { get; set; }

        [ForeignKey("MedicalCareId")]
        public virtual MedicalCare? MedicalCare { get; set; }
    }
}
