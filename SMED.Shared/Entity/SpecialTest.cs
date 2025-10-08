using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class SpecialTest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialTestId { get; set; }

        public string? Test { get; set; }
        public string? Observations { get; set; }

        public int ResultTypeId { get; set; }
        public int MedicalCareId { get; set; }

        [ForeignKey("ResultTypeId")]
        public virtual ResultType ResultType { get; set; }

        [ForeignKey("MedicalCareId")]
        public virtual MedicalCare MedicalCare { get; set; }
    }
}
