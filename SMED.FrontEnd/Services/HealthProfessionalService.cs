using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class HealthProfessionalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HealthProfessionalService> _logger;

        public HealthProfessionalService(HttpClient httpClient, ILogger<HealthProfessionalService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<HealthProfessionalDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/HealthProfessional");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<HealthProfessionalDTO>>() ?? new List<HealthProfessionalDTO>();
                }
                return new List<HealthProfessionalDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener profesionales de salud");
                return new List<HealthProfessionalDTO>();
            }
        }
    }

}
