using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class BloodGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [InverseProperty("BloodGroupNavigation")]
        public virtual ICollection<PersonBloodGroup> PersonBloodGroups { get; set; } = new List<PersonBloodGroup>();
    }

}
