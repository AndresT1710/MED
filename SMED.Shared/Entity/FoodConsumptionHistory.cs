using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.Entity
{
    public class FoodConsumptionHistory
    {
        [Key]
        public int FoodConsumptionHistoryId { get; set; }

        [StringLength(50)]
        public string HistoryNumber { get; set; } = null!;


        [StringLength(50)]
        public string? Hour { get; set; }


        [StringLength(255)]
        public string? Place { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Amount { get; set; }



        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }


        public int ClinicalHistoryId { get; set; }
        public int? FoodId { get; set; }

        [ForeignKey("FoodId")]
        [InverseProperty("FoodConsumptionHistories")]
        public virtual Food? FoodNavigation { get; set; }

        [ForeignKey("ClinicalHistoryId")]
        [InverseProperty("FoodConsumptionHistories")]
        public virtual ClinicalHistory HistoryNavigation { get; set; } = null!;


    }
}
