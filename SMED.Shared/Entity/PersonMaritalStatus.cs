using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonMaritalStatus
    {
        [Key]
        public int PersonId { get; set; }

        public int? MaritalStatusId { get; set; }

        [ForeignKey("MaritalStatusId")]
        [InverseProperty("PersonMaritalStatuses")]
        public virtual MaritalStatus? MaritalStatusNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonMaritalStatus")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
