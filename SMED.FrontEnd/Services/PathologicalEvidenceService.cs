using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de PathologicalEvidence
    /// Maneja las evidencias patológicas para exámenes físicos
    /// </summary>
    public class PathologicalEvidenceService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PathologicalEvidenceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Obtiene todas las evidencias patológicas disponibles
        /// </summary>
        public async Task<List<PathologicalEvidenceDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PathologicalEvidence");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<PathologicalEvidenceDTO>>(json, _jsonOptions) ?? new List<PathologicalEvidenceDTO>();
                }
                return new List<PathologicalEvidenceDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener evidencias patológicas: {ex.Message}");
                return new List<PathologicalEvidenceDTO>();
            }
        }

        /// <summary>
        /// Obtiene una evidencia patológica específica por ID
        /// </summary>
        public async Task<PathologicalEvidenceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PathologicalEvidence/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PathologicalEvidenceDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener evidencia patológica por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el nombre de una evidencia patológica por su ID
        /// </summary>
        public async Task<string> GetPathologicalEvidenceNameByIdAsync(int evidenceId)
        {
            try
            {
                var evidence = await GetByIdAsync(evidenceId);
                return evidence?.Name ?? "Evidencia no encontrada";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener nombre de evidencia patológica: {ex.Message}");
                return "Error al cargar evidencia";
            }
        }
    }
}
