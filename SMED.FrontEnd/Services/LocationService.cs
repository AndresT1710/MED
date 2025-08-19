using SMED.Shared.DTOs;
using System.Net.Http.Json;
using System.Linq;

namespace SMED.FrontEnd.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocationService> _logger;

        public LocationService(HttpClient httpClient, ILogger<LocationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<LocationDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Location");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LocationDTO>>();
                }

                _logger.LogWarning("Error al obtener ubicaciones: {StatusCode}", response.StatusCode);
                return new List<LocationDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las ubicaciones");
                return new List<LocationDTO>();
            }
        }

        public async Task<LocationDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Location/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LocationDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ubicación por ID: {Id}", id);
                return null;
            }
        }

        public async Task<LocationDTO?> GetByNameAsync(string name)
        {
            try
            {
                var locations = await GetAllAsync();
                return locations?.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ubicación por nombre: {Name}", name);
                return null;
            }
        }

        public async Task<int?> GetLocationIdByNameAsync(string name)
        {
            try
            {
                var location = await GetByNameAsync(name);
                return location?.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ID de ubicación por nombre: {Name}", name);
                return null;
            }
        }
    }
}
