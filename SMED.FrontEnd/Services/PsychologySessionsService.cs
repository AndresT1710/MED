using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PsychologySessionsService
    {
        private readonly HttpClient _httpClient;

        public PsychologySessionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PsychologySessionsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologySessionsDTO>>("api/PsychologySessions")
                    ?? new List<PsychologySessionsDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologySessionsDTO>();
            }
        }

        public async Task<PsychologySessionsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PsychologySessionsDTO>($"api/PsychologySessions/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PsychologySessionsDTO?> CreateAsync(PsychologySessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PsychologySessions", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PsychologySessionsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PsychologySessionsDTO?> UpdateAsync(PsychologySessionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PsychologySessions/{dto.PsychologySessionsId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PsychologySessionsDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/PsychologySessions/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PsychologySessionsDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologySessionsDTO>>($"api/PsychologySessions/ByMedicalCare/{medicalCareId}")
                    ?? new List<PsychologySessionsDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologySessionsDTO>();
            }
        }

        public async Task<List<PsychologySessionsDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var url = $"api/PsychologySessions/ByDateRange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
                return await _httpClient.GetFromJsonAsync<List<PsychologySessionsDTO>>(url)
                    ?? new List<PsychologySessionsDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologySessionsDTO>();
            }
        }

        public async Task<List<PsychologySessionsDTO>> GetByMedicalDischargeAsync(bool medicalDischarge)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PsychologySessionsDTO>>($"api/PsychologySessions/ByMedicalDischarge/{medicalDischarge}")
                    ?? new List<PsychologySessionsDTO>();
            }
            catch (Exception)
            {
                return new List<PsychologySessionsDTO>();
            }
        }
    }
}