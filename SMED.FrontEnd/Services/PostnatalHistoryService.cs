using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class PostnatalHistoryService
    {
        private readonly HttpClient _httpClient;

        public PostnatalHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PostnatalHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PostnatalHistoryDTO>>("api/postnatalhistory") ?? new List<PostnatalHistoryDTO>();
        }

        public async Task<PostnatalHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PostnatalHistoryDTO>($"api/postnatalhistory/{id}");
        }

        public async Task<PostnatalHistoryDTO> CreateAsync(PostnatalHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/postnatalhistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostnatalHistoryDTO>() ?? throw new Exception("Error creating postnatal history");
        }

        public async Task<PostnatalHistoryDTO> UpdateAsync(PostnatalHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/postnatalhistory/{dto.PostNatalId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostnatalHistoryDTO>() ?? throw new Exception("Error updating postnatal history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/postnatalhistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PostnatalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}