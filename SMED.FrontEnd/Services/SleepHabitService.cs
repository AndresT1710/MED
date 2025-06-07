using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SleepHabitService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SleepHabitService> _logger;

        public SleepHabitService(HttpClient httpClient, ILogger<SleepHabitService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SleepHabitDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SleepHabit");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SleepHabitDTO>>();
                }
                _logger.LogWarning("Error al obtener hábitos de sueño: {StatusCode}", response.StatusCode);
                return new List<SleepHabitDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener hábitos de sueño");
                return new List<SleepHabitDTO>();
            }
        }

        public async Task<SleepHabitDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SleepHabit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SleepHabitDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener hábito de sueño por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, SleepHabitDTO? Data, string Error)> CreateAsync(SleepHabitDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SleepHabit", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SleepHabitDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear hábito de sueño");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SleepHabitDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SleepHabit/{dto.SleepHabitId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar hábito de sueño");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SleepHabit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar hábito de sueño");
                return (false, ex.Message);
            }
        }
    }
}
