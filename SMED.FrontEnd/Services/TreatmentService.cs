using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class TreatmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TreatmentService> _logger;

        public TreatmentService(HttpClient httpClient, ILogger<TreatmentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TreatmentDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Treatment");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos: {StatusCode}", response.StatusCode);
                return new List<TreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tratamientos");
                return new List<TreatmentDTO>();
            }
        }

        public async Task<TreatmentDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Treatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TreatmentDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamiento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, TreatmentDTO? Data, string Error)> CreateAsync(TreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Treatment", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<TreatmentDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tratamiento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, TreatmentDTO? Data, string Error)> UpdateAsync(TreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Treatment/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<TreatmentDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tratamiento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Treatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tratamiento");
                return (false, ex.Message);
            }
        }

        public async Task<List<TreatmentDTO>?> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Treatment/by-medical-diagnosis/{medicalDiagnosisId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos por diagnóstico: {StatusCode}", response.StatusCode);
                return new List<TreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamientos por MedicalDiagnosisId: {MedicalDiagnosisId}", medicalDiagnosisId);
                return new List<TreatmentDTO>();
            }
        }
    }
}
