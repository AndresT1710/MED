﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class DiseaseDTO
    {
        public int DiseaseId { get; set; }
        public string Name { get; set; } = null!;
        public int DiseaseTypeId { get; set; }
    }
}
