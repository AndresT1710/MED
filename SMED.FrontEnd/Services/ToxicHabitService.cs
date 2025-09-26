using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ToxicHabitService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ToxicHabitService> _logger;

        public ToxicHabitService(HttpClient httpClient, ILogger<ToxicHabitService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ToxicHabitDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ToxicHabit");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ToxicHabitDTO>>();
                }
                _logger.LogWarning("Error al obtener hábitos tóxicos: {StatusCode}", response.StatusCode);
                return new List<ToxicHabitDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener hábitos tóxicos");
                return new List<ToxicHabitDTO>();
            }
        }

        public async Task<ToxicHabitDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ToxicHabit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ToxicHabitDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener hábito tóxico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, ToxicHabitDTO? Data, string Error)> CreateAsync(ToxicHabitDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ToxicHabit", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ToxicHabitDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear hábito tóxico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ToxicHabitDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ToxicHabit/{dto.ToxicHabitId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar hábito tóxico");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ToxicHabit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar hábito tóxico");
                return (false, ex.Message);
            }
        }
    }
}