using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ForbiddenFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ForbiddenFoodService> _logger;

        public ForbiddenFoodService(HttpClient httpClient, ILogger<ForbiddenFoodService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ForbiddenFoodDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ForbiddenFood");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ForbiddenFoodDTO>>();
                }
                _logger.LogWarning("Error al obtener alimentos restringidos: {StatusCode}", response.StatusCode);
                return new List<ForbiddenFoodDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener alimentos restringidos");
                return new List<ForbiddenFoodDTO>();
            }
        }

        public async Task<ForbiddenFoodDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ForbiddenFood/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ForbiddenFoodDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener alimento restringido por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<ForbiddenFoodDTO>?> GetByCareIdAsync(int careId)
        {
            try
            {
                var allForbiddenFoods = await GetAllAsync();
                return allForbiddenFoods?
                    .Where(ff => ff.CareId == careId)
                    .ToList() ?? new List<ForbiddenFoodDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener alimentos restringidos por CareId: {CareId}", careId);
                return new List<ForbiddenFoodDTO>();
            }
        }

        public async Task<(bool Success, ForbiddenFoodDTO? Data, string Error)> CreateAsync(ForbiddenFoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ForbiddenFood", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<ForbiddenFoodDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear alimento restringido");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(ForbiddenFoodDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ForbiddenFood/{dto.ForbiddenFoodId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar alimento restringido");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ForbiddenFood/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar alimento restringido");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, ForbiddenFoodDTO? Data, string Error)> SaveOrUpdateAsync(ForbiddenFoodDTO dto)
        {
            try
            {
                if (dto.ForbiddenFoodId > 0)
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
                _logger.LogError(ex, "Error al guardar o actualizar alimento restringido");
                return (false, null, ex.Message);
            }
        }
    }
}