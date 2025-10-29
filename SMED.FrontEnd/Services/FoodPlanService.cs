using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class FoodPlanService
    {
        private readonly HttpClient _httpClient;

        public FoodPlanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FoodPlanDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FoodPlanDTO>>("api/FoodPlan")
                    ?? new List<FoodPlanDTO>();
            }
            catch
            {
                return new List<FoodPlanDTO>();
            }
        }

        public async Task<FoodPlanDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FoodPlanDTO>($"api/FoodPlan/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<FoodPlanDTO?> CreateAsync(FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/FoodPlan", dto);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<FoodPlanDTO?> UpdateAsync(int id, FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/FoodPlan/{id}", dto);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FoodPlan/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
