using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class EdemaService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EdemaService> _logger;

        public EdemaService(HttpClient httpClient, ILogger<EdemaService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<EdemaDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Edema");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<EdemaDTO>>();
                }
                _logger.LogWarning("Error al obtener edemas: {StatusCode}", response.StatusCode);
                return new List<EdemaDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener edemas");
                return new List<EdemaDTO>();
            }
        }

        public async Task<EdemaDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Edema/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EdemaDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener edema por ID: {Id}", id);
                return null;
            }
        }
    }
}