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
    }
}