using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ColorService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ColorService> _logger;

        public ColorService(HttpClient httpClient, ILogger<ColorService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ColorDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Color");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ColorDTO>>();
                }
                _logger.LogWarning("Error al obtener colores: {StatusCode}", response.StatusCode);
                return new List<ColorDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener colores");
                return new List<ColorDTO>();
            }
        }

        public async Task<ColorDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Color/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ColorDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener color por ID: {Id}", id);
                return null;
            }
        }
    }
}