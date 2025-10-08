using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PosturalEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PosturalEvaluationService> _logger;

        public PosturalEvaluationService(HttpClient httpClient, ILogger<PosturalEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PosturalEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PosturalEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PosturalEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener evaluaciones posturales: {StatusCode}", response.StatusCode);
                return new List<PosturalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones posturales");
                return new List<PosturalEvaluationDTO>();
            }
        }

        public async Task<PosturalEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PosturalEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PosturalEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluación postural por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<PosturalEvaluationDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allEvaluations = await GetAllAsync();
                return allEvaluations?
                    .Where(pe => pe.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<PosturalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones posturales por atención médica: {MedicalCareId}", medicalCareId);
                return new List<PosturalEvaluationDTO>();
            }
        }

        public async Task<(bool Success, PosturalEvaluationDTO? Data, string Error)> CreateAsync(PosturalEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PosturalEvaluation", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<PosturalEvaluationDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación postural");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(PosturalEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PosturalEvaluation/{dto.PosturalEvaluationId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar evaluación postural");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PosturalEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación postural");
                return (false, ex.Message);
            }
        }
    }
}