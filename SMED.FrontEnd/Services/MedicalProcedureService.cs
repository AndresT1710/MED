using SMED.Shared.DTOs;
using System.Net.Http.Json;
using System.Linq;

namespace SMED.FrontEnd.Services
{
    public class MedicalProcedureService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalProcedureService> _logger;

        public MedicalProcedureService(HttpClient httpClient, ILogger<MedicalProcedureService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalProcedureDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalProcedure");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalProcedureDTO>>();
                }

                _logger.LogWarning("Error al obtener procedimientos médicos: {StatusCode}", response.StatusCode);
                return new List<MedicalProcedureDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los procedimientos médicos");
                return new List<MedicalProcedureDTO>();
            }
        }

        public async Task<List<MedicalProcedureDTO>> GetByLocationAsync(int locationId)
        {
            try
            {
                _logger.LogInformation("Requesting procedures for location: {LocationId}", locationId);

                var response = await _httpClient.GetAsync($"api/MedicalProcedure/by-location/{locationId}");
                if (response.IsSuccessStatusCode)
                {
                    var procedures = await response.Content.ReadFromJsonAsync<List<MedicalProcedureDTO>>() ?? new List<MedicalProcedureDTO>();

                    _logger.LogInformation("Retrieved {Count} procedures", procedures.Count);
                    if (procedures.Any())
                    {
                        var firstProc = procedures.First();
                        _logger.LogInformation("First procedure - PatientId: {PatientId}, PatientName: {PatientName}",
                            firstProc.PatientId, firstProc.PatientName ?? "NULL");
                    }

                    return procedures;
                }

                _logger.LogWarning("Error al obtener procedimientos por ubicación: {StatusCode}", response.StatusCode);
                return new List<MedicalProcedureDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener procedimientos por ubicación: {LocationId}", locationId);
                return new List<MedicalProcedureDTO>();
            }
        }

        public async Task<int> GetPendingCountByLocationAsync(string locationName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalProcedure/pending-count/{locationName}");
                if (response.IsSuccessStatusCode)
                {
                    var countString = await response.Content.ReadAsStringAsync();
                    return int.TryParse(countString, out int count) ? count : 0;
                }

                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener conteo de procedimientos pendientes: {LocationName}", locationName);
                return 0;
            }
        }

        public async Task<MedicalProcedureDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalProcedure/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalProcedureDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener procedimiento médico por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<MedicalProcedureDTO>?> GetByPatientIdAsync(int patientId)
        {
            try
            {
                var allProcedures = await GetAllAsync();
                return allProcedures?.Where(p => p.PatientId == patientId).ToList() ?? new List<MedicalProcedureDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener procedimientos médicos por PatientId: {PatientId}", patientId);
                return new List<MedicalProcedureDTO>();
            }
        }

        public async Task<(bool Success, MedicalProcedureDTO? Data, string Error)> CreateAsync(MedicalProcedureDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalProcedure", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicalProcedureDTO>();
                    return (true, created, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear procedimiento médico");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(MedicalProcedureDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalProcedure/{dto.ProcedureId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar procedimiento médico");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> MarkAsCompletedAsync(int procedureId, int treatingPhysicianId)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalProcedure/{procedureId}/mark-completed", treatingPhysicianId);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar procedimiento como realizado");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> MarkAsCompletedAsync(int procedureId, string physicianName)
        {
            try
            {
                var requestData = new { PhysicianName = physicianName };
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalProcedure/{procedureId}/mark-completed-by-name", requestData);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al marcar procedimiento como realizado por nombre");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalProcedure/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar procedimiento médico");
                return (false, ex.Message);
            }
        }

        public async Task<List<MedicalProcedureDTO>> GetPendingByLocationAsync(int locationId)
        {
            try
            {
                // Llama al endpoint de pendientes por LocationId
                var response = await _httpClient.GetAsync($"api/MedicalProcedure/pending/by-location/{locationId}");
                if (response.IsSuccessStatusCode)
                {
                    var pendingProcedures = await response.Content.ReadFromJsonAsync<List<MedicalProcedureDTO>>();
                    return pendingProcedures ?? new List<MedicalProcedureDTO>();
                }

                _logger.LogWarning("Error al obtener procedimientos pendientes por ubicación: {StatusCode}", response.StatusCode);
                return new List<MedicalProcedureDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener procedimientos pendientes por ubicación: {LocationId}", locationId);
                return new List<MedicalProcedureDTO>();
            }
        }
    }
}
