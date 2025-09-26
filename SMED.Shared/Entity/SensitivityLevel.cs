using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class SensitivityLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensitivityLevelId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SensitivityEvaluation> SensitivityEvaluations { get; set; }
    }
}
