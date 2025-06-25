using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ProceduresService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProceduresService> _logger;

        public ProceduresService(HttpClient httpClient, ILogger<ProceduresService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ProceduresDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Procedures");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ProceduresDTO>>();
                }

                _logger.LogWarning("Error al obtener procedimientos: {StatusCode}", response.StatusCode);
                return new List<ProceduresDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los procedimientos");
                return new List<ProceduresDTO>();
            }
        }

        public async Task<ProceduresDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Procedures/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProceduresDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener procedimiento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, ProceduresDTO? Data, string Error)> CreateAsync(ProceduresDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Procedures", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ProceduresDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear procedimiento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ProceduresDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Procedures/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar procedimiento");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Procedures/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar procedimiento");
                return (false, ex.Message);
            }
        }
    }

}
