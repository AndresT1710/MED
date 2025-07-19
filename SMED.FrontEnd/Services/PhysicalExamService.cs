using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de PhysicalExam
    /// Maneja los exámenes físicos de los pacientes
    /// </summary>
    public class PhysicalExamService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PhysicalExamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Obtiene todos los exámenes físicos
        /// </summary>
        public async Task<List<PhysicalExamDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PhysicalExam");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<PhysicalExamDTO>>(json, _jsonOptions) ?? new List<PhysicalExamDTO>();
                }
                return new List<PhysicalExamDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener exámenes físicos: {ex.Message}");
                return new List<PhysicalExamDTO>();
            }
        }

        /// <summary>
        /// Obtiene un examen físico específico por ID
        /// </summary>
        public async Task<PhysicalExamDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PhysicalExam/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PhysicalExamDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener examen físico por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los exámenes físicos asociados a una atención médica específica
        /// </summary>
        public async Task<List<PhysicalExamDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/PhysicalExam/by-medical-care/{medicalCareId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<PhysicalExamDTO>>(json, _jsonOptions) ?? new List<PhysicalExamDTO>();
                }
                return new List<PhysicalExamDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener exámenes físicos por MedicalCareId: {ex.Message}");
                return new List<PhysicalExamDTO>();
            }
        }

        /// <summary>
        /// Crea un nuevo examen físico
        /// </summary>
        public async Task<(bool Success, PhysicalExamDTO? Data, string Error)> CreateAsync(PhysicalExamDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/PhysicalExam", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var created = JsonSerializer.Deserialize<PhysicalExamDTO>(responseJson, _jsonOptions);
                    return (true, created, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al crear examen físico: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un examen físico existente
        /// </summary>
        public async Task<(bool Success, PhysicalExamDTO? Data, string Error)> UpdateAsync(PhysicalExamDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/PhysicalExam/{dto.PhysicalExamId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updated = JsonSerializer.Deserialize<PhysicalExamDTO>(responseJson, _jsonOptions);
                    return (true, updated, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al actualizar examen físico: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un examen físico por ID
        /// </summary>
        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PhysicalExam/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar examen físico: {ex.Message}");
            }
        }
    }
}
