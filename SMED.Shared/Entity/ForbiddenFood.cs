using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMED.Shared.Entity
{
    public class ForbiddenFood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ForbiddenFoodId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        [InverseProperty("ForbiddenFoods")]
        public virtual Food Food { get; set; } = null!;

        public int CareId { get; set; }

        [ForeignKey("CareId")]
        [InverseProperty("ForbiddenFoods")]
        public virtual MedicalCare MedicalCare { get; set; } = null!;
    }
}
