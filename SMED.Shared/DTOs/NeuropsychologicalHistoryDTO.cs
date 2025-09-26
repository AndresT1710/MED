using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class NeuropsychologicalHistoryDTO
    {
        public int NeuropsychologicalHistoryId { get; set; }
        public string HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string Description { get; set; }
    }
}