using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PainMoment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PainMomentId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PainScale> PainScales { get; set; } = new List<PainScale>();
    }
}
