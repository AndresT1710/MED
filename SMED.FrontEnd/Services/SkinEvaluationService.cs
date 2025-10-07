using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SkinEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SkinEvaluationService> _logger;

        public SkinEvaluationService(HttpClient httpClient, ILogger<SkinEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SkinEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SkinEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SkinEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener evaluaciones de piel: {StatusCode}", response.StatusCode);
                return new List<SkinEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones de piel");
                return new List<SkinEvaluationDTO>();
            }
        }

        public async Task<SkinEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SkinEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SkinEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluación de piel por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<SkinEvaluationDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SkinEvaluation/by-medical-care/{medicalCareId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SkinEvaluationDTO>>();
                }
                return new List<SkinEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones de piel por atención médica: {MedicalCareId}", medicalCareId);
                return new List<SkinEvaluationDTO>();
            }
        }

        public async Task<(bool Success, SkinEvaluationDTO? Data, string Error)> CreateAsync(SkinEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SkinEvaluation", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SkinEvaluationDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación de piel");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SkinEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SkinEvaluation/{dto.SkinEvaluationId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar evaluación de piel");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SkinEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación de piel");
                return (false, ex.Message);
            }
        }
    }
}