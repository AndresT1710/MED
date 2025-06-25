using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(HttpClient httpClient, ILogger<ServiceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ServiceDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Service");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ServiceDTO>>();
                }

                _logger.LogWarning("Error al obtener servicios: {StatusCode}", response.StatusCode);
                return new List<ServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los servicios");
                return new List<ServiceDTO>();
            }
        }

        public async Task<ServiceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Service/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ServiceDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicio por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, ServiceDTO? Data, string Error)> CreateAsync(ServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Service", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ServiceDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear servicio");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Service/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar servicio");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Service/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar servicio");
                return (false, ex.Message);
            }
        }
    }

}
