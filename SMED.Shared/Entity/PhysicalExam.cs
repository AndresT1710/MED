using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PhysicalExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhysicalExamId { get; set; }

        public string Extremities { get; set; }

        public int? PhysicalExamDetailId { get; set; }

        [ForeignKey("PhysicalExamDetailId")]
        public virtual PhysicalExamDetail? PhysicalExamDetail { get; set; }
    }


}
