using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class VitalSignsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VitalSignsService> _logger;

        public VitalSignsService(HttpClient httpClient, ILogger<VitalSignsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<VitalSignsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/VitalSigns");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<VitalSignsDTO>>();
                }

                _logger.LogWarning("Error al obtener signos vitales: {StatusCode}", response.StatusCode);
                return new List<VitalSignsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los signos vitales");
                return new List<VitalSignsDTO>();
            }
        }

        public async Task<VitalSignsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/VitalSigns/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<VitalSignsDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener signo vital por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, VitalSignsDTO? Data, string Error)> CreateAsync(VitalSignsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/VitalSigns", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<VitalSignsDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear signo vital");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(VitalSignsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/VitalSigns/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar signo vital");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/VitalSigns/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar signo vital");
                return (false, ex.Message);
            }
        }
    }

}
