using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class OsteoarticularEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OsteoarticularEvaluationService> _logger;

        public OsteoarticularEvaluationService(HttpClient httpClient, ILogger<OsteoarticularEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<OsteoarticularEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/OsteoarticularEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<OsteoarticularEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener evaluaciones osteoarticulares: {StatusCode}", response.StatusCode);
                return new List<OsteoarticularEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones osteoarticulares");
                return new List<OsteoarticularEvaluationDTO>();
            }
        }

        public async Task<OsteoarticularEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/OsteoarticularEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OsteoarticularEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluación osteoarticular por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<OsteoarticularEvaluationDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allEvaluations = await GetAllAsync();
                return allEvaluations?
                    .Where(oe => oe.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<OsteoarticularEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones osteoarticulares por atención médica: {MedicalCareId}", medicalCareId);
                return new List<OsteoarticularEvaluationDTO>();
            }
        }

        public async Task<(bool Success, OsteoarticularEvaluationDTO? Data, string Error)> CreateAsync(OsteoarticularEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/OsteoarticularEvaluation", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<OsteoarticularEvaluationDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación osteoarticular");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(OsteoarticularEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/OsteoarticularEvaluation/{dto.OsteoarticularEvaluationId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar evaluación osteoarticular");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/OsteoarticularEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación osteoarticular");
                return (false, ex.Message);
            }
        }

        public async Task<List<OsteoarticularEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<OsteoarticularEvaluationDTO>>($"api/OsteoarticularEvaluation/ByCare/{medicalCareId}")
                    ?? new List<OsteoarticularEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones osteoarticulares por CareId: {MedicalCareId}", medicalCareId);
                return new List<OsteoarticularEvaluationDTO>();
            }
        }
    }
}