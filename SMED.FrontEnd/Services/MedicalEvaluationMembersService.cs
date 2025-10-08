using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalEvaluationMembersService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalEvaluationMembersService> _logger;

        public MedicalEvaluationMembersService(HttpClient httpClient, ILogger<MedicalEvaluationMembersService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicalEvaluationMembersDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalEvaluationMembers");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicalEvaluationMembersDTO>>();
                }
                _logger.LogWarning("Error al obtener miembros de evaluación médica: {StatusCode}", response.StatusCode);
                return new List<MedicalEvaluationMembersDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener miembros de evaluación médica");
                return new List<MedicalEvaluationMembersDTO>();
            }
        }

        public async Task<MedicalEvaluationMembersDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalEvaluationMembers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicalEvaluationMembersDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener miembro de evaluación médica por ID: {Id}", id);
                return null;
            }
        }
    }
}