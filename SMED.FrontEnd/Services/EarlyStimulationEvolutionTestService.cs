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
                _logger.LogError(ex, "Error al obtener test de evolución por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<EarlyStimulationEvolutionTestDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allTests = await GetAllAsync();
                return allTests?
                    .Where(t => t.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<EarlyStimulationEvolutionTestDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tests por atención médica: {MedicalCareId}", medicalCareId);
                return new List<EarlyStimulationEvolutionTestDTO>();
            }
        }

        public async Task<(bool Success, EarlyStimulationEvolutionTestDTO? Data, string Error)> CreateAsync(EarlyStimulationEvolutionTestDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/EarlyStimulationEvolutionTest", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<EarlyStimulationEvolutionTestDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear test de evolución");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(EarlyStimulationEvolutionTestDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/EarlyStimulationEvolutionTest/{dto.TestId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar test de evolución");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/EarlyStimulationEvolutionTest/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar test de evolución");
                return (false, ex.Message);
            }
        }
    }
}