using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class OrderDiagnosisDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DiagnosisId { get; set; }
    }

}
