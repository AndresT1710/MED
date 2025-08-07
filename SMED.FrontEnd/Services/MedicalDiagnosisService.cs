using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalDiagnosisService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalDiagnosisService> _logger;

        public MedicalDiagnosisService(HttpClient httpClient, ILogger<MedicalDiagnosisService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalDiagnosisDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalDiagnosis");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalDiagnosisDTO>>();
                }
                _logger.LogWarning("Error al obtener diagnósticos médicos: {StatusCode}", response.StatusCode);
                return new List<MedicalDiagnosisDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los diagnósticos médicos");
                return new List<MedicalDiagnosisDTO>();
            }
        }

        public async Task<MedicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalDiagnosis/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalDiagnosisDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diagnóstico médico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, MedicalDiagnosisDTO? Data, string Error)> CreateAsync(MedicalDiagnosisDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalDiagnosis", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicalDiagnosisDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear diagnóstico médico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, MedicalDiagnosisDTO? Data, string Error)> UpdateAsync(MedicalDiagnosisDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalDiagnosis/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<MedicalDiagnosisDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar diagnóstico médico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalDiagnosis/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar diagnóstico médico");
                return (false, ex.Message);
            }
        }

        // ✅ Método para obtener diagnósticos por MedicalCareId
        public async Task<List<MedicalDiagnosisDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allDiagnoses = await GetAllAsync();
                return allDiagnoses?.Where(d => d.MedicalCareId == medicalCareId).ToList() ?? new List<MedicalDiagnosisDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diagnósticos por MedicalCareId: {MedicalCareId}", medicalCareId);
                return new List<MedicalDiagnosisDTO>();
            }
        }

    }
}
