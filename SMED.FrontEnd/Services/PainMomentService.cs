using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PainMomentService
    {
        private readonly HttpClient _httpClient;

        public PainMomentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PainMomentDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PainMomentDTO>>("api/PainMoment")
                    ?? new List<PainMomentDTO>();
            }
            catch (Exception)
            {
                return new List<PainMomentDTO>();
            }
        }

        public async Task<PainMomentDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PainMomentDTO>($"api/PainMoment/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
