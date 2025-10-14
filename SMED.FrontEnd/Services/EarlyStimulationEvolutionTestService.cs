using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class EarlyStimulationEvolutionTestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EarlyStimulationEvolutionTestService> _logger;

        public EarlyStimulationEvolutionTestService(HttpClient httpClient, ILogger<EarlyStimulationEvolutionTestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<EarlyStimulationEvolutionTestDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/EarlyStimulationEvolutionTest");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<EarlyStimulationEvolutionTestDTO>>();
                }
                _logger.LogWarning("Error al obtener tests de evolución: {StatusCode}", response.StatusCode);
                return new List<EarlyStimulationEvolutionTestDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tests de evolución");
                return new List<EarlyStimulationEvolutionTestDTO>();
            }
        }

        public async Task<EarlyStimulationEvolutionTestDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/EarlyStimulationEvolutionTest/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EarlyStimulationEvolutionTestDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener test por ID: {Id}", id);
                return null;
            }
        }
    }
}
