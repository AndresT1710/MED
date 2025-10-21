using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class RecommendedFoodsService
    {
        private readonly HttpClient _httpClient;

        public RecommendedFoodsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RecommendedFoodsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RecommendedFoodsDTO>>("api/RecommendedFoods")
                    ?? new List<RecommendedFoodsDTO>();
            }
            catch (Exception)
            {
                return new List<RecommendedFoodsDTO>();
            }
        }

        public async Task<RecommendedFoodsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RecommendedFoodsDTO>($"api/RecommendedFoods/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RecommendedFoodsDTO?> CreateAsync(RecommendedFoodsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/RecommendedFoods", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RecommendedFoodsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RecommendedFoodsDTO?> UpdateAsync(RecommendedFoodsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/RecommendedFoods/{dto.RecommendedFoodId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RecommendedFoodsDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/RecommendedFoods/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RecommendedFoodsDTO>> GetByFoodIdAsync(int foodId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RecommendedFoodsDTO>>($"api/RecommendedFoods/ByFood/{foodId}")
                    ?? new List<RecommendedFoodsDTO>();
            }
            catch (Exception)
            {
                return new List<RecommendedFoodsDTO>();
            }
        }

        public async Task<List<RecommendedFoodsDTO>> GetByFrequencyAsync(string frequency)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RecommendedFoodsDTO>>($"api/RecommendedFoods/ByFrequency/{frequency}")
                    ?? new List<RecommendedFoodsDTO>();
            }
            catch (Exception)
            {
                return new List<RecommendedFoodsDTO>();
            }
        }
    }
}