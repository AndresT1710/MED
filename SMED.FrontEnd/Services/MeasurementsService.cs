using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class MeasurementsService
    {
        private readonly HttpClient _httpClient;

        public MeasurementsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MeasurementsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MeasurementsDTO>>("api/Measurements")
                    ?? new List<MeasurementsDTO>();
            }
            catch (Exception)
            {
                return new List<MeasurementsDTO>();
            }
        }

        public async Task<MeasurementsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MeasurementsDTO>($"api/Measurements/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MeasurementsDTO?> CreateAsync(MeasurementsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Measurements", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MeasurementsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MeasurementsDTO?> UpdateAsync(MeasurementsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Measurements/{dto.MeasurementsId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MeasurementsDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Measurements/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MeasurementsDTO?> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MeasurementsDTO>($"api/Measurements/ByMedicalCare/{medicalCareId}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}