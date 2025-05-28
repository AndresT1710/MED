using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PersonReligion
    {
        [Key]
        public int PersonId { get; set; }

        public int? ReligionId { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonReligion")]
        public virtual Person PersonNavigation { get; set; } = null!;

        [ForeignKey("ReligionId")]
        [InverseProperty("PersonReligions")]
        public virtual Religion? ReligionNavigation { get; set; }
    }

}
