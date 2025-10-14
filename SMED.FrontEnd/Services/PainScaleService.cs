using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PainScaleService
    {
        private readonly HttpClient _httpClient;

        public PainScaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PainScaleDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PainScaleDTO>>("api/PainScale")
                    ?? new List<PainScaleDTO>();
            }
            catch (Exception)
            {
                return new List<PainScaleDTO>();
            }
        }

        public async Task<List<PainScaleDTO>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var allPainScales = await GetAllAsync();
                return allPainScales.Where(ps => ps.MedicalCareId == medicalCareId).ToList();
            }
            catch (Exception)
            {
                return new List<PainScaleDTO>();
            }
        }

        public async Task<PainScaleDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<PainScaleDTO>($"api/PainScale/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PainScaleDTO?> CreateAsync(PainScaleDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/PainScale", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PainScaleDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PainScaleDTO?> UpdateAsync(PainScaleDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/PainScale/{dto.PainScaleId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<PainScaleDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/PainScale/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PainScaleDTO>> GetByCareIdAsync(int medicalCareId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<PainScaleDTO>>($"api/PainScale/ByCare/{medicalCareId}")
                    ?? new List<PainScaleDTO>();
            }
            catch (Exception)
            {
                return new List<PainScaleDTO>();
            }
        }
    }
}
