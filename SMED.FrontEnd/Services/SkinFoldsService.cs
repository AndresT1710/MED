using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class SkinFoldsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SkinFoldsService> _logger;

        public SkinFoldsService(HttpClient httpClient, ILogger<SkinFoldsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SkinFoldsDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SkinFolds");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SkinFoldsDTO>>();
                }
                _logger.LogWarning("Error al obtener pliegues cutáneos: {StatusCode}", response.StatusCode);
                return new List<SkinFoldsDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pliegues cutáneos");
                return new List<SkinFoldsDTO>();
            }
        }

        public async Task<SkinFoldsDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SkinFolds/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SkinFoldsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pliegues cutáneos por ID: {Id}", id);
                return null;
            }
        }

        public async Task<SkinFoldsDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SkinFolds/ByMeasurements/{measurementsId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SkinFoldsDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener pliegues cutáneos por MeasurementsId: {MeasurementsId}", measurementsId);
                return null;
            }
        }

        public async Task<(bool Success, SkinFoldsDTO? Data, string Error)> CreateAsync(SkinFoldsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/SkinFolds", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<SkinFoldsDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear pliegues cutáneos");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(SkinFoldsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/SkinFolds/{dto.SkinFoldsId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar pliegues cutáneos");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/SkinFolds/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar pliegues cutáneos");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, SkinFoldsDTO? Data, string Error)> SaveOrUpdateAsync(SkinFoldsDTO dto)
        {
            try
            {
                if (dto.SkinFoldsId > 0)
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
                _logger.LogError(ex, "Error al guardar o actualizar pliegues cutáneos");
                return (false, null, ex.Message);
            }
        }
    }
}