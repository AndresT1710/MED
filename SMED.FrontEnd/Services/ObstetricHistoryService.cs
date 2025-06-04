using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ObstetricHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ObstetricHistoryService> _logger;

        public ObstetricHistoryService(HttpClient httpClient, ILogger<ObstetricHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ObstetricHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ObstetricHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ObstetricHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias obstétricas: {StatusCode}", response.StatusCode);
                return new List<ObstetricHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias obstétricas");
                return new List<ObstetricHistoryDTO>();
            }
        }

        public async Task<ObstetricHistoryDTO?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ObstetricHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ObstetricHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia obstétrica por ID clínico: {ClinicalHistoryId}", clinicalHistoryId);
                return null;
            }
        }

        public async Task<ObstetricHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ObstetricHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ObstetricHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia obstétrica por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, ObstetricHistoryDTO? Data, string Error)> CreateAsync(ObstetricHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ObstetricHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ObstetricHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia obstétrica");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ObstetricHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ObstetricHistory/{dto.ObstetricHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia obstétrica");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ObstetricHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia obstétrica");
                return (false, ex.Message);
            }
        }
    }
}