using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class AllergyService
    {
        private readonly HttpClient _http;
        private const string BasePath = "api/allergy";

        public AllergyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AllergyDTO?> GetByIdAsync(int allergyId)
        {
            return await _http.GetFromJsonAsync<AllergyDTO>($"{BasePath}/{allergyId}");
        }

        public async Task<List<AllergyDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<AllergyDTO>>(BasePath);
            return result ?? new List<AllergyDTO>();
        }
    }
}