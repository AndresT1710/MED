using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class MentalFunctionService
    {
        private readonly HttpClient _httpClient;

        public MentalFunctionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MentalFunctionDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MentalFunctionDTO>>("api/MentalFunction")
                    ?? new List<MentalFunctionDTO>();
            }
            catch (Exception)
            {
                return new List<MentalFunctionDTO>();
            }
        }

        public async Task<MentalFunctionDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MentalFunctionDTO>($"api/MentalFunction/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MentalFunctionDTO?> CreateAsync(MentalFunctionDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MentalFunction", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MentalFunctionDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MentalFunctionDTO?> UpdateAsync(MentalFunctionDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MentalFunction/{dto.MentalFunctionId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MentalFunctionDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MentalFunction/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<MentalFunctionDTO>> GetByTypeOfMentalFunctionIdAsync(int typeOfMentalFunctionId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MentalFunctionDTO>>($"api/MentalFunction/ByType/{typeOfMentalFunctionId}")
                    ?? new List<MentalFunctionDTO>();
            }
            catch (Exception)
            {
                return new List<MentalFunctionDTO>();
            }
        }
    }
}