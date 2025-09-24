using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class NeurologicalExamType
    {
        [Key]
        public int NeurologicalExamTypeId { get; set; }

        public string Name { get; set; }

        [InverseProperty("NeurologicalExamType")]
        public virtual ICollection<NeurologicalExam> NeurologicalExams { get; set; } = new List<NeurologicalExam>();
    }
}