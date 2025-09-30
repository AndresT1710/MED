using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class HospitalizationsHistoryService
    {
        private readonly HttpClient _httpClient;

        public HospitalizationsHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HospitalizationsHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HospitalizationsHistoryDTO>>("api/hospitalizationshistory") ?? new List<HospitalizationsHistoryDTO>();
        }

        public async Task<HospitalizationsHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HospitalizationsHistoryDTO>($"api/hospitalizationshistory/{id}");
        }

        public async Task<HospitalizationsHistoryDTO> CreateAsync(HospitalizationsHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hospitalizationshistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HospitalizationsHistoryDTO>() ?? throw new Exception("Error creating hospitalizations history");
        }

        public async Task<HospitalizationsHistoryDTO> UpdateAsync(HospitalizationsHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/hospitalizationshistory/{dto.HospitalizationsHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<HospitalizationsHistoryDTO>() ?? throw new Exception("Error updating hospitalizations history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/hospitalizationshistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<HospitalizationsHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}