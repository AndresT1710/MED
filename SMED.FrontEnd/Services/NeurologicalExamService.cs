using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class NeurologicalExamService
    {
        private readonly HttpClient _httpClient;

        public NeurologicalExamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NeurologicalExamDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NeurologicalExamDTO>>("api/neurologicalexam") ?? new List<NeurologicalExamDTO>();
        }

        public async Task<NeurologicalExamDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<NeurologicalExamDTO>($"api/neurologicalexam/{id}");
        }

        public async Task<NeurologicalExamDTO> CreateAsync(NeurologicalExamDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/neurologicalexam", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeurologicalExamDTO>() ?? throw new Exception("Error creating neurological exam");
        }

        public async Task<NeurologicalExamDTO> UpdateAsync(NeurologicalExamDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/neurologicalexam/{dto.NeurologicalExamId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeurologicalExamDTO>() ?? throw new Exception("Error updating neurological exam");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/neurologicalexam/{id}");
            return response.IsSuccessStatusCode;
        }

        
        public async Task<List<NeurologicalExamDTO>> GetAllExamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NeurologicalExamDTO>>("api/neurologicalexam");
        }

        public async Task<List<NeurologicalExamDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList();
        }

        public async Task<List<NeurologicalExamDTO>> GetByExamTypeIdAsync(int examTypeId)
        {
            var all = await GetAllAsync();
            return all.Where(x => x.NeurologicalExamTypeId == examTypeId).ToList();
        }
    }
}