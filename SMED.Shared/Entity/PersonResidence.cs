using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PersonResidence
    {
        [Key]
        public int PersonId { get; set; }

        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("PersonResidences")]
        public virtual City? CityNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonResidence")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
