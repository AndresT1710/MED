using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PerimetersService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PerimetersService> _logger;

        public PerimetersService(HttpClient httpClient, ILogger<PerimetersService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<PerimetersDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Perimeters");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<PerimetersDTO>>();
                }
                _logger.LogWarning("Error al obtener perímetros: {StatusCode}", response.StatusCode);
                return new List<PerimetersDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perímetros");
                return new List<PerimetersDTO>();
            }
        }

        public async Task<PerimetersDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Perimeters/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PerimetersDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perímetro por ID: {Id}", id);
                return null;
            }
        }

        public async Task<PerimetersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Perimeters/ByMeasurements/{measurementsId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PerimetersDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perímetro por MeasurementsId: {MeasurementsId}", measurementsId);
                return null;
            }
        }

        public async Task<(bool Success, PerimetersDTO? Data, string Error)> CreateAsync(PerimetersDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Perimeters", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<PerimetersDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear perímetro");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(PerimetersDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Perimeters/{dto.PerimetersId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar perímetro");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Perimeters/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar perímetro");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, PerimetersDTO? Data, string Error)> SaveOrUpdateAsync(PerimetersDTO dto)
        {
            try
            {
                if (dto.PerimetersId > 0)
                {
                    var updateResult = await UpdateAsync(dto);
                    return (updateResult.Success, dto, updateResult.Error);
                }
                else
                {
                    return await CreateAsync(dto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o actualizar perímetro");
                return (false, null, ex.Message);
            }
        }
    }
}