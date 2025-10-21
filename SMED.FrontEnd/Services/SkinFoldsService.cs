using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class SkinFoldsService
    {
        private readonly HttpClient _httpClient;

        public SkinFoldsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SkinFoldsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<SkinFoldsDTO>>("api/SkinFolds")
                    ?? new List<SkinFoldsDTO>();
            }
            catch (Exception)
            {
                return new List<SkinFoldsDTO>();
            }
        }

        public async Task<SkinFoldsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SkinFoldsDTO>($"api/SkinFolds/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SkinFoldsDTO?> CreateAsync(SkinFoldsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SkinFolds", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SkinFoldsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SkinFoldsDTO?> UpdateAsync(SkinFoldsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SkinFolds/{dto.SkinFoldsId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SkinFoldsDTO>();
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
                var response = await _httpClient.DeleteAsync($"api/SkinFolds/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<SkinFoldsDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SkinFoldsDTO>($"api/SkinFolds/ByMeasurements/{measurementsId}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}