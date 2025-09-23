using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicationHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicationHistoryService> _logger;

        public MedicationHistoryService(HttpClient httpClient, ILogger<MedicationHistoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicationHistoryDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicationHistory");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicationHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de medicación: {StatusCode}", response.StatusCode);
                return new List<MedicationHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de medicación");
                return new List<MedicationHistoryDTO>();
            }
        }

        public async Task<List<MedicationHistoryDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicationHistory/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicationHistoryDTO>>();
                }
                _logger.LogWarning("Error al obtener historiales de medicación por historia clínica: {StatusCode}", response.StatusCode);
                return new List<MedicationHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales de medicación por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<MedicationHistoryDTO>();
            }
        }

        public async Task<MedicationHistoryDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicationHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicationHistoryDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de medicación por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, MedicationHistoryDTO? Data, string Error)> CreateAsync(MedicationHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicationHistory", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicationHistoryDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear historial de medicación");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(MedicationHistoryDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicationHistory/{dto.MedicationHistoryId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial de medicación");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicationHistory/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial de medicación");
                return (false, ex.Message);
            }
        }
    }
}