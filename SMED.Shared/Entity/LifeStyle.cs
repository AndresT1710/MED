using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class LifeStyle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LifeStyleId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("LifeStyleNavigation")]
        public virtual ICollection<LifeStyleHistory> LifeStyleHistories { get; set; } = new List<LifeStyleHistory>();
    }
}
