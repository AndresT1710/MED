namespace SMED.Shared.DTOs
{
    public class TransfusionsHistoryDTO
    {
        public int TransfusionsHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int? ClinicalHistoryId { get; set; }
        public string? TransfusionReason { get; set; }
        public DateTime? TransfusionDate { get; set; }
        public string? Observations { get; set; }
    }
}