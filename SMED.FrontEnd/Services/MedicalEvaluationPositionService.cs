using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalEvaluationPositionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalEvaluationPositionService> _logger;

        public MedicalEvaluationPositionService(HttpClient httpClient, ILogger<MedicalEvaluationPositionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalEvaluationPositionDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalEvaluationPosition");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalEvaluationPositionDTO>>();
                }
                _logger.LogWarning("Error al obtener posiciones de evaluación médica: {StatusCode}", response.StatusCode);
                return new List<MedicalEvaluationPositionDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener posiciones de evaluación médica");
                return new List<MedicalEvaluationPositionDTO>();
            }
        }

        public async Task<MedicalEvaluationPositionDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalEvaluationPosition/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalEvaluationPositionDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener posición de evaluación médica por ID: {Id}", id);
                return null;
            }
        }
    }
}