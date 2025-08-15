using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicineService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicineService> _logger;

        public MedicineService(HttpClient httpClient, ILogger<MedicineService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<MedicineDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Medicine");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<MedicineDTO>>();
                }
                _logger.LogWarning("Error al obtener medicamentos: {StatusCode}", response.StatusCode);
                return new List<MedicineDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los medicamentos");
                return new List<MedicineDTO>();
            }
        }

        public async Task<MedicineDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Medicine/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MedicineDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener medicamento por ID: {Id}", id);
                return null;
            }
        }

        public async Task<(bool Success, MedicineDTO? Data, string Error)> CreateAsync(MedicineDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Medicine", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<MedicineDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear medicamento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, MedicineDTO? Data, string Error)> UpdateAsync(MedicineDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Medicine/{dto.Id}", dto);
                if (response.IsSuccessStatusCode)
                {
                    var updated = await response.Content.ReadFromJsonAsync<MedicineDTO>();
                    return (true, updated, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar medicamento");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Medicine/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar medicamento");
                return (false, ex.Message);
            }
        }
    }
}
