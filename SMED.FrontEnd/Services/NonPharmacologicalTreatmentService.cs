using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class NonPharmacologicalTreatmentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NonPharmacologicalTreatmentService> _logger;

        public NonPharmacologicalTreatmentService(HttpClient httpClient, ILogger<NonPharmacologicalTreatmentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NonPharmacologicalTreatmentDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/NonPharmacologicalTreatment");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<NonPharmacologicalTreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos no farmacológicos: {StatusCode}", response.StatusCode);
                return new List<NonPharmacologicalTreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los tratamientos no farmacológicos");
                return new List<NonPharmacologicalTreatmentDTO>();
            }
        }

        public async Task<NonPharmacologicalTreatmentDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/NonPharmacologicalTreatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<NonPharmacologicalTreatmentDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamiento no farmacológico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<NonPharmacologicalTreatmentDTO>?> GetByMedicalDiagnosisIdAsync(int medicalDiagnosisId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/NonPharmacologicalTreatment/by-medical-diagnosis/{medicalDiagnosisId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<NonPharmacologicalTreatmentDTO>>();
                }
                _logger.LogWarning("Error al obtener tratamientos no farmacológicos por diagnóstico: {StatusCode}", response.StatusCode);
                return new List<NonPharmacologicalTreatmentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tratamientos no farmacológicos por MedicalDiagnosisId: {MedicalDiagnosisId}", medicalDiagnosisId);
                return new List<NonPharmacologicalTreatmentDTO>();
            }
        }

        public async Task<(bool Success, NonPharmacologicalTreatmentDTO? Data, string Error)> CreateAsync(NonPharmacologicalTreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/NonPharmacologicalTreatment", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<NonPharmacologicalTreatmentDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear tratamiento no farmacológico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, NonPharmacologicalTreatmentDTO? Data, string Error)> UpdateAsync(NonPharmacologicalTreatmentDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/NonPharmacologicalTreatment/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<NonPharmacologicalTreatmentDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar tratamiento no farmacológico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/NonPharmacologicalTreatment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tratamiento no farmacológico");
                return (false, ex.Message);
            }
        }
    }
}
