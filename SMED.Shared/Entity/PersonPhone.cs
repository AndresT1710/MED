using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class PersonPhone
    {
        [Key]
        public int PersonId { get; set; }

        [StringLength(10)]
        public string? Mobile { get; set; }

        [StringLength(10)]
        public string? Landline { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonPhone")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
