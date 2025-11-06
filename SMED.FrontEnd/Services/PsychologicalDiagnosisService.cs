using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PsychologicalDiagnosisService
    {
        private readonly HttpClient _httpClient;

        public PsychologicalDiagnosisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologicalDiagnosisDTO>>("api/PsychologicalDiagnosis")
                    ?? new List<PsychologicalDiagnosisDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologicalDiagnosisDTO>();
            }
        }

        public async Task<PsychologicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PsychologicalDiagnosisDTO>($"api/PsychologicalDiagnosis/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PsychologicalDiagnosisDTO?> CreateAsync(PsychologicalDiagnosisDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PsychologicalDiagnosis", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PsychologicalDiagnosisDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PsychologicalDiagnosisDTO?> UpdateAsync(PsychologicalDiagnosisDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PsychologicalDiagnosis/{dto.PsychologicalDiagnosisId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PsychologicalDiagnosisDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/PsychologicalDiagnosis/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PsychologicalDiagnosisDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PsychologicalDiagnosisDTO>($"api/PsychologicalDiagnosis/ByMedicalCare/{medicalCareId}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetByCIE10Async(string cie10)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologicalDiagnosisDTO>>($"api/PsychologicalDiagnosis/ByCIE10/{cie10}")
                    ?? new List<PsychologicalDiagnosisDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologicalDiagnosisDTO>();
            }
        }

        public async Task<List<PsychologicalDiagnosisDTO>> GetByDiagnosticTypeIdAsync(int diagnosticTypeId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologicalDiagnosisDTO>>($"api/PsychologicalDiagnosis/ByDiagnosticType/{diagnosticTypeId}")
                    ?? new List<PsychologicalDiagnosisDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologicalDiagnosisDTO>();
            }
        }
    }
}