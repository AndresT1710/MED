using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class BioImpedanceService
    {
        private readonly HttpClient _httpClient;

        public BioImpedanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BioImpedanceDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<BioImpedanceDTO>>("api/BioImpedance")
                    ?? new List<BioImpedanceDTO>();
            }
            catch (Exception)
            {
                return new List<BioImpedanceDTO>();
            }
        }

        public async Task<BioImpedanceDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BioImpedanceDTO>($"api/BioImpedance/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BioImpedanceDTO?> CreateAsync(BioImpedanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/BioImpedance", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BioImpedanceDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BioImpedanceDTO?> UpdateAsync(BioImpedanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/BioImpedance/{dto.BioImpedanceId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BioImpedanceDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/BioImpedance/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<BioImpedanceDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BioImpedanceDTO>($"api/BioImpedance/ByMeasurements/{measurementsId}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}