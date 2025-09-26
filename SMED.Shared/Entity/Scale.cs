using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Scale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScaleId { get; set; }

        public int Value { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PainScale> PainScales { get; set; } = new List<PainScale>();
    }
}
