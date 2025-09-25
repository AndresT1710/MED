using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class PrenatalHistoryService
    {
        private readonly HttpClient _httpClient;

        public PrenatalHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PrenatalHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrenatalHistoryDTO>>("api/prenatalhistory") ?? new List<PrenatalHistoryDTO>();
        }

        public async Task<PrenatalHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PrenatalHistoryDTO>($"api/prenatalhistory/{id}");
        }

        public async Task<PrenatalHistoryDTO> CreateAsync(PrenatalHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/prenatalhistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PrenatalHistoryDTO>() ?? throw new Exception("Error creating prenatal history");
        }

        public async Task<PrenatalHistoryDTO> UpdateAsync(PrenatalHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/prenatalhistory/{dto.PrenatalHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PrenatalHistoryDTO>() ?? throw new Exception("Error updating prenatal history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/prenatalhistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<PrenatalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}