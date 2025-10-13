using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    public class SystemsDevicesService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public SystemsDevicesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public async Task<List<SystemsDevicesDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/SystemsDevices");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<SystemsDevicesDTO>>(json, _jsonOptions) ?? new();
            }
            return new();
        }

        public async Task<string> GetSystemNameByIdAsync(int id)
        {
            var systems = await GetAllAsync();
            return systems.FirstOrDefault(s => s.Id == id)?.Name ?? "Sistema no encontrado";
        }

        public async Task<SystemsDevicesDTO?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/SystemsDevices/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SystemsDevicesDTO>(json, _jsonOptions);
            }
            return null;
        }

    }
}