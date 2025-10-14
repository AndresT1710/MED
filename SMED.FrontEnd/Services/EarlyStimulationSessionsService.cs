using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class EarlyStimulationSessionsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EarlyStimulationSessionsService> _logger;

        public EarlyStimulationSessionsService(HttpClient httpClient, ILogger<EarlyStimulationSessionsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<EarlyStimulationSessionsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/EarlyStimulationSessions");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<EarlyStimulationSessionsDTO>>();
                }
                _logger.LogWarning("Error al obtener sesiones: {StatusCode}", response.StatusCode);
                return new List<EarlyStimulationSessionsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones");
                return new List<EarlyStimulationSessionsDTO>();
            }
        }

        public async Task<EarlyStimulationSessionsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/EarlyStimulationSessions/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EarlyStimulationSessionsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesión por ID: {Id}", id);
                return null;
            }
        }
    }
}
