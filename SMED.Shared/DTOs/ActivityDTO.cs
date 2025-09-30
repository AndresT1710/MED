using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class ActivityDTO
    {
        public int ActivityId { get; set; }
        public string? Name { get; set; }
        public int? SessionId { get; set; }
    }

}
