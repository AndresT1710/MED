using SMED.Shared.DTOs;

namespace SMED.BackEnd.Repositories.Interface
{
    public interface IReportRepository
    {
        Task<ReportResultDTO> GenerateMedicalCareReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GeneratePatientReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateProfessionalReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateStatisticalReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateProfessionalDetailReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateLocationReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateAreaReportAsync(ReportRequestDTO request);
        Task<ReportResultDTO> GenerateTopPatientReportAsync(ReportRequestDTO request);

        // Métodos para obtener datos
        Task<StatisticalReportDTO> GetStatisticalDataAsync(DateTime? startDate, DateTime? endDate, bool includeAllRecords = true);
        Task<List<PatientReportDTO>> GetPatientReportDataAsync(ReportRequestDTO request);
        Task<List<ProfessionalReportDTO>> GetProfessionalReportDataAsync(ReportRequestDTO request);
        Task<ProfessionalDetailReportDTO> GetProfessionalDetailDataAsync(ReportRequestDTO request);
        Task<LocationReportDTO> GetLocationReportDataAsync(ReportRequestDTO request);
        Task<AreaReportDTO> GetAreaReportDataAsync(ReportRequestDTO request);
        Task<List<PatientReportDTO>> GetTopPatientsDataAsync(ReportRequestDTO request);

    }
}