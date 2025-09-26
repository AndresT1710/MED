using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class DevelopmentRecordService
    {
        private readonly HttpClient _httpClient;

        public DevelopmentRecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DevelopmentRecordDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DevelopmentRecordDTO>>("api/developmentrecord") ?? new List<DevelopmentRecordDTO>();
        }

        public async Task<DevelopmentRecordDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<DevelopmentRecordDTO>($"api/developmentrecord/{id}");
        }

        public async Task<DevelopmentRecordDTO> CreateAsync(DevelopmentRecordDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/developmentrecord", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DevelopmentRecordDTO>() ?? throw new Exception("Error creating development record");
        }

        public async Task<DevelopmentRecordDTO> UpdateAsync(DevelopmentRecordDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/developmentrecord/{dto.DevelopmentRecordId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DevelopmentRecordDTO>() ?? throw new Exception("Error updating development record");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/developmentrecord/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<DevelopmentRecordDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}