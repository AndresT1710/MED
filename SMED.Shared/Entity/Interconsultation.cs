using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Interconsultation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? InterconsultationDate { get; set; }
        public string Reason { get; set; } = null!;
        public int DiagnosisId { get; set; }

        [ForeignKey("DiagnosisId")]
        [InverseProperty("Interconsultations")]
        public virtual Diagnosis Diagnosis { get; set; } = null!;

        [ForeignKey("ServiceId")]
        [InverseProperty("Interconsultations")]
        public virtual Service Service { get; set; } = null!;
    }
}
