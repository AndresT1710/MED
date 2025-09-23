using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class WorkHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WorkHistoryService> _logger;

        public WorkHistoryService(HttpClient httpClient, ILogger<WorkHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<WorkHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/WorkHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<WorkHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales laborales: {StatusCode}", response.StatusCode);
                return new List<WorkHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales laborales");
                return new List<WorkHistoryDTO>();
            }
        }

        public async Task<List<WorkHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/WorkHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<WorkHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales laborales por historia clínica: {StatusCode}", response.StatusCode);
                return new List<WorkHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales laborales por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<WorkHistoryDTO>();
            }
        }

        public async Task<WorkHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/WorkHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<WorkHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial laboral por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, WorkHistoryDTO? Data, string Error)> CreateAsync(WorkHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/WorkHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<WorkHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial laboral");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(WorkHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/WorkHistory/{dto.WorkHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial laboral");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/WorkHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial laboral");
                return (false, ex.Message);
            }
        }
    }
}