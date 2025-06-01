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

        public async Task<List<PersonalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/personalhistory/by-history/{clinicalHistoryId}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error getting personal histories: {response.StatusCode} - {errorContent}");
                    return new List<PersonalHistoryDTO>();
                }

                return await response.Content.ReadFromJsonAsync<List<PersonalHistoryDTO>>() ?? new List<PersonalHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByClinicalHistoryIdAsync");
                return new List<PersonalHistoryDTO>();
            }
        }

        public async Task<(bool Success, PersonalHistoryDTO? Result, string Error)> CreateAsync(PersonalHistoryDTO dto)
        {
            try
            {
                // Validación básica antes de enviar
                if (dto.ClinicalHistoryId <= 0)
                {
                    return (false, null, "ClinicalHistoryId es requerido");
                }

                if (string.IsNullOrWhiteSpace(dto.Description))
                {
                    dto.Description = $"Antecedente personal - {DateTime.Now:yyyy-MM-dd}";
                }

                dto.RegistrationDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/personalhistory", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error creating personal history: {response.StatusCode} - {errorContent}");
                    return (false, null, errorContent);
                }

                var result = await response.Content.ReadFromJsonAsync<PersonalHistoryDTO>();
                return (true, result, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateAsync");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(PersonalHistoryDTO dto)
        {
            try
            {
                if (dto.PersonalHistoryId <= 0)
                {
                    return (false, "ID de antecedente personal inválido");
                }

                var response = await _http.PutAsJsonAsync($"api/personalhistory/{dto.PersonalHistoryId}", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error updating personal history: {response.StatusCode} - {errorContent}");
                    return (false, errorContent);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateAsync");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/personalhistory/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error deleting personal history: {response.StatusCode} - {errorContent}");
                    return (false, errorContent);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteAsync");
                return (false, ex.Message);
            }
        }
    }
}