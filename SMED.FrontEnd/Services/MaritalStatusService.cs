using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MaritalStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MaritalStatusService> _logger;

        public MaritalStatusService(HttpClient httpClient, ILogger<MaritalStatusService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MaritalStatusDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MaritalStatus");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MaritalStatusDTO>>();
                }
                _logger.LogWarning("Error al obtener estados civiles: {StatusCode}", response.StatusCode);
                return new List<MaritalStatusDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estados civiles");
                return new List<MaritalStatusDTO>();
            }
        }

        public async Task<MaritalStatusDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MaritalStatus/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MaritalStatusDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estado civil por ID: {Id}", id);
                return null;
            }
        }
    }
}