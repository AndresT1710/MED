using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PersonId { get; set; }

        [StringLength(255)]
        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("User")]
        public virtual Person? PersonNavigation { get; set; }
    }

}
