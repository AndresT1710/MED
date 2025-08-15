using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class IndicationsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<IndicationsService> _logger;

        public IndicationsService(HttpClient httpClient, ILogger<IndicationsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<IndicationsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Indications");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<IndicationsDTO>>();
                }
                _logger.LogWarning("Error al obtener indicaciones: {StatusCode}", response.StatusCode);
                return new List<IndicationsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las indicaciones");
                return new List<IndicationsDTO>();
            }
        }

        public async Task<IndicationsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Indications/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IndicationsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener indicación por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, IndicationsDTO? Data, string Error)> CreateAsync(IndicationsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Indications", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<IndicationsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear indicación");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, IndicationsDTO? Data, string Error)> UpdateAsync(IndicationsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Indications/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<IndicationsDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar indicación");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Indications/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar indicación");
                return (false, ex.Message);
            }
        }

        public async Task<List<IndicationsDTO>?> GetByTreatmentIdAsync(int treatmentId)
        {
            try
            {
                var allIndications = await GetAllAsync();
                return allIndications?.Where(i => i.TreatmentId == treatmentId).ToList() ?? new List<IndicationsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener indicaciones por TreatmentId: {TreatmentId}", treatmentId);
                return new List<IndicationsDTO>();
            }
        }
    }
}
