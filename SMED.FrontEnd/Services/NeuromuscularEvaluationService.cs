using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class NeuromuscularEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NeuromuscularEvaluationService> _logger;

        public NeuromuscularEvaluationService(HttpClient httpClient, ILogger<NeuromuscularEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NeuromuscularEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/NeuromuscularEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<NeuromuscularEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener evaluaciones neuromusculares: {StatusCode}", response.StatusCode);
                return new List<NeuromuscularEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones neuromusculares");
                return new List<NeuromuscularEvaluationDTO>();
            }
        }

        public async Task<NeuromuscularEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/NeuromuscularEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<NeuromuscularEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluación neuromuscular por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<NeuromuscularEvaluationDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allEvaluations = await GetAllAsync();
                return allEvaluations?
                    .Where(ne => ne.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<NeuromuscularEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones neuromusculares por atención médica: {MedicalCareId}", medicalCareId);
                return new List<NeuromuscularEvaluationDTO>();
            }
        }

        public async Task<(bool Success, NeuromuscularEvaluationDTO? Data, string Error)> CreateAsync(NeuromuscularEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/NeuromuscularEvaluation", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<NeuromuscularEvaluationDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación neuromuscular");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(NeuromuscularEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/NeuromuscularEvaluation/{dto.NeuromuscularEvaluationId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar evaluación neuromuscular");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/NeuromuscularEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación neuromuscular");
                return (false, ex.Message);
            }
        }
    }
}