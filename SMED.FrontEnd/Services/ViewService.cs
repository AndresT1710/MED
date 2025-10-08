using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ViewService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ViewService> _logger;

        public ViewService(HttpClient httpClient, ILogger<ViewService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ViewDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/View");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ViewDTO>>();
                }
                _logger.LogWarning("Error al obtener vistas: {StatusCode}", response.StatusCode);
                return new List<ViewDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vistas");
                return new List<ViewDTO>();
            }
        }

        public async Task<ViewDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/View/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ViewDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vista por ID: {Id}", id);
                return null;
            }
        }
    }
}