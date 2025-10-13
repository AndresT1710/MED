using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class MedicalReferral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? DateOfReferral { get; set; }
        public string Description { get; set; } = null!;

        public int MedicalCareId { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("MedicalReferrals")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}
