using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class FoodIntoleranceHistory
    {
        [Key]
        public int FoodIntoleranceHistoryId { get; set; }

        [StringLength(50)]

        public int ClinicalHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }

        public int? FoodId { get; set; }

        [ForeignKey("FoodId")]
        [InverseProperty("FoodIntoleranceHistories")]
        public virtual Food? FoodNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("FoodIntoleranceHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;
    }
}
