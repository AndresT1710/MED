using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class RestrictionService
    {
        private readonly HttpClient _httpClient;

        public RestrictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RestrictionDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RestrictionDTO>>("api/Restriction")
                    ?? new List<RestrictionDTO>();
            }
            catch (Exception)
            {
                return new List<RestrictionDTO>();
            }
        }

        public async Task<RestrictionDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RestrictionDTO>($"api/Restriction/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RestrictionDTO?> CreateAsync(RestrictionDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Restriction", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RestrictionDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RestrictionDTO?> UpdateAsync(RestrictionDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Restriction/{dto.RestrictionId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RestrictionDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Restriction/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RestrictionDTO>> GetByFoodIdAsync(int foodId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<RestrictionDTO>>($"api/Restriction/ByFood/{foodId}")
                    ?? new List<RestrictionDTO>();
            }
            catch (Exception)
            {
                return new List<RestrictionDTO>();
            }
        }
    }
}