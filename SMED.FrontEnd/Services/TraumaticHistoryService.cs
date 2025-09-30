using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class TraumaticHistoryService
    {
        private readonly HttpClient _httpClient;

        public TraumaticHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TraumaticHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TraumaticHistoryDTO>>("api/traumatichistory") ?? new List<TraumaticHistoryDTO>();
        }

        public async Task<TraumaticHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TraumaticHistoryDTO>($"api/traumatichistory/{id}");
        }

        public async Task<TraumaticHistoryDTO> CreateAsync(TraumaticHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/traumatichistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TraumaticHistoryDTO>() ?? throw new Exception("Error creating traumatic history");
        }

        public async Task<TraumaticHistoryDTO> UpdateAsync(TraumaticHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/traumatichistory/{dto.TraumaticHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TraumaticHistoryDTO>() ?? throw new Exception("Error updating traumatic history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/traumatichistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TraumaticHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}