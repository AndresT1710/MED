using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PsychopsychiatricHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PsychopsychiatricHistoryService> _logger;

        public PsychopsychiatricHistoryService(HttpClient httpClient, ILogger<PsychopsychiatricHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PsychopsychiatricHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PsychopsychiatricHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PsychopsychiatricHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales psicopsiquiátricos: {StatusCode}", response.StatusCode);
                return new List<PsychopsychiatricHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales psicopsiquiátricos");
                return new List<PsychopsychiatricHistoryDTO>();
            }
        }

        public async Task<List<PsychopsychiatricHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PsychopsychiatricHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PsychopsychiatricHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales psicopsiquiátricos por historia clínica: {StatusCode}", response.StatusCode);
                return new List<PsychopsychiatricHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales psicopsiquiátricos por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<PsychopsychiatricHistoryDTO>();
            }
        }

        public async Task<PsychopsychiatricHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PsychopsychiatricHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PsychopsychiatricHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial psicopsiquiátrico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, PsychopsychiatricHistoryDTO? Data, string Error)> CreateAsync(PsychopsychiatricHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PsychopsychiatricHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<PsychopsychiatricHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial psicopsiquiátrico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(PsychopsychiatricHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PsychopsychiatricHistory/{dto.PsychopsychiatricHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial psicopsiquiátrico");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PsychopsychiatricHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial psicopsiquiátrico");
                return (false, ex.Message);
            }
        }
    }
}