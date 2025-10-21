using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class DiametersService
    {
        private readonly HttpClient _httpClient;

        public DiametersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DiametersDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<DiametersDTO>>("api/Diameters")
                    ?? new List<DiametersDTO>();
            }
            catch (Exception)
            {
                return new List<DiametersDTO>();
            }
        }

        public async Task<DiametersDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DiametersDTO>($"api/Diameters/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DiametersDTO?> CreateAsync(DiametersDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Diameters", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<DiametersDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DiametersDTO?> UpdateAsync(DiametersDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Diameters/{dto.DiametersId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<DiametersDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/Diameters/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DiametersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DiametersDTO>($"api/Diameters/ByMeasurements/{measurementsId}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}