using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class PersonResidenceDTO
    {
        public int? CityId { get; set; }
        public string? CityName { get; set; }

        public int? ProvinceId { get; set; }
        public string? ProvinceName { get; set; }
    }
}
