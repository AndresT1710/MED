using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class JointConditionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JointConditionService> _logger;

        public JointConditionService(HttpClient httpClient, ILogger<JointConditionService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<JointConditionDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/JointCondition");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<JointConditionDTO>>();
                }
                _logger.LogWarning("Error al obtener condiciones articulares: {StatusCode}", response.StatusCode);
                return new List<JointConditionDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener condiciones articulares");
                return new List<JointConditionDTO>();
            }
        }

        public async Task<JointConditionDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/JointCondition/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<JointConditionDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener condición articular por ID: {Id}", id);
                return null;
            }
        }
    }
}