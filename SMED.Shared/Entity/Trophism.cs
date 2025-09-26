using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Trophism
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrophismId { get; set; }
        public string? Name { get; set; }

        [InverseProperty("Trophism")]
        public virtual ICollection<NeuromuscularEvaluation> NeuromuscularEvaluations { get; set; } = new List<NeuromuscularEvaluation>();
    }

}
