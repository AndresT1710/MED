using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ComplementaryExamsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ComplementaryExamsService> _logger;

        public ComplementaryExamsService(HttpClient httpClient, ILogger<ComplementaryExamsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ComplementaryExamsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ComplementaryExams");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ComplementaryExamsDTO>>();
                }
                _logger.LogWarning("Error al obtener exámenes complementarios: {StatusCode}", response.StatusCode);
                return new List<ComplementaryExamsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener exámenes complementarios");
                return new List<ComplementaryExamsDTO>();
            }
        }

        public async Task<ComplementaryExamsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ComplementaryExams/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ComplementaryExamsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener examen complementario por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<ComplementaryExamsDTO>?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allExams = await GetAllAsync();
                return allExams?
                    .Where(ce => ce.MedicalCareId == medicalCareId)
                    .ToList() ?? new List<ComplementaryExamsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener exámenes complementarios por atención médica: {MedicalCareId}", medicalCareId);
                return new List<ComplementaryExamsDTO>();
            }
        }

        public async Task<(bool Success, ComplementaryExamsDTO? Data, string Error)> CreateAsync(ComplementaryExamsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ComplementaryExams", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ComplementaryExamsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear examen complementario");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ComplementaryExamsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ComplementaryExams/{dto.ComplementaryExamsId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar examen complementario");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ComplementaryExams/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar examen complementario");
                return (false, ex.Message);
            }
        }
    }
}