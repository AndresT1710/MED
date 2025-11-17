using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class Sessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionsId { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Treatment { get; set; }
        public bool? MedicalDischarge { get; set; }
        public string? Observations { get; set; }
        public int? MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("Sessions")]
        public virtual MedicalCare? MedicalCare { get; set; }


        // En Sessions entity, agregar esta propiedad en la sección de InverseProperty
        [InverseProperty("Session")]
        public virtual ICollection<Advance> Advances { get; set; } = new List<Advance>();
    }

}
