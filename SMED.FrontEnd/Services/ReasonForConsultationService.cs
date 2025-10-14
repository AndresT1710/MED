using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ReasonForConsultationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReasonForConsultationService> _logger;

        public ReasonForConsultationService(HttpClient httpClient, ILogger<ReasonForConsultationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ReasonForConsultationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ReasonForConsultation");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ReasonForConsultationDTO>>();
                }
                _logger.LogWarning("Error al obtener motivos de consulta: {StatusCode}", response.StatusCode);
                return new List<ReasonForConsultationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los motivos de consulta");
                return new List<ReasonForConsultationDTO>();
            }
        }

        public async Task<ReasonForConsultationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ReasonForConsultation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ReasonForConsultationDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener motivo de consulta por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<ReasonForConsultationDTO>> GetByCareIdAsync(int medicalCareId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ReasonForConsultation/ByCare/{medicalCareId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ReasonForConsultationDTO>>()
                        ?? new List<ReasonForConsultationDTO>();
                }

                _logger.LogWarning("Error al obtener motivos de consulta por CareId: {StatusCode}", response.StatusCode);
                return new List<ReasonForConsultationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener motivos de consulta por CareId: {MedicalCareId}", medicalCareId);
                return new List<ReasonForConsultationDTO>();
            }
        }

        public async Task<ReasonForConsultationDTO?> GetFirstByCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ReasonForConsultationDTO>($"api/ReasonForConsultation/ByCare/{medicalCareId}/First");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el primer motivo de consulta por CareId: {MedicalCareId}", medicalCareId);
                return null;
            }
        }

        public async Task<(bool Success, ReasonForConsultationDTO? Data, string Error)> CreateAsync(ReasonForConsultationDTO dto)
        {
            try
            {
                // Validaciones básicas
                if (dto == null)
                    return (false, null, "El DTO no puede ser nulo");

                if (string.IsNullOrWhiteSpace(dto.Description))
                    return (false, null, "La descripción es requerida");

                if (dto.MedicalCareId <= 0)
                    return (false, null, "MedicalCareId no válido");

                var response = await _httpClient.PostAsJsonAsync("api/ReasonForConsultation", dto);

                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ReasonForConsultationDTO>();
                    return (true, created, string.Empty);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return (false, null, $"Error de validación: {error}");
                }

                return (false, null, $"Error del servidor: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear motivo de consulta");
                return (false, null, $"Error de conexión: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ReasonForConsultationDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ReasonForConsultation/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar motivo de consulta");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ReasonForConsultation/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar motivo de consulta");
                return (false, ex.Message);
            }
        }
    }

}
