using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class FoodService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FoodService> _logger;

        public FoodService(HttpClient httpClient, ILogger<FoodService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<FoodDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Food");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FoodDTO>>();
                }
                _logger.LogWarning("Error al obtener alimentos: {StatusCode}", response.StatusCode);
                return new List<FoodDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener alimentos");
                return new List<FoodDTO>();
            }
        }

        public async Task<FoodDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Food/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FoodDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener alimento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, FoodDTO? Data, string Error)> CreateAsync(FoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Food", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<FoodDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear alimento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(FoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Food/{dto.FoodId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar alimento");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Food/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar alimento");
                return (false, ex.Message);
            }
        }
    }
}
