using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class TherapeuticPlanService
    {
        private readonly HttpClient _httpClient;

        public TherapeuticPlanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TherapeuticPlanDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TherapeuticPlanDTO>>("api/TherapeuticPlan")
                    ?? new List<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return new List<TherapeuticPlanDTO>();
            }
        }

        public async Task<TherapeuticPlanDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TherapeuticPlanDTO>($"api/TherapeuticPlan/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TherapeuticPlanDTO?> CreateAsync(TherapeuticPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TherapeuticPlan", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TherapeuticPlanDTO?> UpdateAsync(TherapeuticPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TherapeuticPlan/{dto.TherapeuticPlanId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/TherapeuticPlan/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TherapeuticPlanDTO>> GetByPsychologicalDiagnosisIdAsync(int psychologicalDiagnosisId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TherapeuticPlanDTO>>($"api/TherapeuticPlan/ByPsychologicalDiagnosis/{psychologicalDiagnosisId}")
                    ?? new List<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return new List<TherapeuticPlanDTO>();
            }
        }

        public async Task<List<TherapeuticPlanDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TherapeuticPlanDTO>>($"api/TherapeuticPlan/ByMedicalCare/{medicalCareId}")
                    ?? new List<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return new List<TherapeuticPlanDTO>();
            }
        }

        public async Task<List<TherapeuticPlanDTO>> GetByPatientIdAsync(int patientId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TherapeuticPlanDTO>>($"api/TherapeuticPlan/ByPatient/{patientId}")
                    ?? new List<TherapeuticPlanDTO>();
            }
            catch (Exception)
            {
                return new List<TherapeuticPlanDTO>();
            }
        }
    }
}