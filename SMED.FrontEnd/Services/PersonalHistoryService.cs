using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PersonalHistoryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<PersonalHistoryService> _logger;

        public PersonalHistoryService(HttpClient http, ILogger<PersonalHistoryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // Obtiene todos los antecedentes personales asociados a una historia clínica
        public async Task<List<PersonalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/personalhistory/by-history/{clinicalHistoryId}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error getting personal histories for ClinicalHistoryId {Id}: {Status} - {Error}", clinicalHistoryId, response.StatusCode, error);
                    return new List<PersonalHistoryDTO>();
                }
                var list = await response.Content.ReadFromJsonAsync<List<PersonalHistoryDTO>>();
                return list ?? new List<PersonalHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetByClinicalHistoryIdAsync for ClinicalHistoryId {Id}", clinicalHistoryId);
                return new List<PersonalHistoryDTO>();
            }
        }

        // Crear nuevo antecedente personal
        public async Task<(bool Success, PersonalHistoryDTO? Result, string ErrorMessage)> CreateAsync(PersonalHistoryDTO dto)
        {
            try
            {
                if (dto.ClinicalHistoryId <= 0)
                    return (false, null, "El Id de Historia Clínica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.MedicalRecordNumber))
                    return (false, null, "El número de expediente médico es requerido.");

                if (string.IsNullOrWhiteSpace(dto.Description))
                    dto.Description = $"Antecedente personal - {DateTime.Now:yyyy-MM-dd}";

                dto.RegistrationDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/personalhistory", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error creating personal history: {Status} - {Error}", response.StatusCode, error);
                    return (false, null, error);
                }

                var created = await response.Content.ReadFromJsonAsync<PersonalHistoryDTO>();
                return (true, created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync");
                return (false, null, ex.Message);
            }
        }

        // Actualizar antecedente personal existente
        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(PersonalHistoryDTO dto)
        {
            try
            {
                if (dto.PersonalHistoryId <= 0)
                    return (false, "El Id de antecedente personal es inválido.");

                var response = await _http.PutAsJsonAsync($"api/personalhistory/{dto.PersonalHistoryId}", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error updating personal history Id {Id}: {Status} - {Error}", dto.PersonalHistoryId, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync for PersonalHistoryId {Id}", dto.PersonalHistoryId);
                return (false, ex.Message);
            }
        }

        // Eliminar antecedente personal
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/personalhistory/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error deleting personal history Id {Id}: {Status} - {Error}", id, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync for PersonalHistoryId {Id}", id);
                return (false, ex.Message);
            }
        }
    }
}
