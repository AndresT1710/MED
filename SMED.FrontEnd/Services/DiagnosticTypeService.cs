using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de DiagnosticType
    /// Maneja los tipos de diagnóstico disponibles
    /// </summary>
    public class DiagnosticTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public DiagnosticTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Obtiene todos los tipos de diagnóstico
        /// </summary>
        public async Task<List<DiagnosticTypeDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DiagnosticType");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<DiagnosticTypeDTO>>(json, _jsonOptions) ?? new List<DiagnosticTypeDTO>();
                }
                return new List<DiagnosticTypeDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener tipos de diagnóstico: {ex.Message}");
                return new List<DiagnosticTypeDTO>();
            }
        }

        /// <summary>
        /// Obtiene un tipo de diagnóstico específico por ID
        /// </summary>
        public async Task<DiagnosticTypeDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/DiagnosticType/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<DiagnosticTypeDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener tipo de diagnóstico por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el nombre de un tipo de diagnóstico por su ID
        /// </summary>
        public async Task<string> GetDiagnosticTypeNameByIdAsync(int id)
        {
            try
            {
                var diagnosticType = await GetByIdAsync(id);
                return diagnosticType?.Name ?? "Tipo no encontrado";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener nombre del tipo de diagnóstico: {ex.Message}");
                return "Error al cargar";
            }
        }

        /// <summary>
        /// Crea un nuevo tipo de diagnóstico
        /// </summary>
        public async Task<(bool Success, DiagnosticTypeDTO? Data, string Error)> CreateAsync(DiagnosticTypeDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/DiagnosticType", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var created = JsonSerializer.Deserialize<DiagnosticTypeDTO>(responseJson, _jsonOptions);
                    return (true, created, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al crear tipo de diagnóstico: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un tipo de diagnóstico existente
        /// </summary>
        public async Task<(bool Success, DiagnosticTypeDTO? Data, string Error)> UpdateAsync(DiagnosticTypeDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/DiagnosticType/{dto.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updated = JsonSerializer.Deserialize<DiagnosticTypeDTO>(responseJson, _jsonOptions);
                    return (true, updated, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al actualizar tipo de diagnóstico: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un tipo de diagnóstico por ID
        /// </summary>
        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/DiagnosticType/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar tipo de diagnóstico: {ex.Message}");
            }
        }
    }
}
