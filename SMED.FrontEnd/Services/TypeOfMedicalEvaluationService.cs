using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class TypeOfMedicalEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TypeOfMedicalEvaluationService> _logger;

        public TypeOfMedicalEvaluationService(HttpClient httpClient, ILogger<TypeOfMedicalEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TypeOfMedicalEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/TypeOfMedicalEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TypeOfMedicalEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener tipos de evaluación médica: {StatusCode}", response.StatusCode);
                return new List<TypeOfMedicalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipos de evaluación médica");
                return new List<TypeOfMedicalEvaluationDTO>();
            }
        }

        public async Task<TypeOfMedicalEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/TypeOfMedicalEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TypeOfMedicalEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipo de evaluación médica por ID: {Id}", id);
                return null;
            }
        }
    }
}