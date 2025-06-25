using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class TypeOfProceduresService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TypeOfProceduresService> _logger;

        public TypeOfProceduresService(HttpClient httpClient, ILogger<TypeOfProceduresService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<TypeOfProceduresDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/TypeOfProcedures");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<TypeOfProceduresDTO>>();
                }

                _logger.LogWarning("Error al obtener tipos de procedimientos: {StatusCode}", response.StatusCode);
                return new List<TypeOfProceduresDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tipos de procedimientos");
                return new List<TypeOfProceduresDTO>();
            }
        }

        public async Task<TypeOfProceduresDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/TypeOfProcedures/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TypeOfProceduresDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipo de procedimiento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, TypeOfProceduresDTO? Data, string Error)> CreateAsync(TypeOfProceduresDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TypeOfProcedures", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<TypeOfProceduresDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tipo de procedimiento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(TypeOfProceduresDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TypeOfProcedures/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tipo de procedimiento");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/TypeOfProcedures/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tipo de procedimiento");
                return (false, ex.Message);
            }
        }
    }

}
