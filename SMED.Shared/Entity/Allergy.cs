using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public  class Allergy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllergyId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("AllergyNavigation")]
        public virtual ICollection<AllergyHistory> AllergyHistories { get; set; } = new List<AllergyHistory>();
    }
}
