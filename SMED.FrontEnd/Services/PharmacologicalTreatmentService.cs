using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PharmacologicalTreatmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PharmacologicalTreatmentService> _logger;

        public PharmacologicalTreatmentService(HttpClient httpClient, ILogger<PharmacologicalTreatmentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PharmacologicalTreatmentDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PharmacologicalTreatment");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PharmacologicalTreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos farmacológicos: {StatusCode}", response.StatusCode);
                return new List<PharmacologicalTreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tratamientos farmacológicos");
                return new List<PharmacologicalTreatmentDTO>();
            }
        }

        public async Task<PharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PharmacologicalTreatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PharmacologicalTreatmentDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamiento farmacológico por ID: {Id}", id);
                return null;
            }
        }

        // ✅ Método corregido para obtener por diagnóstico
        public async Task<List<PharmacologicalTreatmentDTO>?> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PharmacologicalTreatment/by-medical-diagnosis/{medicalDiagnosisId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PharmacologicalTreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos farmacológicos por diagnóstico: {StatusCode}", response.StatusCode);
                return new List<PharmacologicalTreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamientos farmacológicos por MedicalDiagnosisId: {MedicalDiagnosisId}", medicalDiagnosisId);
                return new List<PharmacologicalTreatmentDTO>();
            }
        }

        public async Task<(bool Success, PharmacologicalTreatmentDTO? Data, string Error)> CreateAsync(PharmacologicalTreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PharmacologicalTreatment", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<PharmacologicalTreatmentDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tratamiento farmacológico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, PharmacologicalTreatmentDTO? Data, string Error)> UpdateAsync(PharmacologicalTreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PharmacologicalTreatment/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<PharmacologicalTreatmentDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tratamiento farmacológico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PharmacologicalTreatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tratamiento farmacológico");
                return (false, ex.Message);
            }
        }
    }
}
