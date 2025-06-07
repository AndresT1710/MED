using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class LifeStyleHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LifeStyleHistoryService> _logger;

        public LifeStyleHistoryService(HttpClient httpClient, ILogger<LifeStyleHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<LifeStyleHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/LifeStyleHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LifeStyleHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de estilos de vida: {StatusCode}", response.StatusCode);
                return new List<LifeStyleHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de estilos de vida");
                return new List<LifeStyleHistoryDTO>();
            }
        }

        public async Task<List<LifeStyleHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/LifeStyleHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LifeStyleHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de estilos de vida por historia clínica: {StatusCode}", response.StatusCode);
                return new List<LifeStyleHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de estilos de vida por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<LifeStyleHistoryDTO>();
            }
        }

        public async Task<LifeStyleHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/LifeStyleHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LifeStyleHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de estilo de vida por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, LifeStyleHistoryDTO? Data, string Error)> CreateAsync(LifeStyleHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/LifeStyleHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<LifeStyleHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de estilo de vida");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(LifeStyleHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/LifeStyleHistory/{dto.LifeStyleHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de estilo de vida");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/LifeStyleHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de estilo de vida");
                return (false, ex.Message);
            }
        }
    }
}
