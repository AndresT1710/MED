using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class TrophismService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TrophismService> _logger;

        public TrophismService(HttpClient httpClient, ILogger<TrophismService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TrophismDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Trophism");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TrophismDTO>>();
                }
                _logger.LogWarning("Error al obtener trofismos: {StatusCode}", response.StatusCode);
                return new List<TrophismDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener trofismos");
                return new List<TrophismDTO>();
            }
        }

        public async Task<TrophismDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Trophism/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TrophismDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener trofismo por ID: {Id}", id);
                return null;
            }
        }
    }
}