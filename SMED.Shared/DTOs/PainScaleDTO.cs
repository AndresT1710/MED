public class PainScaleDTO
{
    public int PainScaleId { get; set; }
    public string Observation { get; set; }

    public int? ActionId { get; set; }
    public string? ActionName { get; set; }

    public int? ScaleId { get; set; }
    public int? ScaleValue { get; set; }
    public string? ScaleDescription { get; set; }

    public int? PainMomentId { get; set; }
    public string? PainMomentName { get; set; }

    public int? MedicalCareId { get; set; }
}
