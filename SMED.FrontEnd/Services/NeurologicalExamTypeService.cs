using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.Frontend.Services
{
    public class NeurologicalExamTypeService
    {
        private readonly HttpClient _httpClient;

        public NeurologicalExamTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NeurologicalExamTypeDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NeurologicalExamTypeDTO>>("api/neurologicalexamtype") ?? new List<NeurologicalExamTypeDTO>();
        }

        public async Task<NeurologicalExamTypeDTO?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<NeurologicalExamTypeDTO>($"api/neurologicalexamtype/{id}");
        }

        public async Task<NeurologicalExamTypeDTO> CreateAsync(NeurologicalExamTypeDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/neurologicalexamtype", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeurologicalExamTypeDTO>() ?? throw new Exception("Error creating neurological exam type");
        }

        public async Task<NeurologicalExamTypeDTO> UpdateAsync(NeurologicalExamTypeDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/neurologicalexamtype/{dto.NeurologicalExamTypeId}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NeurologicalExamTypeDTO>() ?? throw new Exception("Error updating neurological exam type");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/neurologicalexamtype/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}