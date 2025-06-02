using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class RelationshipService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RelationshipService> _logger;

        public RelationshipService(HttpClient httpClient, ILogger<RelationshipService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<RelationshipDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Relationship");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<RelationshipDTO>>();
                }
                _logger.LogWarning("Error al obtener parentescos: {StatusCode}", response.StatusCode);
                return new List<RelationshipDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener parentescos");
                return new List<RelationshipDTO>();
            }
        }

        public async Task<RelationshipDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Relationship/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RelationshipDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener parentesco por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, RelationshipDTO? Data, string Error)> CreateAsync(RelationshipDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Relationship", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<RelationshipDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear parentesco");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(RelationshipDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Relationship/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar parentesco");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Relationship/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar parentesco");
                return (false, ex.Message);
            }
        }
    }
}