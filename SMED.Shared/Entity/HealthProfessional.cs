using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class HealthProfessional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HealthProfessionalId { get; set; }

        public int? HealthProfessionalTypeId { get; set; }

        [StringLength(50)]
        public string? RegistrationNumber { get; set; }

        [ForeignKey("HealthProfessionalId")]
        [InverseProperty("HealthProfessional")]
        public virtual Person PersonNavigation { get; set; } = null!;

        [ForeignKey("HealthProfessionalTypeId")]
        [InverseProperty("HealthProfessionals")]
        public virtual HealthProfessionalType? HealthProfessionalTypeNavigation { get; set; }

        [InverseProperty("HealthProfessional")]
        public virtual ICollection<MedicalCare> MedicalCares { get; set; } = new List<MedicalCare>();

        [InverseProperty("HealthProfessional")]
        public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();

        [InverseProperty("HealthProfessional")]
        public virtual ICollection<MedicalProcedure> MedicalProceduresAsHealthProfessional { get; set; } = new List<MedicalProcedure>();

        [InverseProperty("TreatingPhysician")]
        public virtual ICollection<MedicalProcedure> MedicalProceduresAsTreatingPhysician { get; set; } = new List<MedicalProcedure>();

        [InverseProperty("AttendedByProfessional")]
        public virtual ICollection<MedicalReferral> AttendedMedicalReferrals { get; set; } = new List<MedicalReferral>();
    }

}
