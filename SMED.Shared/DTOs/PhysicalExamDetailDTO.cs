using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PhysicalExamDetailDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int PhysicalExamTypeId { get; set; }
    }
}
