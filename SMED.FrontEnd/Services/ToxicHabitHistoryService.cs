using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ToxicHabitHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ToxicHabitHistoryService> _logger;

        public ToxicHabitHistoryService(HttpClient httpClient, ILogger<ToxicHabitHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ToxicHabitHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ToxicHabitHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ToxicHabitHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de hábitos tóxicos: {StatusCode}", response.StatusCode);
                return new List<ToxicHabitHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de hábitos tóxicos");
                return new List<ToxicHabitHistoryDTO>();
            }
        }

        public async Task<List<ToxicHabitHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ToxicHabitHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ToxicHabitHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de hábitos tóxicos por historia clínica: {StatusCode}", response.StatusCode);
                return new List<ToxicHabitHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de hábitos tóxicos por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<ToxicHabitHistoryDTO>();
            }
        }

        public async Task<ToxicHabitHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ToxicHabitHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ToxicHabitHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de hábito tóxico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, ToxicHabitHistoryDTO? Data, string Error)> CreateAsync(ToxicHabitHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ToxicHabitHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ToxicHabitHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial de hábito tóxico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ToxicHabitHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ToxicHabitHistory/{dto.ToxicHabitHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial de hábito tóxico");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ToxicHabitHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial de hábito tóxico");
                return (false, ex.Message);
            }
        }
    }
}