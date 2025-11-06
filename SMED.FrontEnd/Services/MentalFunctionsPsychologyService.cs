using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class MentalFunctionsPsychologyService
    {
        private readonly HttpClient _httpClient;

        public MentalFunctionsPsychologyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MentalFunctionsPsychologyDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MentalFunctionsPsychologyDTO>>("api/MentalFunctionsPsychology")
                    ?? new List<MentalFunctionsPsychologyDTO>();
            }
            catch
            {
                return new List<MentalFunctionsPsychologyDTO>();
            }
        }

        public async Task<MentalFunctionsPsychologyDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MentalFunctionsPsychologyDTO>($"api/MentalFunctionsPsychology/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<MentalFunctionsPsychologyDTO?> CreateAsync(MentalFunctionsPsychologyDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MentalFunctionsPsychology", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MentalFunctionsPsychologyDTO>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<MentalFunctionsPsychologyDTO?> UpdateAsync(MentalFunctionsPsychologyDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MentalFunctionsPsychology/{dto.MentalFunctionsPsychologyId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MentalFunctionsPsychologyDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/MentalFunctionsPsychology/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MentalFunctionsPsychologyDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MentalFunctionsPsychologyDTO>>($"api/MentalFunctionsPsychology/ByMedicalCare/{medicalCareId}")
                    ?? new List<MentalFunctionsPsychologyDTO>();
            }
            catch
            {
                return new List<MentalFunctionsPsychologyDTO>();
            }
        }
    }
}
