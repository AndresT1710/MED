using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class ActionFService
    {
        private readonly HttpClient _httpClient;

        public ActionFService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ActionFDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ActionFDTO>>("api/ActionF")
                    ?? new List<ActionFDTO>();
            }
            catch (Exception)
            {
                return new List<ActionFDTO>();
            }
        }

        public async Task<ActionFDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ActionFDTO>($"api/ActionF/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
