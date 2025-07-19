using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de Region
    /// Maneja las regiones anatómicas para exámenes físicos
    /// </summary>
    public class RegionService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public RegionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Obtiene todas las regiones disponibles
        /// </summary>
        public async Task<List<RegionDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Region");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<RegionDTO>>(json, _jsonOptions) ?? new List<RegionDTO>();
                }
                return new List<RegionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener regiones: {ex.Message}");
                return new List<RegionDTO>();
            }
        }

        /// <summary>
        /// Obtiene una región específica por ID
        /// </summary>
        public async Task<RegionDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Region/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<RegionDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener región por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el nombre de una región por su ID
        /// </summary>
        public async Task<string> GetRegionNameByIdAsync(int regionId)
        {
            try
            {
                var region = await GetByIdAsync(regionId);
                return region?.Name ?? "Región no encontrada";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener nombre de región: {ex.Message}");
                return "Error al cargar región";
            }
        }
    }
}
