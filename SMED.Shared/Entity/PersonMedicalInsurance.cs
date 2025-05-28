using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonMedicalInsurance
    {
        [Key]
        public int Id { get; set; }

        public int? PersonId { get; set; }

        public int? MedicalInsuranceId { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonMedicalInsurances")]
        public virtual Person? PersonNavigation { get; set; }

        [ForeignKey("MedicalInsuranceId")]
        [InverseProperty("PersonMedicalInsurances")]
        public virtual MedicalInsurance? MedicalInsuranceNavigation { get; set; }
    }

}
