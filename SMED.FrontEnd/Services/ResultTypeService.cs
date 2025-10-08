using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class ResultTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ResultTypeService> _logger;

        public ResultTypeService(HttpClient httpClient, ILogger<ResultTypeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<ResultTypeDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ResultType");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<ResultTypeDTO>>();
                }
                _logger.LogWarning("Error al obtener tipos de resultado: {StatusCode}", response.StatusCode);
                return new List<ResultTypeDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipos de resultado");
                return new List<ResultTypeDTO>();
            }
        }

        public async Task<ResultTypeDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/ResultType/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ResultTypeDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipo de resultado por ID: {Id}", id);
                return null;
            }
        }
    }
}