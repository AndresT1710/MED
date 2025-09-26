using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class TypeOfActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeOfActivityId { get; set; }
        public string? Name { get; set; }


        [InverseProperty("TypeOfActivity")]
        public virtual ICollection<Activity>? Activities { get; set; }
    }
}
