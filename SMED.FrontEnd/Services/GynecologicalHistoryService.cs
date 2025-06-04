using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class GynecologicalHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GynecologicalHistoryService> _logger;

        public GynecologicalHistoryService(HttpClient httpClient, ILogger<GynecologicalHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<GynecologicalHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/GynecologicalHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<GynecologicalHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias ginecológicas: {StatusCode}", response.StatusCode);
                return new List<GynecologicalHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias ginecológicas");
                return new List<GynecologicalHistoryDTO>();
            }
        }

        public async Task<GynecologicalHistoryDTO?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/GynecologicalHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GynecologicalHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia ginecológica por ID clínico: {ClinicalHistoryId}", clinicalHistoryId);
                return null;
            }
        }

        public async Task<GynecologicalHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/GynecologicalHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GynecologicalHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia ginecológica por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, GynecologicalHistoryDTO? Data, string Error)> CreateAsync(GynecologicalHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/GynecologicalHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<GynecologicalHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia ginecológica");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(GynecologicalHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/GynecologicalHistory/{dto.GynecologicalHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia ginecológica");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/GynecologicalHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia ginecológica");
                return (false, ex.Message);
            }
        }
    }
}