using SMED.Shared.DTOs;
using System.Text.Json;

namespace SMED.FrontEnd.Services
{
    public class ReviewSystemDevicesService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ReviewSystemDevicesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        public async Task<List<ReviewSystemDevicesDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/ReviewSystemDevices");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ReviewSystemDevicesDTO>>(json, _jsonOptions) ?? new();
            }
            return new();
        }

        public async Task<List<ReviewSystemDevicesDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            var all = await GetAllAsync();
            return all.Where(r => r.MedicalCareId == medicalCareId).ToList();
        }

        public async Task<(bool Success, ReviewSystemDevicesDTO? Data, string Error)> CreateAsync(ReviewSystemDevicesDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/ReviewSystemDevices", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var created = JsonSerializer.Deserialize<ReviewSystemDevicesDTO>(responseJson, _jsonOptions);
                    return (true, created, string.Empty);
                }
                return (false, null, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, ReviewSystemDevicesDTO? Data, string Error)> UpdateAsync(ReviewSystemDevicesDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, _jsonOptions);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/ReviewSystemDevices/{dto.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var updated = JsonSerializer.Deserialize<ReviewSystemDevicesDTO>(responseJson, _jsonOptions);
                    return (true, updated, string.Empty);
                }
                return (false, null, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ReviewSystemDevices/{id}");
                return (response.IsSuccessStatusCode, response.IsSuccessStatusCode ? "" : await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}