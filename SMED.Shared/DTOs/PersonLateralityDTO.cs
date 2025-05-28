using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PersonLateralityDTO
    {
        public int PersonId { get; set; }
        public int? LateralityId { get; set; }
        public string NameLaterality { get; set; }
    }
}
