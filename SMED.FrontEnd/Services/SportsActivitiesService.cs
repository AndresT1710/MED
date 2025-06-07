using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SportsActivitiesService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SportsActivitiesService> _logger;

        public SportsActivitiesService(HttpClient httpClient, ILogger<SportsActivitiesService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SportsActivitiesDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SportsActivities");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SportsActivitiesDTO>>();
                }
                _logger.LogWarning("Error al obtener actividades deportivas: {StatusCode}", response.StatusCode);
                return new List<SportsActivitiesDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividades deportivas");
                return new List<SportsActivitiesDTO>();
            }
        }

        public async Task<SportsActivitiesDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SportsActivities/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SportsActivitiesDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener actividad deportiva por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, SportsActivitiesDTO? Data, string Error)> CreateAsync(SportsActivitiesDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SportsActivities", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SportsActivitiesDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear actividad deportiva");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SportsActivitiesDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SportsActivities/{dto.SportActivityId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar actividad deportiva");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SportsActivities/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar actividad deportiva");
                return (false, ex.Message);
            }
        }
    }
}