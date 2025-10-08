using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class JointRangeOfMotionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JointRangeOfMotionService> _logger;

        public JointRangeOfMotionService(HttpClient httpClient, ILogger<JointRangeOfMotionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<JointRangeOfMotionDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/JointRangeOfMotion");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<JointRangeOfMotionDTO>>();
                }
                _logger.LogWarning("Error al obtener rangos de movimiento articular: {StatusCode}", response.StatusCode);
                return new List<JointRangeOfMotionDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener rangos de movimiento articular");
                return new List<JointRangeOfMotionDTO>();
            }
        }

        public async Task<JointRangeOfMotionDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/JointRangeOfMotion/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<JointRangeOfMotionDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener rango de movimiento articular por ID: {Id}", id);
                return null;
            }
        }
    }
}