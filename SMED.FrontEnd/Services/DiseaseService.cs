using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class DiseaseService
    {
        private readonly HttpClient _http;

        public DiseaseService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DiseaseDTO?> GetByIdAsync(int diseaseId)
        {
            return await _http.GetFromJsonAsync<DiseaseDTO>($"api/disease/{diseaseId}");
        }

        public async Task<List<DiseaseDTO>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<DiseaseDTO>>("api/disease");
            return result ?? new List<DiseaseDTO>();
        }

        public async Task<List<string>> GetNamesByTypeIdAsync(int diseaseTypeId)
        {
            try
            {
                var response = await _http.GetFromJsonAsync<List<string>>($"api/disease/names/by-type-id/{diseaseTypeId}");
                return response ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public async Task<List<DiseaseTypeDTO>> GetAllAsyncDiseaseTypes()
        {
            var result = await _http.GetFromJsonAsync<List<DiseaseTypeDTO>>("api/diseasetype");
            return result ?? new List<DiseaseTypeDTO>();
        }
    }
}