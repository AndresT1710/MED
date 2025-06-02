using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class HabitService
    {
        private readonly HttpClient _http;
        private const string BasePath = "api/habits";

        public HabitService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HabitsDTO?> GetByIdAsync(int habitId)
        {
            return await _http.GetFromJsonAsync<HabitsDTO>($"{BasePath}/{habitId}");
        }

        public async Task<List<HabitsDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<HabitsDTO>>(BasePath);
            return result ?? new List<HabitsDTO>();
        }

        public async Task<HabitsDTO?> CreateAsync(HabitsDTO habit)
        {
            var response = await _http.PostAsJsonAsync(BasePath, habit);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<HabitsDTO>();
            }

            return null;
        }

        public async Task<bool> UpdateAsync(HabitsDTO habit)
        {
            var response = await _http.PutAsJsonAsync($"{BasePath}/{habit.HabitId}", habit);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int habitId)
        {
            var response = await _http.DeleteAsync($"{BasePath}/{habitId}");
            return response.IsSuccessStatusCode;
        }
    }
}