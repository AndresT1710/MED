using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class CurrentProblemHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CurrentProblemHistoryService> _logger;

        public CurrentProblemHistoryService(HttpClient httpClient, ILogger<CurrentProblemHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<CurrentProblemHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/CurrentProblemHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CurrentProblemHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de problemas actuales: {StatusCode}", response.StatusCode);
                return new List<CurrentProblemHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de problemas actuales");
                return new List<CurrentProblemHistoryDTO>();
            }
        }

        public async Task<List<CurrentProblemHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CurrentProblemHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CurrentProblemHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de problemas actuales por historia clínica: {StatusCode}", response.StatusCode);
                return new List<CurrentProblemHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de problemas actuales por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<CurrentProblemHistoryDTO>();
            }
        }

        public async Task<CurrentProblemHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CurrentProblemHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CurrentProblemHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de problema actual por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, CurrentProblemHistoryDTO? Data, string Error)> CreateAsync(CurrentProblemHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CurrentProblemHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<CurrentProblemHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial de problema actual");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(CurrentProblemHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/CurrentProblemHistory/{dto.CurrentProblemHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial de problema actual");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/CurrentProblemHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial de problema actual");
                return (false, ex.Message);
            }
        }
    }
}