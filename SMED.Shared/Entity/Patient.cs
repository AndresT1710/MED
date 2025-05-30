using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class Patient
{
    [Key]
    public int PersonId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<MedicalVisit> MedicalVisits { get; set; } = new List<MedicalVisit>();


    [InverseProperty("Patient")]
    public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; } = new List<EmergencyContact>();

    [InverseProperty("Patient")]
    public virtual ClinicalHistory ClinicalHistory { get; set; }

    [InverseProperty("Patient")]
    public virtual PatientRelationship? PatientRelationship { get; set; }


    [ForeignKey("PersonId")]
    [InverseProperty("Patient")]
    public virtual Person PersonNavigation { get; set; } = null!;
}

}
