using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public partial class PersonEducation
    {
        [Key]
        public int PersonId { get; set; }

        public int? EducationLevelId { get; set; }

        [ForeignKey("EducationLevelId")]
        [InverseProperty("PersonEducations")]
        public virtual EducationLevel? EducationLevelNavigation { get; set; }

        [ForeignKey("PersonId")]
        [InverseProperty("PersonEducation")]
        public virtual Person PersonNavigation { get; set; } = null!;
    }

}
