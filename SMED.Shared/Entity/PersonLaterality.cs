using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PersonLaterality
    {
        [Key]
        public int PersonId { get; set; }

        public int? LateralityId { get; set; }

        [ForeignKey("LateralityId")]
        [InverseProperty("PersonLateralities")]
        public virtual Laterality? LateralityNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonLaterality")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
