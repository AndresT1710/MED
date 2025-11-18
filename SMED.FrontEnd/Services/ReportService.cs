using System.Net.Http.Json;
using SMED.Shared.DTOs;
using Microsoft.JSInterop;

namespace SMED.FrontEnd.Services
{
    public class ReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public ReportService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        // MÉTODOS PARA GENERAR REPORTES (POST)
        public async Task<byte[]> GenerateMedicalCareReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/medical-care", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GeneratePatientReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/patient", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GenerateProfessionalReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/professional", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GenerateStatisticalReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/statistical", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        // NUEVOS MÉTODOS PARA GENERAR REPORTES
        public async Task<byte[]> GenerateProfessionalDetailReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/professional-detail", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GenerateLocationReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/location", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GenerateAreaReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/area", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<byte[]> GenerateTopPatientReportAsync(ReportRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reports/top-patients", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        // MÉTODOS PARA OBTENER DATOS (GET)
        public async Task<StatisticalReportDTO> GetStatisticalDataAsync(DateTime? startDate, DateTime? endDate, bool includeAllRecords = true)
        {
            var url = $"api/reports/statistical-data?includeAllRecords={includeAllRecords}";

            if (!includeAllRecords && startDate.HasValue && endDate.HasValue)
            {
                url += $"&startDate={startDate.Value:yyyy-MM-dd}&endDate={endDate.Value:yyyy-MM-dd}";
            }

            return await _httpClient.GetFromJsonAsync<StatisticalReportDTO>(url)
                ?? new StatisticalReportDTO();
        }

        public async Task<List<PatientReportDTO>> GetPatientReportDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<List<PatientReportDTO>>(
                $"api/reports/patient-data{queryParams}")
                ?? new List<PatientReportDTO>();
        }

        public async Task<List<ProfessionalReportDTO>> GetProfessionalReportDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<List<ProfessionalReportDTO>>(
                $"api/reports/professional-data{queryParams}")
                ?? new List<ProfessionalReportDTO>();
        }

        // NUEVOS MÉTODOS PARA OBTENER DATOS
        public async Task<ProfessionalDetailReportDTO> GetProfessionalDetailDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<ProfessionalDetailReportDTO>(
                $"api/reports/professional-detail-data{queryParams}")
                ?? new ProfessionalDetailReportDTO();
        }

        public async Task<LocationReportDTO> GetLocationReportDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<LocationReportDTO>(
                $"api/reports/location-data{queryParams}")
                ?? new LocationReportDTO();
        }

        public async Task<AreaReportDTO> GetAreaReportDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<AreaReportDTO>(
                $"api/reports/area-data{queryParams}")
                ?? new AreaReportDTO();
        }

        public async Task<List<PatientReportDTO>> GetTopPatientsDataAsync(ReportRequestDTO request)
        {
            var queryParams = BuildQueryString(request);
            return await _httpClient.GetFromJsonAsync<List<PatientReportDTO>>(
                $"api/reports/top-patients-data{queryParams}")
                ?? new List<PatientReportDTO>();
        }

        // MÉTODO AUXILIAR PARA CONSTRUIR QUERY STRING
        private string BuildQueryString(ReportRequestDTO request)
        {
            var queryParams = new List<string>();

            if (request.StartDate.HasValue)
                queryParams.Add($"startDate={request.StartDate.Value:yyyy-MM-dd}");

            if (request.EndDate.HasValue)
                queryParams.Add($"endDate={request.EndDate.Value:yyyy-MM-dd}");

            if (request.LocationId.HasValue)
                queryParams.Add($"locationId={request.LocationId.Value}");

            if (request.HealthProfessionalId.HasValue)
                queryParams.Add($"healthProfessionalId={request.HealthProfessionalId.Value}");

            if (request.PlaceOfAttentionId.HasValue)
                queryParams.Add($"placeOfAttentionId={request.PlaceOfAttentionId.Value}");

            if (!string.IsNullOrEmpty(request.DocumentNumber))
                queryParams.Add($"documentNumber={Uri.EscapeDataString(request.DocumentNumber)}");

            if (!string.IsNullOrEmpty(request.Area))
                queryParams.Add($"area={Uri.EscapeDataString(request.Area)}");

            return queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
        }

        // MÉTODO PARA DESCARGAR ARCHIVOS
        public async Task DownloadReport(byte[] content, string fileName, string contentType = "application/pdf")
        {
            var fileStream = new MemoryStream(content);
            using var streamRef = new DotNetStreamReference(fileStream);

            await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, contentType, streamRef);
        }

        // MÉTODO CONVENIENTE PARA GENERAR Y DESCARGAR EN UN SOLO PASO
        public async Task GenerateAndDownloadReport(Func<ReportRequestDTO, Task<byte[]>> reportMethod, ReportRequestDTO request, string fileName)
        {
            var content = await reportMethod(request);
            await DownloadReport(content, fileName);
        }

        public async Task<string> DebugReportRequest(ReportRequestDTO request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/reports/debug", request);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}