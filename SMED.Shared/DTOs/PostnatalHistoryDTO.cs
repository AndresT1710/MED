using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PostnatalHistoryDTO
    {
        public int PostNatalId { get; set; }
        public string? HistoryNumber { get; set; }
        public int ClinicalHistoryId { get; set; }
        public string? Description { get; set; }

        public bool? Bcg { get; set; }
        public bool? Rotavirus { get; set; }
        public bool? Pentavalente { get; set; }
        public bool? Influenza { get; set; }
        public bool? Varicela { get; set; }
        public bool? HepatitisB { get; set; }
        public bool? TripleViral { get; set; }
        public bool? PolioVirus { get; set; }
        public bool? Neumococo { get; set; }
        public string? Observations { get; set; }
    }
}