﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMED.Shared.DTOs
{
    public class FoodConsumptionHistoryDTO
    {
        public int FoodConsumptionHistoryId { get; set; }
        public string HistoryNumber { get; set; } = null!;
        public string? Hour { get; set; }
        public string? Place { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; }
        public int ClinicalHistoryId { get; set; }
        public int? FoodId { get; set; }
        public string? FoodName { get; set; }
    }
}
