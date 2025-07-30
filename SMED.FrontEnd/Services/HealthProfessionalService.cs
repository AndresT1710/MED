using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class HealthProfessionalService
    {
        private readonly HttpClient _httpClient;

        public HealthProfessionalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HealthProfessionalDTO>> GetAllHealthProfessionalsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<HealthProfessionalDTO>>("api/HealthProfessional");
                return response ?? new List<HealthProfessionalDTO>();
            }
            catch (Exception)
            {
                return new List<HealthProfessionalDTO>();
            }
        }

        public async Task<HealthProfessionalDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<HealthProfessionalDTO>($"api/HealthProfessional/{id}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting health professional by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HealthProfessionalDTO>> SearchHealthProfessionalsAsync(string searchTerm)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<HealthProfessionalDTO>>($"api/HealthProfessional/search?searchTerm={Uri.EscapeDataString(searchTerm)}");
                return response ?? new List<HealthProfessionalDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching health professionals: {ex.Message}");
                return new List<HealthProfessionalDTO>();
            }
        }
    }
}
