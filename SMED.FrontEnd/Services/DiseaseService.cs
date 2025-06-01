using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class DiseaseService
    {
        private readonly HttpClient _http;
        private const string BasePath = "api/disease";

        public DiseaseService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DiseaseDTO?> GetByIdAsync(int diseaseId)
        {
            return await _http.GetFromJsonAsync<DiseaseDTO>($"{BasePath}/{diseaseId}");
        }

        public async Task<List<DiseaseDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<DiseaseDTO>>(BasePath);
            return result ?? new List<DiseaseDTO>();
        }

        public async Task<List<DiseaseDTO>> GetDiseasesByTypeAsync(int diseaseTypeId)
        {
            try
            {
                // Cambié aquí la URL para que coincida con el controlador que funciona
                var response = await _http.GetFromJsonAsync<List<DiseaseDTO>>($"{BasePath}/disease/{diseaseTypeId}");
                return response ?? new List<DiseaseDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener enfermedades por tipo: {ex.Message}");
                return new List<DiseaseDTO>();
            }
        }

        public async Task<List<DiseaseTypeDTO>> GetAllAsyncDiseaseTypes()
        {
            var result = await _http.GetFromJsonAsync<List<DiseaseTypeDTO>>("api/diseasetype/list");
            return result ?? new List<DiseaseTypeDTO>();
        }
    }
}
