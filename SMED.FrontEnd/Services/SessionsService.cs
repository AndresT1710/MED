using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SessionsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SessionsService> _logger;

        public SessionsService(HttpClient httpClient, ILogger<SessionsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SessionsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Sessions");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SessionsDTO>>();
                }
                _logger.LogWarning("Error al obtener sesiones: {StatusCode}", response.StatusCode);
                return new List<SessionsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones");
                return new List<SessionsDTO>();
            }
        }

        public async Task<SessionsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Sessions/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SessionsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesión por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<SessionsDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allSessions = await GetAllAsync();
                return allSessions?
                    .Where(s => s.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<SessionsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sesiones por atención médica: {MedicalCareId}", medicalCareId);
                return new List<SessionsDTO>();
            }
        }

        public async Task<(bool Success, SessionsDTO? Data, string Error)> CreateAsync(SessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Sessions", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SessionsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear sesión");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Sessions/{dto.SessionsId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar sesión");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Sessions/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar sesión");
                return (false, ex.Message);
            }
        }
    }
}