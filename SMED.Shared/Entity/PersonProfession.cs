using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonProfession
    {
        [Key]
        public int PersonId { get; set; }

        [Key]
        public int ProfessionId { get; set; }

        [ForeignKey(nameof(PersonId))]
        [InverseProperty("PersonProfessions")]
        public virtual Person? Person { get; set; }

        [ForeignKey(nameof(ProfessionId))]
        [InverseProperty("PersonProfessions")]
        public virtual Profession? ProfessionNavigation { get; set; }
    }

}
