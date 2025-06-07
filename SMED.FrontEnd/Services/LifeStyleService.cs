using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class LifeStyleService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LifeStyleService> _logger;

        public LifeStyleService(HttpClient httpClient, ILogger<LifeStyleService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<LifeStyleDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/LifeStyle");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LifeStyleDTO>>();
                }
                _logger.LogWarning("Error al obtener estilos de vida: {StatusCode}", response.StatusCode);
                return new List<LifeStyleDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estilos de vida");
                return new List<LifeStyleDTO>();
            }
        }

        public async Task<LifeStyleDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/LifeStyle/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LifeStyleDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estilo de vida por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, LifeStyleDTO? Data, string Error)> CreateAsync(LifeStyleDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/LifeStyle", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<LifeStyleDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear estilo de vida");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(LifeStyleDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/LifeStyle/{dto.LifeStyleId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estilo de vida");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/LifeStyle/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar estilo de vida");
                return (false, ex.Message);
            }
        }
    }
}
