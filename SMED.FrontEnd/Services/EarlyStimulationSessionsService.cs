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
                _logger.LogWarning("Error al obtener sesiones de estimulación temprana: {StatusCode}", response.StatusCode);
                return new List<EarlyStimulationSessionsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones de estimulación temprana");
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
                _logger.LogError(ex, "Error al obtener sesión de estimulación temprana por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<EarlyStimulationSessionsDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allSessions = await GetAllAsync();
                return allSessions?
                    .Where(s => s.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<EarlyStimulationSessionsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones por atención médica: {MedicalCareId}", medicalCareId);
                return new List<EarlyStimulationSessionsDTO>();
            }
        }

        public async Task<(bool Success, EarlyStimulationSessionsDTO? Data, string Error)> CreateAsync(EarlyStimulationSessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/EarlyStimulationSessions", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<EarlyStimulationSessionsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear sesión de estimulación temprana");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(EarlyStimulationSessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/EarlyStimulationSessions/{dto.SessionsId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar sesión de estimulación temprana");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/EarlyStimulationSessions/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar sesión de estimulación temprana");
                return (false, ex.Message);
            }
        }
    }
}