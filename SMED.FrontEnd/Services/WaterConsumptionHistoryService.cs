using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class WaterConsumptionHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WaterConsumptionHistoryService> _logger;

        public WaterConsumptionHistoryService(HttpClient httpClient, ILogger<WaterConsumptionHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<WaterConsumptionHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/WaterConsumptionHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<WaterConsumptionHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de consumo de agua: {StatusCode}", response.StatusCode);
                return new List<WaterConsumptionHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de consumo de agua");
                return new List<WaterConsumptionHistoryDTO>();
            }
        }

        public async Task<List<WaterConsumptionHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/WaterConsumptionHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<WaterConsumptionHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de consumo de agua por historia clínica: {StatusCode}", response.StatusCode);
                return new List<WaterConsumptionHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de consumo de agua por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<WaterConsumptionHistoryDTO>();
            }
        }

        public async Task<WaterConsumptionHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/WaterConsumptionHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<WaterConsumptionHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de consumo de agua por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, WaterConsumptionHistoryDTO? Data, string Error)> CreateAsync(WaterConsumptionHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/WaterConsumptionHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<WaterConsumptionHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de consumo de agua");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(WaterConsumptionHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/WaterConsumptionHistory/{dto.WaterConsumptionHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de consumo de agua");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/WaterConsumptionHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de consumo de agua");
                return (false, ex.Message);
            }
        }
    }
}
