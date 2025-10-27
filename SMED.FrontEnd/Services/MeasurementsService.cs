using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class MeasurementsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MeasurementsService> _logger;

        public MeasurementsService(HttpClient httpClient, ILogger<MeasurementsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MeasurementsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MeasurementsDTO>>("api/Measurements")
                    ?? new List<MeasurementsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las mediciones");
                return new List<MeasurementsDTO>();
            }
        }

        public async Task<MeasurementsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MeasurementsDTO>($"api/Measurements/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener medición por ID: {Id}", id);
                return null;
            }
        }

        // MANTENEMOS ESTE MÉTODO QUE YA TENÍAS
        public async Task<MeasurementsDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MeasurementsDTO>($"api/Measurements/ByMedicalCare/{medicalCareId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener medición por MedicalCareId: {MedicalCareId}", medicalCareId);
                return null;
            }
        }

        // AGREGAMOS ESTE MÉTODO NUEVO PARA EL PATRÓN DE RETORNO CONSISTENTE
        public async Task<(bool Success, MeasurementsDTO? Data, string Error)> CreateAsync(MeasurementsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Measurements", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MeasurementsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear medición");
                return (false, null, ex.Message);
            }
        }

        // MANTENEMOS TU MÉTODO ORIGINAL TAMBIÉN (PARA BACKWARD COMPATIBILITY)
        public async Task<MeasurementsDTO?> CreateMeasurementAsync(MeasurementsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Measurements", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MeasurementsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear medición (método legacy)");
                return null;
            }
        }

        public async Task<MeasurementsDTO?> UpdateAsync(MeasurementsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Measurements/{dto.MeasurementsId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MeasurementsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar medición: {MeasurementsId}", dto.MeasurementsId);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Measurements/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar medición: {Id}", id);
                return false;
            }
        }
    }
}