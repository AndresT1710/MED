﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class IndicationsDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int TreatmentId { get; set; }
    }

}
