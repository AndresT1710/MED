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

        public int? CareId { get; set; } // FK a MedicalCares (opcional)

        public DateTime? ServiceDate { get; set; } // Fecha de atención

        [StringLength(100)]
        public string? ServiceType { get; set; } // Tipo de servicio

        public string? Diagnosis { get; set; } // Diagnóstico

        public string? Observations { get; set; } // Observaciones

        [StringLength(255)]
        public string? Recommendations { get; set; } // Recomendaciones

        // Navegación
        [ForeignKey("CareId")]
        [InverseProperty("MedicalServices")]
        public virtual MedicalCare? MedicalCare { get; set; }
    }

}
