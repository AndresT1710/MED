using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [InverseProperty("Service")]
        public virtual ICollection<Interconsultation> Interconsultations { get; set; } = new List<Interconsultation>();
        [InverseProperty("Service")]
        public virtual ICollection<MedicalReferral> MedicalReferrals { get; set; } = new List<MedicalReferral>();
    }
}
