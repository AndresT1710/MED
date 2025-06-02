using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class SurgeryService
    {
        private readonly HttpClient _http;
        private const string BasePath = "api/surgery";

        public SurgeryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<SurgeryDTO?> GetByIdAsync(int surgeryId)
        {
            return await _http.GetFromJsonAsync<SurgeryDTO>($"{BasePath}/{surgeryId}");
        }

        public async Task<List<SurgeryDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<SurgeryDTO>>(BasePath);
            return result ?? new List<SurgeryDTO>();
        }
    }
}