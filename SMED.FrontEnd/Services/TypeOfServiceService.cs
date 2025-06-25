using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class TypeOfServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TypeOfServiceService> _logger;

        public TypeOfServiceService(HttpClient httpClient, ILogger<TypeOfServiceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TypeOfServiceDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/TypeOfService");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TypeOfServiceDTO>>();
                }

                _logger.LogWarning("Error al obtener tipos de servicio: {StatusCode}", response.StatusCode);
                return new List<TypeOfServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tipos de servicio");
                return new List<TypeOfServiceDTO>();
            }
        }

        public async Task<TypeOfServiceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/TypeOfService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TypeOfServiceDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipo de servicio por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, TypeOfServiceDTO? Data, string Error)> CreateAsync(TypeOfServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TypeOfService", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<TypeOfServiceDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tipo de servicio");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(TypeOfServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TypeOfService/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tipo de servicio");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/TypeOfService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tipo de servicio");
                return (false, ex.Message);
            }
        }
    }

}
