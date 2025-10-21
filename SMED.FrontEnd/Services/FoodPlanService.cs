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
            catch (Exception)
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
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<FoodPlanDTO?> CreateAsync(FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/FoodPlan", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<FoodPlanDTO?> UpdateAsync(FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/FoodPlan/{dto.FoodPlanId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/FoodPlan/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<FoodPlanDTO>> GetByCareIdAsync(int careId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FoodPlanDTO>>($"api/FoodPlan/ByCare/{careId}")
                    ?? new List<FoodPlanDTO>();
            }
            catch (Exception)
            {
                return new List<FoodPlanDTO>();
            }
        }

        public async Task<FoodPlanDTO?> GetByRestrictionIdAsync(int restrictionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FoodPlanDTO>($"api/FoodPlan/ByRestriction/{restrictionId}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<FoodPlanDTO>> GetByRecommendedFoodIdAsync(int recommendedFoodId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FoodPlanDTO>>($"api/FoodPlan/ByRecommendedFood/{recommendedFoodId}")
                    ?? new List<FoodPlanDTO>();
            }
            catch (Exception)
            {
                return new List<FoodPlanDTO>();
            }
        }
    }
}