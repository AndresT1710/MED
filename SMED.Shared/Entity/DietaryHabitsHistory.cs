using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class DietaryHabitsHistory
    {

        [Key]
        public int DietaryHabitHistoryId { get; set; }

        [StringLength(50)]
        public string MedicalRecordNumber { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }



        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }



        public int ClinicalHistoryId { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("DietaryHabitHistories")]
        public virtual ClinicalHistory MedicalRecordNavigation { get; set; } = null!;


    }
}
