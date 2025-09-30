using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class TransfusionsHistoryService
    {
        private readonly HttpClient _httpClient;

        public TransfusionsHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TransfusionsHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TransfusionsHistoryDTO>>("api/transfusionshistory") ?? new List<TransfusionsHistoryDTO>();
        }

        public async Task<TransfusionsHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TransfusionsHistoryDTO>($"api/transfusionshistory/{id}");
        }

        public async Task<TransfusionsHistoryDTO> CreateAsync(TransfusionsHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/transfusionshistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TransfusionsHistoryDTO>() ?? throw new Exception("Error creating transfusions history");
        }

        public async Task<TransfusionsHistoryDTO> UpdateAsync(TransfusionsHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/transfusionshistory/{dto.TransfusionsHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TransfusionsHistoryDTO>() ?? throw new Exception("Error updating transfusions history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/transfusionshistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<TransfusionsHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}