using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class GenderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GenderService> _logger;

        public GenderService(HttpClient httpClient, ILogger<GenderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<GenderDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Gender");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<GenderDTO>>();
                }
                _logger.LogWarning("Error al obtener géneros: {StatusCode}", response.StatusCode);
                return new List<GenderDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener géneros");
                return new List<GenderDTO>();
            }
        }

        public async Task<GenderDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Gender/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GenderDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener género por ID: {Id}", id);
                return null;
            }
        }
    }
}