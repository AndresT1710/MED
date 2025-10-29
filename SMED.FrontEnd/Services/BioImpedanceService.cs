using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class BioImpedanceService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BioImpedanceService> _logger;

        public BioImpedanceService(HttpClient httpClient, ILogger<BioImpedanceService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<BioImpedanceDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/BioImpedance");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<BioImpedanceDTO>>();
                }
                _logger.LogWarning("Error al obtener bioimpedancias: {StatusCode}", response.StatusCode);
                return new List<BioImpedanceDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bioimpedancias");
                return new List<BioImpedanceDTO>();
            }
        }

        public async Task<BioImpedanceDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/BioImpedance/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<BioImpedanceDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bioimpedancia por ID: {Id}", id);
                return null;
            }
        }

        public async Task<BioImpedanceDTO?> GetByMeasurementsIdAsync(int measurementsId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/BioImpedance/ByMeasurements/{measurementsId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<BioImpedanceDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bioimpedancia por MeasurementsId: {MeasurementsId}", measurementsId);
                return null;
            }
        }

        public async Task<(bool Success, BioImpedanceDTO? Data, string Error)> CreateAsync(BioImpedanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/BioImpedance", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<BioImpedanceDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear bioimpedancia");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(BioImpedanceDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/BioImpedance/{dto.BioImpedanceId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar bioimpedancia");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/BioImpedance/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar bioimpedancia");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, BioImpedanceDTO? Data, string Error)> SaveOrUpdateAsync(BioImpedanceDTO dto)
        {
            try
            {
                if (dto.BioImpedanceId > 0)
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
                _logger.LogError(ex, "Error al guardar o actualizar bioimpedancia");
                return (false, null, ex.Message);
            }
        }
    }
}