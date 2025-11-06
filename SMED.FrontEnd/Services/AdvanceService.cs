using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class AdvanceService
    {
        private readonly HttpClient _httpClient;

        public AdvanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AdvanceDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AdvanceDTO>>("api/Advance")
                    ?? new List<AdvanceDTO>();
            }
            catch (Exception)
            {
                return new List<AdvanceDTO>();
            }
        }

        public async Task<AdvanceDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AdvanceDTO>($"api/Advance/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AdvanceDTO?> CreateAsync(AdvanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Advance", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AdvanceDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<AdvanceDTO?> UpdateAsync(AdvanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Advance/{dto.AdvanceId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AdvanceDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Advance/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<AdvanceDTO>> GetBySessionIdAsync(int sessionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AdvanceDTO>>($"api/Advance/BySession/{sessionId}")
                    ?? new List<AdvanceDTO>();
            }
            catch (Exception)
            {
                return new List<AdvanceDTO>();
            }
        }

        public async Task<List<AdvanceDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var url = $"api/Advance/ByDateRange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";
                return await _httpClient.GetFromJsonAsync<List<AdvanceDTO>>(url)
                    ?? new List<AdvanceDTO>();
            }
            catch (Exception)
            {
                return new List<AdvanceDTO>();
            }
        }

        public async Task<List<AdvanceDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<AdvanceDTO>>($"api/Advance/ByMedicalCare/{medicalCareId}")
                    ?? new List<AdvanceDTO>();
            }
            catch (Exception)
            {
                return new List<AdvanceDTO>();
            }
        }
    }
}