using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class MedicalInsurance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [InverseProperty("MedicalInsuranceNavigation")]
        public virtual ICollection<PersonMedicalInsurance> PersonMedicalInsurances { get; set; } = new List<PersonMedicalInsurance>();
    }

}
