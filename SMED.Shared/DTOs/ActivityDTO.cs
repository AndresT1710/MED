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
        public string? NameActivity { get; set; }
        public DateTime? DateActivity { get; set; }
        public int? PsychologySessionId { get; set; }
        public int? TypeOfActivityId { get; set; }

        // Propiedades de navegación para mostrar información relacionada
        public string? SessionDescription { get; set; }
        public string? PsychologySessionDescription { get; set; }
        public string? TypeOfActivityName { get; set; }
    }

}
