using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class SurgeryHistoryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<SurgeryHistoryService> _logger;

        public SurgeryHistoryService(HttpClient http, ILogger<SurgeryHistoryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // Obtiene todos los antecedentes quirúrgicos asociados a una historia clínica
        public async Task<List<SurgeryHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/surgeryhistory/by-history/{clinicalHistoryId}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error getting surgery histories for ClinicalHistoryId {Id}: {Status} - {Error}", clinicalHistoryId, response.StatusCode, error);
                    return new List<SurgeryHistoryDTO>();
                }
                var list = await response.Content.ReadFromJsonAsync<List<SurgeryHistoryDTO>>();
                return list ?? new List<SurgeryHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetByClinicalHistoryIdAsync for ClinicalHistoryId {Id}", clinicalHistoryId);
                return new List<SurgeryHistoryDTO>();
            }
        }

        // Crear nuevo antecedente quirúrgico
        // En SurgeryHistoryService.cs, método CreateAsync
        public async Task<(bool Success, SurgeryHistoryDTO? Result, string ErrorMessage)> CreateAsync(SurgeryHistoryDTO dto)
        {
            try
            {
                if (dto.ClinicalHistoryId <= 0)
                    return (false, null, "El Id de Historia Clínica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.HistoryNumber))
                    return (false, null, "El número de historia médica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.Description))
                    dto.Description = $"Antecedente quirúrgico - {DateTime.Now:yyyy-MM-dd}";

                dto.RegistrationDate = DateTime.Now;

                // Si no se especifica fecha de cirugía, usar la fecha actual
                if (dto.SurgeryDate == null || dto.SurgeryDate == default(DateTime))
                    dto.SurgeryDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/surgeryhistory", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error creating surgery history: {Status} - {Error}", response.StatusCode, error);
                    return (false, null, error);
                }

                var created = await response.Content.ReadFromJsonAsync<SurgeryHistoryDTO>();
                return (true, created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync");
                return (false, null, ex.Message);
            }
        }

        // Actualizar antecedente quirúrgico existente
        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(SurgeryHistoryDTO dto)
        {
            try
            {
                if (dto.SurgeryHistoryId <= 0)
                    return (false, "El Id de antecedente quirúrgico es inválido.");

                var response = await _http.PutAsJsonAsync($"api/surgeryhistory/{dto.SurgeryHistoryId}", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error updating surgery history Id {Id}: {Status} - {Error}", dto.SurgeryHistoryId, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync for SurgeryHistoryId {Id}", dto.SurgeryHistoryId);
                return (false, ex.Message);
            }
        }

        // Eliminar antecedente quirúrgico
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/surgeryhistory/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error deleting surgery history Id {Id}: {Status} - {Error}", id, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync for SurgeryHistoryId {Id}", id);
                return (false, ex.Message);
            }
        }
    }
}