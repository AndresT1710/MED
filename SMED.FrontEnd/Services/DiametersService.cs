using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class DiametersService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DiametersService> _logger;

        public DiametersService(HttpClient httpClient, ILogger<DiametersService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<DiametersDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Diameters");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DiametersDTO>>();
                }
                _logger.LogWarning("Error al obtener diámetros: {StatusCode}", response.StatusCode);
                return new List<DiametersDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diámetros");
                return new List<DiametersDTO>();
            }
        }

        public async Task<DiametersDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Diameters/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DiametersDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diámetro por ID: {Id}", id);
                return null;
            }
        }

        public async Task<DiametersDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Diameters/ByMeasurements/{measurementsId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DiametersDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener diámetro por MeasurementsId: {MeasurementsId}", measurementsId);
                return null;
            }
        }

        public async Task<(bool Success, DiametersDTO? Data, string Error)> CreateAsync(DiametersDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Diameters", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<DiametersDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear diámetro");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(DiametersDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Diameters/{dto.DiametersId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar diámetro");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Diameters/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar diámetro");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, DiametersDTO? Data, string Error)> SaveOrUpdateAsync(DiametersDTO dto)
        {
            try
            {
                if (dto.DiametersId > 0)
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
                _logger.LogError(ex, "Error al guardar o actualizar diámetro");
                return (false, null, ex.Message);
            }
        }
    }
}