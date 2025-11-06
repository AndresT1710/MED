using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public partial class MentalFunction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MentalFunctionId { get; set; }
        public string MentalFunctionName { get; set; }

        public int TypeOfMentalFunctionId { get; set; }

        [ForeignKey("TypeOfMentalFunctionId")]
        [InverseProperty("MentalFunctions")]
        public virtual TypesOfMentalFunctions TypeOfMentalFunction { get; set; } = null!;

        [InverseProperty("MentalFunction")]
        public virtual ICollection<MentalFunctionsPsychology> MentalFunctionsPsychologies { get; set; } = new List<MentalFunctionsPsychology>();
    }
}
