using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class MedicalCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CareId { get; set; }

        [ForeignKey("LocationId")]
        [InverseProperty("MedicalCares")]
        public virtual PlaceOfAttention PlaceOfAttentionNavigation { get; set; } = null!;

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();

        [InverseProperty("MedicalCare")]
        public virtual VitalSigns? VitalSigns { get; set; }

        [InverseProperty("MedicalCare")]
        public virtual ICollection<Evolution> Evolutions { get; set; } = new List<Evolution>();

    }
}
