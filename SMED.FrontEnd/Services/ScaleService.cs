using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class ScaleService
    {
        private readonly HttpClient _httpClient;

        public ScaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ScaleDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ScaleDTO>>("api/Scale")
                    ?? new List<ScaleDTO>();
            }
            catch (Exception)
            {
                return new List<ScaleDTO>();
            }
        }

        public async Task<ScaleDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ScaleDTO>($"api/Scale/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
