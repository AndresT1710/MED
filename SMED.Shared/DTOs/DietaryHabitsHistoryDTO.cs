using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class DietaryHabitsHistoryDTO
    {
        public int DietaryHabitHistoryId { get; set; }
        public string MedicalRecordNumber { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public int ClinicalHistoryId { get; set; }
    }
}
