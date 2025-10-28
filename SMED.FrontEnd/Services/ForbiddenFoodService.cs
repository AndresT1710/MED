using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class ForbiddenFoodService
    {
        private readonly HttpClient _httpClient;

        public ForbiddenFoodService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ForbiddenFoodDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ForbiddenFoodDTO>>("api/ForbiddenFood")
                    ?? new List<ForbiddenFoodDTO>();
            }
            catch
            {
                return new List<ForbiddenFoodDTO>();
            }
        }

        public async Task<ForbiddenFoodDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ForbiddenFoodDTO>($"api/ForbiddenFood/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<ForbiddenFoodDTO?> CreateAsync(ForbiddenFoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ForbiddenFood", dto);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<ForbiddenFoodDTO>();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ForbiddenFoodDTO?> UpdateAsync(int id, ForbiddenFoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ForbiddenFood/{id}", dto);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<ForbiddenFoodDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/ForbiddenFood/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
