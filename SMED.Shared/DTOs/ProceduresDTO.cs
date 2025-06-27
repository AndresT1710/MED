using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ProceduresDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TypeOfProcedureId { get; set; }
        public int? MedicalCareId { get; set; }
        public string TypeOfProcedureName { get; set; } = string.Empty;
    }

}
