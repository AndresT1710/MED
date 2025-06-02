using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class HabitHistoryService
    {
        private readonly HttpClient _http;
        private readonly ILogger<HabitHistoryService> _logger;

        public HabitHistoryService(HttpClient http, ILogger<HabitHistoryService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // Obtiene todos los antecedentes de hábitos asociados a una historia clínica
        public async Task<List<HabitHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            try
            {
                var response = await _http.GetAsync($"api/habithistory/by-history/{clinicalHistoryId}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error getting habit histories for ClinicalHistoryId {Id}: {Status} - {Error}", clinicalHistoryId, response.StatusCode, error);
                    return new List<HabitHistoryDTO>();
                }
                var list = await response.Content.ReadFromJsonAsync<List<HabitHistoryDTO>>();
                return list ?? new List<HabitHistoryDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetByClinicalHistoryIdAsync for ClinicalHistoryId {Id}", clinicalHistoryId);
                return new List<HabitHistoryDTO>();
            }
        }

        // Crear nuevo antecedente de hábito
        public async Task<(bool Success, HabitHistoryDTO? Result, string ErrorMessage)> CreateAsync(HabitHistoryDTO dto)
        {
            try
            {
                if (dto.ClinicalHistoryId <= 0)
                    return (false, null, "El Id de Historia Clínica es requerido.");

                if (string.IsNullOrWhiteSpace(dto.HistoryNumber))
                    return (false, null, "El número de historia médica es requerido.");

                dto.RecordDate = DateTime.Now;

                var response = await _http.PostAsJsonAsync("api/habithistory", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error creating habit history: {Status} - {Error}", response.StatusCode, error);
                    return (false, null, error);
                }

                var created = await response.Content.ReadFromJsonAsync<HabitHistoryDTO>();
                return (true, created, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync");
                return (false, null, ex.Message);
            }
        }

        // Actualizar antecedente de hábito existente
        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(HabitHistoryDTO dto)
        {
            try
            {
                if (dto.HabitHistoryId <= 0)
                    return (false, "El Id de antecedente de hábito es inválido.");

                var response = await _http.PutAsJsonAsync($"api/habithistory/{dto.HabitHistoryId}", dto);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error updating habit history Id {Id}: {Status} - {Error}", dto.HabitHistoryId, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync for HabitHistoryId {Id}", dto.HabitHistoryId);
                return (false, ex.Message);
            }
        }

        // Eliminar antecedente de hábito
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"api/habithistory/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error deleting habit history Id {Id}: {Status} - {Error}", id, response.StatusCode, error);
                    return (false, error);
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync for HabitHistoryId {Id}", id);
                return (false, ex.Message);
            }
        }
    }
}