using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalEvaluationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalEvaluationService> _logger;

        public MedicalEvaluationService(HttpClient httpClient, ILogger<MedicalEvaluationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalEvaluationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalEvaluation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalEvaluationDTO>>();
                }
                _logger.LogWarning("Error al obtener evaluaciones médicas: {StatusCode}", response.StatusCode);
                return new List<MedicalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones médicas");
                return new List<MedicalEvaluationDTO>();
            }
        }

        public async Task<MedicalEvaluationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalEvaluationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluación médica por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<MedicalEvaluationDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allEvaluations = await GetAllAsync();
                return allEvaluations?
                    .Where(me => me.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<MedicalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones médicas por atención médica: {MedicalCareId}", medicalCareId);
                return new List<MedicalEvaluationDTO>();
            }
        }

        public async Task<(bool Success, MedicalEvaluationDTO? Data, string Error)> CreateAsync(MedicalEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalEvaluation", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicalEvaluationDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación médica");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(MedicalEvaluationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalEvaluation/{dto.MedicalEvaluationId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar evaluación médica");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalEvaluation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación médica");
                return (false, ex.Message);
            }
        }

        public async Task<List<MedicalEvaluationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MedicalEvaluationDTO>>($"api/MedicalEvaluation/ByCare/{medicalCareId}")
                    ?? new List<MedicalEvaluationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener evaluaciones médicas por CareId: {MedicalCareId}", medicalCareId);
                return new List<MedicalEvaluationDTO>();
            }
        }
    }
}