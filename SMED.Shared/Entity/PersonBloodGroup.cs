using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonBloodGroup
    {
        [Key]
        public int PersonId { get; set; }

        public int? BloodGroupId { get; set; }

        [ForeignKey("BloodGroupId")]
        [InverseProperty("PersonBloodGroups")]
        public virtual BloodGroup? BloodGroupNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonBloodGroup")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
