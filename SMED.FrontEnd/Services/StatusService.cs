using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class StatusService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StatusService> _logger;

        public StatusService(HttpClient httpClient, ILogger<StatusService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<StatusDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Status");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<StatusDTO>>();
                }
                _logger.LogWarning("Error al obtener estados: {StatusCode}", response.StatusCode);
                return new List<StatusDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estados");
                return new List<StatusDTO>();
            }
        }

        public async Task<StatusDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Status/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<StatusDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estado por ID: {Id}", id);
                return null;
            }
        }
    }
}