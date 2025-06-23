using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalReferral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? DateOfReferral { get; set; }
        public string Description { get; set; } = null!;
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        [InverseProperty("MedicalReferrals")]
        public virtual Service Service { get; set; } = null!;

        public int MedicalCareId { get; set; }  // FK a MedicalCare

        [ForeignKey("MedicalCareId")]
        [InverseProperty("MedicalReferral")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;

    }
}
