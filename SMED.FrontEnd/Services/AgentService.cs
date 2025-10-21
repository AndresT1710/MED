using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class AgentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AgentService> _logger;

        public AgentService(HttpClient httpClient, ILogger<AgentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<AgentDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Agent");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AgentDTO>>();
                }
                _logger.LogWarning("Error al obtener agentes: {StatusCode}", response.StatusCode);
                return new List<AgentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener agentes");
                return new List<AgentDTO>();
            }
        }

        public async Task<AgentDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Agent/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AgentDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener agente por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, AgentDTO? Data, string Error)> CreateAsync(AgentDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Agent", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<AgentDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear agente");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(AgentDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Agent/{dto.AgentId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar agente");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Agent/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar agente");
                return (false, ex.Message);
            }
        }

        public async Task<List<AgentDTO>?> GetByPatientId(int patientId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Agent/ByPatient/{patientId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<AgentDTO>>();
                }
                _logger.LogWarning("Error al obtener agentes por paciente: {StatusCode}", response.StatusCode);
                return new List<AgentDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener agentes por paciente ID: {PatientId}", patientId);
                return new List<AgentDTO>();
            }
        }

        // Asignar agente a paciente
        public async Task<(bool Success, string Error)> AssignAgentToPatient(int patientId, int agentId)
        {
            try
            {
                var request = new { PatientId = patientId, AgentId = agentId };
                var response = await _httpClient.PutAsJsonAsync($"api/Agent/AssignToPatient", request);

                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al asignar agente al paciente");
                return (false, ex.Message);
            }
        }


        // Agrega este método en tu AgentService - para desvincular agente de paciente
        public async Task<(bool Success, string Error)> UnassignAgentFromPatient(int patientId, int agentId)
        {
            try
            {
                var request = new { PatientId = patientId, AgentId = agentId };
                var response = await _httpClient.PutAsJsonAsync($"api/Agent/UnassignFromPatient", request);

                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var error = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Error al desvincular agente: {StatusCode} - {Error}", response.StatusCode, error);
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desvincular agente del paciente");
                return (false, ex.Message);
            }
        }
    }
}