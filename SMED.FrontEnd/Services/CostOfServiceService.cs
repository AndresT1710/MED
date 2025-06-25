using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class CostOfServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CostOfServiceService> _logger;

        public CostOfServiceService(HttpClient httpClient, ILogger<CostOfServiceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<CostOfServiceDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/CostOfService");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CostOfServiceDTO>>();
                }

                _logger.LogWarning("Error al obtener costos de servicio: {StatusCode}", response.StatusCode);
                return new List<CostOfServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los costos de servicio");
                return new List<CostOfServiceDTO>();
            }
        }

        public async Task<CostOfServiceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CostOfService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CostOfServiceDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener costo de servicio por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, CostOfServiceDTO? Data, string Error)> CreateAsync(CostOfServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CostOfService", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<CostOfServiceDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear costo de servicio");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(CostOfServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/CostOfService/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar costo de servicio");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/CostOfService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar costo de servicio");
                return (false, ex.Message);
            }
        }
    }

}
