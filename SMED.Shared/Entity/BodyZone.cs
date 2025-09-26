using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class BodyZone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodyZoneId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SensitivityEvaluation> SensitivityEvaluations { get; set; }
    }
}
