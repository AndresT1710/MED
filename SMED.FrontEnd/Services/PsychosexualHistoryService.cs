using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PsychosexualHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PsychosexualHistoryService> _logger;

        public PsychosexualHistoryService(HttpClient httpClient, ILogger<PsychosexualHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PsychosexualHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PsychosexualHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PsychosexualHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales psicosexuales: {StatusCode}", response.StatusCode);
                return new List<PsychosexualHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales psicosexuales");
                return new List<PsychosexualHistoryDTO>();
            }
        }

        public async Task<List<PsychosexualHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PsychosexualHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PsychosexualHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales psicosexuales por historia clínica: {StatusCode}", response.StatusCode);
                return new List<PsychosexualHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales psicosexuales por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<PsychosexualHistoryDTO>();
            }
        }

        public async Task<PsychosexualHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PsychosexualHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PsychosexualHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial psicosexual por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, PsychosexualHistoryDTO? Data, string Error)> CreateAsync(PsychosexualHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PsychosexualHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<PsychosexualHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial psicosexual");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(PsychosexualHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PsychosexualHistory/{dto.PsychosexualHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial psicosexual");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PsychosexualHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial psicosexual");
                return (false, ex.Message);
            }
        }
    }
}