using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class DocumentTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DocumentTypeService> _logger;

        public DocumentTypeService(HttpClient httpClient, ILogger<DocumentTypeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<DocumentTypeDTO>?> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DocumentType");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<DocumentTypeDTO>>();
                }
                _logger.LogWarning("Error al obtener tipos de documento: {StatusCode}", response.StatusCode);
                return new List<DocumentTypeDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipos de documento");
                return new List<DocumentTypeDTO>();
            }
        }

        public async Task<DocumentTypeDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/DocumentType/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<DocumentTypeDTO>();
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tipo de documento por ID: {Id}", id);
                return null;
            }
        }
    }
}