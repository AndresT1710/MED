using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class ActivityService
    {
        private readonly HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ActivityDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ActivityDTO>>("api/Activity")
                    ?? new List<ActivityDTO>();
            }
            catch (Exception)
            {
                return new List<ActivityDTO>();
            }
        }

        public async Task<ActivityDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ActivityDTO>($"api/Activity/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActivityDTO?> CreateAsync(ActivityDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Activity", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ActivityDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActivityDTO?> UpdateAsync(ActivityDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Activity/{dto.ActivityId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ActivityDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Activity/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ActivityDTO>> GetBySessionIdAsync(int sessionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ActivityDTO>>($"api/Activity/BySession/{sessionId}")
                    ?? new List<ActivityDTO>();
            }
            catch (Exception)
            {
                return new List<ActivityDTO>();
            }
        }

        public async Task<List<ActivityDTO>> GetByPsychologySessionIdAsync(int psychologySessionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ActivityDTO>>($"api/Activity/ByPsychologySession/{psychologySessionId}")
                    ?? new List<ActivityDTO>();
            }
            catch (Exception)
            {
                return new List<ActivityDTO>();
            }
        }

        public async Task<List<ActivityDTO>> GetByTypeOfActivityIdAsync(int typeOfActivityId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ActivityDTO>>($"api/Activity/ByTypeOfActivity/{typeOfActivityId}")
                    ?? new List<ActivityDTO>();
            }
            catch (Exception)
            {
                return new List<ActivityDTO>();
            }
        }
    }
}