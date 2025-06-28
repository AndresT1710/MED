using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class PlaceOfAttentionService
    {
        private readonly HttpClient _httpClient;

        public PlaceOfAttentionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlaceOfAttentionDTO>> GetAllPlacesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PlaceOfAttention");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener las áreas: {error}");
                    return new List<PlaceOfAttentionDTO>();
                }

                return await response.Content.ReadFromJsonAsync<List<PlaceOfAttentionDTO>>() ?? new List<PlaceOfAttentionDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en GetAllPlacesAsync: {ex}");
                return new List<PlaceOfAttentionDTO>();
            }
        }
    }

}
