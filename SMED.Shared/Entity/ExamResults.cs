using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class ExamResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LinkPdf { get; set; }
        public DateTime? ExamDate { get; set; }
        public string Description { get; set; } = null!;
        public int ExamTypeId { get; set; }

        [ForeignKey("ExamTypeId")]
        [InverseProperty("ExamResults")]
        public virtual ExamType ExamTypeNavigation { get; set; } = null!;
    }
}
