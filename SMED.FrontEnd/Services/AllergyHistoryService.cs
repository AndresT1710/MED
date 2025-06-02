using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class AllergyHistoryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<AllergyHistoryService> _logger;

        public AllergyHistoryService(HttpClient http, ILogger<AllergyHistoryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // Obtiene todos los antecedentes alérgicos asociados a una historia clínica
        public async Task<List<AllergyHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/allergyhistory/by-history/{clinicalHistoryId}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error getting allergy histories for ClinicalHistoryId {Id}: {Status} - {Error}", clinicalHistoryId, response.StatusCode, error);
                    return new List<AllergyHistoryDTO>();
                }
                var list = await response.Content.ReadFromJsonAsync<List<AllergyHistoryDTO>>();
                return list ?? new List<AllergyHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetByClinicalHistoryIdAsync for ClinicalHistoryId {Id}", clinicalHistoryId);
                return new List<AllergyHistoryDTO>();
            }
        }

        // Crear nuevo antecedente alérgico
        public async Task<(bool Success, AllergyHistoryDTO? Result, string ErrorMessage)> CreateAsync(AllergyHistoryDTO dto)
        {
            try
            {
                if (dto.ClinicalHistoryId <= 0)
                    return (false, null, "El Id de Historia Clínica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.HistoryNumber))
                    return (false, null, "El número de historia médica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.Description))
                    dto.Description = $"Antecedente alérgico - {DateTime.Now:yyyy-MM-dd}";

                dto.RegistrationDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/allergyhistory", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error creating allergy history: {Status} - {Error}", response.StatusCode, error);
                    return (false, null, error);
                }

                var created = await response.Content.ReadFromJsonAsync<AllergyHistoryDTO>();
                return (true, created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync");
                return (false, null, ex.Message);
            }
        }

        // Actualizar antecedente alérgico existente
        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(AllergyHistoryDTO dto)
        {
            try
            {
                if (dto.AllergyHistoryId <= 0)
                    return (false, "El Id de antecedente alérgico es inválido.");

                var response = await _http.PutAsJsonAsync($"api/allergyhistory/{dto.AllergyHistoryId}", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error updating allergy history Id {Id}: {Status} - {Error}", dto.AllergyHistoryId, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync for AllergyHistoryId {Id}", dto.AllergyHistoryId);
                return (false, ex.Message);
            }
        }

        // Eliminar antecedente alérgico
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/allergyhistory/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error deleting allergy history Id {Id}: {Status} - {Error}", id, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync for AllergyHistoryId {Id}", id);
                return (false, ex.Message);
            }
        }
    }
}