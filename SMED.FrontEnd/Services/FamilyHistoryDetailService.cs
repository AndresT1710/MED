using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class FamilyHistoryDetailService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FamilyHistoryDetailService> _logger;

        public FamilyHistoryDetailService(HttpClient httpClient, ILogger<FamilyHistoryDetailService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<FamilyHistoryDetailDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/FamilyHistoryDetail");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FamilyHistoryDetailDTO>>();
                }
                _logger.LogWarning("Error al obtener antecedentes familiares: {StatusCode}", response.StatusCode);
                return new List<FamilyHistoryDetailDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener antecedentes familiares");
                return new List<FamilyHistoryDetailDTO>();
            }
        }

        public async Task<List<FamilyHistoryDetailDTO>?> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/FamilyHistoryDetail/by-clinical-history/{clinicalHistoryId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FamilyHistoryDetailDTO>>();
                }
                _logger.LogWarning("Error al obtener antecedentes familiares por historia clínica: {StatusCode}", response.StatusCode);
                return new List<FamilyHistoryDetailDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener antecedentes familiares por historia clínica: {ClinicalHistoryId}", clinicalHistoryId);
                return new List<FamilyHistoryDetailDTO>();
            }
        }

        public async Task<FamilyHistoryDetailDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/FamilyHistoryDetail/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FamilyHistoryDetailDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener antecedente familiar por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, FamilyHistoryDetailDTO? Data, string Error)> CreateAsync(FamilyHistoryDetailDTO dto)
        {
            try
            {
                // Asegurar que RegistrationDate esté establecida
                if (!dto.RegistrationDate.HasValue)
                {
                    dto.RegistrationDate = DateTime.Now;
                }

                var response = await _httpClient.PostAsJsonAsync("api/FamilyHistoryDetail", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<FamilyHistoryDetailDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear antecedente familiar");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(FamilyHistoryDetailDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/FamilyHistoryDetail/{dto.FamilyHistoryDetailId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar antecedente familiar");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FamilyHistoryDetail/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar antecedente familiar");
                return (false, ex.Message);
            }
        }
    }
}