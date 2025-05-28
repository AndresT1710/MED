using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PersonLaborActivity
    {
        [Key]
        public int PersonId { get; set; }

        public int? LaborActivityId { get; set; }

        [ForeignKey("LaborActivityId")]
        [InverseProperty("PersonLaborActivities")]
        public virtual LaborActivity? LaborActivityNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonLaborActivity")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
