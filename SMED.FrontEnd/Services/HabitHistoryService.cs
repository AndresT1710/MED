using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class HabitHistoryService
    {
        private readonly HttpClient _http;
        private const string BasePath = "api/habitHistory";

        public HabitHistoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HabitHistoryDTO?> GetByIdAsync(int habitHistoryId)
        {
            return await _http.GetFromJsonAsync<HabitHistoryDTO>($"{BasePath}/{habitHistoryId}");
        }

        public async Task<List<HabitHistoryDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<HabitHistoryDTO>>(BasePath);
            return result ?? new List<HabitHistoryDTO>();
        }

        public async Task<List<HabitHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var result = await _http.GetFromJsonAsync<List<HabitHistoryDTO>>($"{BasePath}/byClinicalHistory/{clinicalHistoryId}");
            return result ?? new List<HabitHistoryDTO>();
        }

        public async Task<HabitHistoryDTO?> CreateAsync(HabitHistoryDTO habitHistory)
        {
            var response = await _http.PostAsJsonAsync(BasePath, habitHistory);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabitHistoryDTO>();
            }

            return null;
        }

        public async Task<bool> UpdateAsync(HabitHistoryDTO habitHistory)
        {
            var response = await _http.PutAsJsonAsync($"{BasePath}/{habitHistory.HabitHistoryId}", habitHistory);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int habitHistoryId)
        {
            var response = await _http.DeleteAsync($"{BasePath}/{habitHistoryId}");
            return response.IsSuccessStatusCode;
        }
    }
}