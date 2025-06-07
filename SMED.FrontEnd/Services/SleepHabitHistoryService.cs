using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SleepHabitHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SleepHabitHistoryService> _logger;

        public SleepHabitHistoryService(HttpClient httpClient, ILogger<SleepHabitHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SleepHabitHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SleepHabitHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SleepHabitHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de hábitos de sueño: {StatusCode}", response.StatusCode);
                return new List<SleepHabitHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de hábitos de sueño");
                return new List<SleepHabitHistoryDTO>();
            }
        }

        public async Task<List<SleepHabitHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SleepHabitHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SleepHabitHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de hábitos de sueño por historia clínica: {StatusCode}", response.StatusCode);
                return new List<SleepHabitHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de hábitos de sueño por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<SleepHabitHistoryDTO>();
            }
        }

        public async Task<SleepHabitHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SleepHabitHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SleepHabitHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de hábito de sueño por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, SleepHabitHistoryDTO? Data, string Error)> CreateAsync(SleepHabitHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SleepHabitHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SleepHabitHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de hábito de sueño");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SleepHabitHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SleepHabitHistory/{dto.HabitSleepHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de hábito de sueño");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SleepHabitHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de hábito de sueño");
                return (false, ex.Message);
            }
        }
    }
}
