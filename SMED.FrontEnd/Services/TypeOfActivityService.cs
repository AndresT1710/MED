using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class TypeOfActivityService
    {
        private readonly HttpClient _httpClient;

        public TypeOfActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypeOfActivityDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TypeOfActivityDTO>>("api/TypeOfActivity")
                       ?? new List<TypeOfActivityDTO>();
            }
            catch
            {
                return new List<TypeOfActivityDTO>();
            }
        }

        public async Task<TypeOfActivityDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TypeOfActivityDTO>($"api/TypeOfActivity/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<TypeOfActivityDTO?> CreateAsync(TypeOfActivityDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TypeOfActivity", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TypeOfActivityDTO>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<TypeOfActivityDTO?> UpdateAsync(TypeOfActivityDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TypeOfActivity/{dto.TypeOfActivityId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TypeOfActivityDTO>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/TypeOfActivity/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}