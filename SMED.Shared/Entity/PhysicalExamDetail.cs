using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class PhysicalExamDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public int PhysicalExamTypeId { get; set; }

        [ForeignKey("PhysicalExamTypeId")]
        [InverseProperty("PhysicalExamDetails")]
        public virtual PhysicalExamType ExamTypeNavigation { get; set; } = null!;

        public virtual PhysicalExam? PhysicalExam { get; set; }

    }


}
