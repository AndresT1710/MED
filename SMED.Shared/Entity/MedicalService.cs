using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class MedicalService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        public int? CareId { get; set; }

        public DateTime? ServiceDate { get; set; }

        [StringLength(100)]
        public string? ServiceType { get; set; }

        public string? Diagnosis { get; set; }

        public string? Observations { get; set; }

        public int PatientId { get; set; }

        public int HealthProfessionalId { get; set; }

        [StringLength(255)]
        public string? Recommendations { get; set; }

        // Relaciones
        [ForeignKey("CareId")]
        [InverseProperty("MedicalServices")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("MedicalServices")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("HealthProfessionalId")]
        [InverseProperty("MedicalServices")]
        public virtual HealthProfessional? HealthProfessional { get; set; }
    }

}
