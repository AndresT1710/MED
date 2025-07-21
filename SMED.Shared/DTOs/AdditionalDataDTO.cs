using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class AdditionalDataDTO
    {
        public int AdditionalDataId { get; set; }
        public string Observacion { get; set; } = null!;
        public int MedicalCareId { get; set; }
    }

}
