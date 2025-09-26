using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class ResultType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultTypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<SpecialTest> SpecialTests { get; set; } = new List<SpecialTest>();
    }
}
