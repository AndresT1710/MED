using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SpecialTestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SpecialTestService> _logger;

        public SpecialTestService(HttpClient httpClient, ILogger<SpecialTestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SpecialTestDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SpecialTest");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SpecialTestDTO>>();
                }
                _logger.LogWarning("Error al obtener pruebas especiales: {StatusCode}", response.StatusCode);
                return new List<SpecialTestDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pruebas especiales");
                return new List<SpecialTestDTO>();
            }
        }

        public async Task<SpecialTestDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SpecialTest/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SpecialTestDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener prueba especial por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<SpecialTestDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allTests = await GetAllAsync();
                return allTests?
                    .Where(st => st.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<SpecialTestDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pruebas especiales por atención médica: {MedicalCareId}", medicalCareId);
                return new List<SpecialTestDTO>();
            }
        }

        public async Task<(bool Success, SpecialTestDTO? Data, string Error)> CreateAsync(SpecialTestDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SpecialTest", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SpecialTestDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear prueba especial");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SpecialTestDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SpecialTest/{dto.SpecialTestId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar prueba especial");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SpecialTest/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar prueba especial");
                return (false, ex.Message);
            }
        }
    }
}