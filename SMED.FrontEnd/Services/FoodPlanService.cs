using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class FoodPlanService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FoodPlanService> _logger;

        public FoodPlanService(HttpClient httpClient, ILogger<FoodPlanService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<FoodPlanDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/FoodPlan");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<FoodPlanDTO>>();
                }
                _logger.LogWarning("Error al obtener planes de alimentación: {StatusCode}", response.StatusCode);
                return new List<FoodPlanDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planes de alimentación");
                return new List<FoodPlanDTO>();
            }
        }

        public async Task<FoodPlanDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/FoodPlan/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener plan de alimentación por ID: {Id}", id);
                return null;
            }
        }

        public async Task<List<FoodPlanDTO>?> GetByCareIdAsync(int careId)
        {
            try
            {
                var allFoodPlans = await GetAllAsync();
                return allFoodPlans?
                    .Where(fp => fp.CareId == careId)
                    .ToList() ?? new List<FoodPlanDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener planes de alimentación por CareId: {CareId}", careId);
                return new List<FoodPlanDTO>();
            }
        }

        public async Task<(bool Success, FoodPlanDTO? Data, string Error)> CreateAsync(FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/FoodPlan", dto);
                if (response.IsSuccessStatusCode)
                {
                    var created = await response.Content.ReadFromJsonAsync<FoodPlanDTO>();
                    return (true, created, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, null, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear plan de alimentación");
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> UpdateAsync(FoodPlanDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/FoodPlan/{dto.FoodPlanId}", dto);
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar plan de alimentación");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Error)> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FoodPlan/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return (true, string.Empty);
                }
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar plan de alimentación");
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, FoodPlanDTO? Data, string Error)> SaveOrUpdateAsync(FoodPlanDTO dto)
        {
            try
            {
                if (dto.FoodPlanId > 0)
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
                _logger.LogError(ex, "Error al guardar o actualizar plan de alimentación");
                return (false, null, ex.Message);
            }
        }
    }
}