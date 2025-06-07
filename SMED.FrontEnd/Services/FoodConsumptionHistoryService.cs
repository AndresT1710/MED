using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class FoodConsumptionHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FoodConsumptionHistoryService> _logger;

        public FoodConsumptionHistoryService(HttpClient httpClient, ILogger<FoodConsumptionHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<FoodConsumptionHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/FoodConsumptionHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FoodConsumptionHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de consumo de alimentos: {StatusCode}", response.StatusCode);
                return new List<FoodConsumptionHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de consumo de alimentos");
                return new List<FoodConsumptionHistoryDTO>();
            }
        }

        public async Task<List<FoodConsumptionHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/FoodConsumptionHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FoodConsumptionHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de consumo de alimentos por historia clínica: {StatusCode}", response.StatusCode);
                return new List<FoodConsumptionHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de consumo de alimentos por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<FoodConsumptionHistoryDTO>();
            }
        }

        public async Task<FoodConsumptionHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/FoodConsumptionHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FoodConsumptionHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de consumo de alimento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, FoodConsumptionHistoryDTO? Data, string Error)> CreateAsync(FoodConsumptionHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/FoodConsumptionHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<FoodConsumptionHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de consumo de alimento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(FoodConsumptionHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/FoodConsumptionHistory/{dto.FoodConsumptionHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de consumo de alimento");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FoodConsumptionHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de consumo de alimento");
                return (false, ex.Message);
            }
        }
    }
}
