using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ShadeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShadeService> _logger;

        public ShadeService(HttpClient httpClient, ILogger<ShadeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ShadeDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Shade");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ShadeDTO>>();
                }
                _logger.LogWarning("Error al obtener tonos: {StatusCode}", response.StatusCode);
                return new List<ShadeDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tonos");
                return new List<ShadeDTO>();
            }
        }

        public async Task<ShadeDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Shade/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ShadeDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tono por ID: {Id}", id);
                return null;
            }
        }
    }
}