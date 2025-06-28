using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalServiceService> _logger;

        public MedicalServiceService(HttpClient httpClient, ILogger<MedicalServiceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalServiceDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalService");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalServiceDTO>>();
                }

                _logger.LogWarning("Error al obtener servicios médicos: {StatusCode}", response.StatusCode);
                return new List<MedicalServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los servicios médicos");
                return new List<MedicalServiceDTO>();
            }
        }

        public async Task<MedicalServiceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalServiceDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicio médico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<MedicalServiceDTO>?> GetByPatientIdAsync(int patientId)
        {
            try
            {
                var allServices = await GetAllAsync();
                return allServices?.Where(s => s.PatientId == patientId).ToList() ?? new List<MedicalServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicios médicos por PatientId: {PatientId}", patientId);
                return new List<MedicalServiceDTO>();
            }
        }

        public async Task<List<MedicalServiceDTO>?> GetByCareIdAsync(int careId)
        {
            try
            {
                var allServices = await GetAllAsync();
                return allServices?.Where(s => s.CareId == careId).ToList() ?? new List<MedicalServiceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicios médicos por CareId: {CareId}", careId);
                return new List<MedicalServiceDTO>();
            }
        }

        public async Task<(bool Success, MedicalServiceDTO? Data, string Error)> CreateAsync(MedicalServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalService", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicalServiceDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear servicio médico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(MedicalServiceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalService/{dto.ServiceId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar servicio médico");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalService/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar servicio médico");
                return (false, ex.Message);
            }
        }
    }

}
