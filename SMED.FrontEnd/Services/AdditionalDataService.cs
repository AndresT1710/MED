using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    public class AdditionalDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AdditionalDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<List<AdditionalDataDTO>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/AdditionalData");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<AdditionalDataDTO>>(json, _jsonOptions) ?? new List<AdditionalDataDTO>();
                }
                return new List<AdditionalDataDTO>();
            }
            catch
            {
                return new List<AdditionalDataDTO>();
            }
        }

        public async Task<AdditionalDataDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/AdditionalData/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AdditionalDataDTO>(json, _jsonOptions);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<AdditionalDataDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/AdditionalData/by-medical-care/{medicalCareId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<AdditionalDataDTO>>(json, _jsonOptions) ?? new List<AdditionalDataDTO>();
                }
                return new List<AdditionalDataDTO>();
            }
            catch
            {
                return new List<AdditionalDataDTO>();
            }
        }

        public async Task<(bool Success, AdditionalDataDTO? Data, string Error)> CreateAsync(AdditionalDataDTO additionalData)
        {
            try
            {
                var json = JsonSerializer.Serialize(additionalData, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/AdditionalData", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var createdData = JsonSerializer.Deserialize<AdditionalDataDTO>(responseJson, _jsonOptions);
                    return (true, createdData, string.Empty);
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, null, $"Error del servidor: {response.StatusCode} - {errorContent}");
            }
            catch (Exception ex)
            {
                return (false, null, $"Error de conexión: {ex.Message}");
            }
        }

        public async Task<(bool Success, AdditionalDataDTO? Data, string Error)> UpdateAsync(AdditionalDataDTO additionalData)
        {
            try
            {
                var json = JsonSerializer.Serialize(additionalData, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/AdditionalData/{additionalData.AdditionalDataId}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updatedData = JsonSerializer.Deserialize<AdditionalDataDTO>(responseJson, _jsonOptions);
                    return (true, updatedData, string.Empty);
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
                var response = await _httpClient.DeleteAsync($"api/AdditionalData/{id}");
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
