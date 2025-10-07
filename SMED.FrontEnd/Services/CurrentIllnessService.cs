using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class CurrentIllnessService
    {
        private readonly HttpClient _httpClient;

        public CurrentIllnessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CurrentIllnessDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CurrentIllnessDTO>>("api/CurrentIllness")
                    ?? new List<CurrentIllnessDTO>();
            }
            catch (Exception)
            {
                return new List<CurrentIllnessDTO>();
            }
        }

        public async Task<CurrentIllnessDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CurrentIllnessDTO>($"api/CurrentIllness/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CurrentIllnessDTO?> CreateAsync(CurrentIllnessDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CurrentIllness", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CurrentIllnessDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CurrentIllnessDTO?> UpdateAsync(CurrentIllnessDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/CurrentIllness/{dto.CurrentIllnessId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<CurrentIllnessDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/CurrentIllness/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CurrentIllnessDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CurrentIllnessDTO>($"api/CurrentIllness/ByMedicalCare/{medicalCareId}");
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
