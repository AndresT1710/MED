using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SwellingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SwellingService> _logger;

        public SwellingService(HttpClient httpClient, ILogger<SwellingService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SwellingDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Swelling");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SwellingDTO>>();
                }
                _logger.LogWarning("Error al obtener tumefacciones: {StatusCode}", response.StatusCode);
                return new List<SwellingDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tumefacciones");
                return new List<SwellingDTO>();
            }
        }

        public async Task<SwellingDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Swelling/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SwellingDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tumefacción por ID: {Id}", id);
                return null;
            }
        }
    }
}