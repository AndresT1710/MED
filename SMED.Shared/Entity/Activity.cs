using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        public string? Name { get; set; }

        public int? SessionId { get; set; }
        [ForeignKey("SessionId")]
        [InverseProperty("Activities")]
        public virtual Sessions? Session { get; set; }

        public int? TypeOfActivityId { get; set; }
        [ForeignKey("TypeOfActivityId")]
        [InverseProperty("Activities")]
        public virtual TypeOfActivity? TypeOfActivity { get; set; }
    }

}
