using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class PerinatalHistoryService
    {
        private readonly HttpClient _httpClient;

        public PerinatalHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PerinatalHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PerinatalHistoryDTO>>("api/perinatalhistory") ?? new List<PerinatalHistoryDTO>();
        }

        public async Task<PerinatalHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PerinatalHistoryDTO>($"api/perinatalhistory/{id}");
        }

        public async Task<PerinatalHistoryDTO> CreateAsync(PerinatalHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/perinatalhistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PerinatalHistoryDTO>() ?? throw new Exception("Error creating perinatal history");
        }

        public async Task<PerinatalHistoryDTO> UpdateAsync(PerinatalHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/perinatalhistory/{dto.PerinatalHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PerinatalHistoryDTO>() ?? throw new Exception("Error updating perinatal history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/perinatalhistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PerinatalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}