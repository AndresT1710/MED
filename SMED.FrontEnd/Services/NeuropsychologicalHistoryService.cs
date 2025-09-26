using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class NeuropsychologicalHistoryService
    {
        private readonly HttpClient _httpClient;

        public NeuropsychologicalHistoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NeuropsychologicalHistoryDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NeuropsychologicalHistoryDTO>>("api/neuropsychologicalhistory") ?? new List<NeuropsychologicalHistoryDTO>();
        }

        public async Task<NeuropsychologicalHistoryDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<NeuropsychologicalHistoryDTO>($"api/neuropsychologicalhistory/{id}");
        }

        public async Task<NeuropsychologicalHistoryDTO> CreateAsync(NeuropsychologicalHistoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/neuropsychologicalhistory", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeuropsychologicalHistoryDTO>() ?? throw new Exception("Error creating neuropsychological history");
        }

        public async Task<NeuropsychologicalHistoryDTO> UpdateAsync(NeuropsychologicalHistoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/neuropsychologicalhistory/{dto.NeuropsychologicalHistoryId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeuropsychologicalHistoryDTO>() ?? throw new Exception("Error updating neuropsychological history");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/neuropsychologicalhistory/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<NeuropsychologicalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }
    }
}