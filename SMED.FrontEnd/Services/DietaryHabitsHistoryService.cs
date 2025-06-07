using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class DietaryHabitsHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DietaryHabitsHistoryService> _logger;

        public DietaryHabitsHistoryService(HttpClient httpClient, ILogger<DietaryHabitsHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<DietaryHabitsHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DietaryHabitsHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DietaryHabitsHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de hábitos dietéticos: {StatusCode}", response.StatusCode);
                return new List<DietaryHabitsHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de hábitos dietéticos");
                return new List<DietaryHabitsHistoryDTO>();
            }
        }

        public async Task<List<DietaryHabitsHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/DietaryHabitsHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DietaryHabitsHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historias de hábitos dietéticos por historia clínica: {StatusCode}", response.StatusCode);
                return new List<DietaryHabitsHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historias de hábitos dietéticos por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<DietaryHabitsHistoryDTO>();
            }
        }

        public async Task<DietaryHabitsHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/DietaryHabitsHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DietaryHabitsHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historia de hábito dietético por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, DietaryHabitsHistoryDTO? Data, string Error)> CreateAsync(DietaryHabitsHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/DietaryHabitsHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<DietaryHabitsHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historia de hábito dietético");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(DietaryHabitsHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/DietaryHabitsHistory/{dto.DietaryHabitHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historia de hábito dietético");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/DietaryHabitsHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historia de hábito dietético");
                return (false, ex.Message);
            }
        }
    }
}
