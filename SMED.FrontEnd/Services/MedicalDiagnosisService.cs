using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    /// <summary>
    /// Servicio para gestionar las operaciones CRUD de MedicalDiagnosis
    /// Maneja los diagnósticos médicos de los pacientes
    /// </summary>
    public class MedicalDiagnosisService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MedicalDiagnosisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Obtiene todos los diagnósticos médicos
        /// </summary>
        public async Task<List<MedicalDiagnosisDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalDiagnosis");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<MedicalDiagnosisDTO>>(json, _jsonOptions) ?? new List<MedicalDiagnosisDTO>();
                }
                return new List<MedicalDiagnosisDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener diagnósticos médicos: {ex.Message}");
                return new List<MedicalDiagnosisDTO>();
            }
        }

        /// <summary>
        /// Obtiene un diagnóstico médico específico por ID
        /// </summary>
        public async Task<MedicalDiagnosisDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalDiagnosis/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<MedicalDiagnosisDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener diagnóstico médico por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los diagnósticos médicos asociados a una atención médica específica
        /// </summary>
        public async Task<List<MedicalDiagnosisDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allDiagnoses = await GetAllAsync();
                return allDiagnoses.Where(d => d.MedicalCareId == medicalCareId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener diagnósticos médicos por MedicalCareId: {ex.Message}");
                return new List<MedicalDiagnosisDTO>();
            }
        }

        /// <summary>
        /// Crea un nuevo diagnóstico médico
        /// </summary>
        public async Task<(bool Success, MedicalDiagnosisDTO? Data, string Error)> CreateAsync(MedicalDiagnosisDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/MedicalDiagnosis", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var created = JsonSerializer.Deserialize<MedicalDiagnosisDTO>(responseJson, _jsonOptions);
                    return (true, created, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al crear diagnóstico médico: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un diagnóstico médico existente
        /// </summary>
        public async Task<(bool Success, MedicalDiagnosisDTO? Data, string Error)> UpdateAsync(MedicalDiagnosisDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/MedicalDiagnosis/{dto.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updated = JsonSerializer.Deserialize<MedicalDiagnosisDTO>(responseJson, _jsonOptions);
                    return (true, updated, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error al actualizar diagnóstico médico: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un diagnóstico médico por ID
        /// </summary>
        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalDiagnosis/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, $"Error del servidor: {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, $"Error al eliminar diagnóstico médico: {ex.Message}");
            }
        }
    }
}
