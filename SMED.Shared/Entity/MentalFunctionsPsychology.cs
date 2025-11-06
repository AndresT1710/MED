using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class MentalFunctionsPsychology
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MentalFunctionsPsychologyId { get; set; }

        public int? MedicalCareId { get; set; }
        public int? MentalFunctionId { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [ForeignKey("MedicalCareId")]
        [InverseProperty("MentalFunctionsPsychologies")]
        public virtual MedicalCare? MedicalCare { get; set; }

        [ForeignKey("MentalFunctionId")]
        [InverseProperty("MentalFunctionsPsychologies")]
        public virtual MentalFunction? MentalFunction { get; set; }
    }
}
