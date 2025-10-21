using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PerimetersService
    {
        private readonly HttpClient _httpClient;

        public PerimetersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PerimetersDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PerimetersDTO>>("api/Perimeters")
                    ?? new List<PerimetersDTO>();
            }
            catch (Exception)
            {
                return new List<PerimetersDTO>();
            }
        }

        public async Task<PerimetersDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PerimetersDTO>($"api/Perimeters/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PerimetersDTO?> CreateAsync(PerimetersDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Perimeters", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PerimetersDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PerimetersDTO?> UpdateAsync(PerimetersDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Perimeters/{dto.PerimetersId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PerimetersDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Perimeters/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PerimetersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PerimetersDTO>($"api/Perimeters/ByMeasurements/{measurementsId}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}