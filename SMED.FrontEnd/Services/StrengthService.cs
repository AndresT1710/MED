using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class StrengthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StrengthService> _logger;

        public StrengthService(HttpClient httpClient, ILogger<StrengthService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<StrengthDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Strength");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<StrengthDTO>>();
                }
                _logger.LogWarning("Error al obtener fuerzas: {StatusCode}", response.StatusCode);
                return new List<StrengthDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fuerzas");
                return new List<StrengthDTO>();
            }
        }

        public async Task<StrengthDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Strength/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<StrengthDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fuerza por ID: {Id}", id);
                return null;
            }
        }
    }
}