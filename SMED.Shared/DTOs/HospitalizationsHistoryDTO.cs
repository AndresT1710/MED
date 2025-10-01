namespace SMED.Shared.DTOs
{
    public class HospitalizationsHistoryDTO
    {
        public int HospitalizationsHistoryId { get; set; }
        public string? HistoryNumber { get; set; }
        public int? ClinicalHistoryId { get; set; }
        public string? HospitalizationReason { get; set; }
        public DateTime? HospitalizationDate { get; set; }
        public string? HospitalizationPlace { get; set; }
        public string? Observations { get; set; }
    }
}