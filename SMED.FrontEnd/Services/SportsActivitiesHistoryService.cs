using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SportsActivitiesHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SportsActivitiesHistoryService> _logger;

        public SportsActivitiesHistoryService(HttpClient httpClient, ILogger<SportsActivitiesHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SportsActivitiesHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SportsActivitiesHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SportsActivitiesHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de actividades deportivas: {StatusCode}", response.StatusCode);
                return new List<SportsActivitiesHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de actividades deportivas");
                return new List<SportsActivitiesHistoryDTO>();
            }
        }

        public async Task<List<SportsActivitiesHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SportsActivitiesHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SportsActivitiesHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de actividades deportivas por historia clínica: {StatusCode}", response.StatusCode);
                return new List<SportsActivitiesHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de actividades deportivas por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<SportsActivitiesHistoryDTO>();
            }
        }

        public async Task<SportsActivitiesHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SportsActivitiesHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SportsActivitiesHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de actividad deportiva por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, SportsActivitiesHistoryDTO? Data, string Error)> CreateAsync(SportsActivitiesHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SportsActivitiesHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SportsActivitiesHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de actividad deportiva");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SportsActivitiesHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SportsActivitiesHistory/{dto.SportActivityHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de actividad deportiva");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SportsActivitiesHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de actividad deportiva");
                return (false, ex.Message);
            }
        }
    }
}