using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalCareService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalCareService> _logger;

        public MedicalCareService(HttpClient httpClient, ILogger<MedicalCareService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalCareDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalCare");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalCareDTO>>();
                }

                _logger.LogWarning("Error al obtener atenciones médicas: {StatusCode}", response.StatusCode);
                return new List<MedicalCareDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las atenciones médicas");
                return new List<MedicalCareDTO>();
            }
        }

        public async Task<MedicalCareDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalCare/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalCareDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener atención médica por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, MedicalCareDTO? Data, string Error)> CreateAsync(MedicalCareDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalCare", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicalCareDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear atención médica");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(MedicalCareDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalCare/{dto.CareId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar atención médica");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalCare/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar atención médica");
                return (false, ex.Message);
            }
        }
    }

}
