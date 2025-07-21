using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    public class EvolutionService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EvolutionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<EvolutionDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Evolution");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<EvolutionDTO>>(json, _jsonOptions) ?? new List<EvolutionDTO>();
                }
                return new List<EvolutionDTO>();
            }
            catch
            {
                return new List<EvolutionDTO>();
            }
        }

        public async Task<EvolutionDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Evolution/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<EvolutionDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<EvolutionDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allEvolutions = await GetAllAsync();
                return allEvolutions.Where(e => e.MedicalCareId == medicalCareId).ToList();
            }
            catch
            {
                return new List<EvolutionDTO>();
            }
        }

        public async Task<(bool Success, EvolutionDTO? Data, string Error)> CreateAsync(EvolutionDTO evolution)
        {
            try
            {
                var json = JsonSerializer.Serialize(evolution, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Evolution", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var createdEvolution = JsonSerializer.Deserialize<EvolutionDTO>(responseJson, _jsonOptions);
                    return (true, createdEvolution, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error de conexión: {ex.Message}");
            }
        }

        public async Task<(bool Success, EvolutionDTO? Data, string Error)> UpdateAsync(EvolutionDTO evolution)
        {
            try
            {
                var json = JsonSerializer.Serialize(evolution, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/Evolution/{evolution.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updatedEvolution = JsonSerializer.Deserialize<EvolutionDTO>(responseJson, _jsonOptions);
                    return (true, updatedEvolution, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error de conexión: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Evolution/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, $"Error del servidor: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, $"Error de conexión: {ex.Message}");
            }
        }
    }
}
